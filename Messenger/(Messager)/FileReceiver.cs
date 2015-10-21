using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using Messenger.Network;
using Messenger.Protocol;

namespace Messenger
{
    class FileReceiver
    {
        string RootFolder;
        TcpConnection Connection;
        Action<string> ProgressStart;
        Action<double, double> ProgressUpdate;

        public FileReceiver(string folder, TcpConnection connection,
            Action<string> progressStart, Action<double, double> progressUpdate)
        {
            RootFolder = folder;
            Connection = connection;
            ProgressStart = progressStart;
            ProgressUpdate = progressUpdate;
        }

        public void ReceiveFiles(Message message, Attachment[] attachments)
        {
            foreach (Attachment attachment in attachments)
            {
                if (ProgressStart != null)
                {
                    ProgressStart(attachment.Name);
                }

                string filePath = Path.Combine(RootFolder, attachment.Name);
                if (WhetherOverWrite(filePath) == false) continue;

                Connection.Open(message.SenderIP);
                if (attachment.IsFile)
                {
                    string text = string.Format("{0:x}:{1:x}:{2:x}", message.PacketNo, attachment.ID, 0);
                    Message getFileMsg = Messager.CreateMessage(CommandID.IPMSG_GETFILEDATA, text);
                    byte[] data = Message.Encode(getFileMsg);

                    Connection.Send(data);

                    Messager.Logger.Info("Receive file " + attachment.Name);
                    FileStream stream = new FileStream(filePath, FileMode.Create);
                    DateTime startTime = DateTime.Now;
                    long bytes = Connection.Receive(stream, attachment.Size, delegate(long bytesTransferred)
                    {
                        double progress = (double)bytesTransferred / attachment.Size;
                        if ((int)(progress * 100) % 5 == 0)
                        {
                            ProgressUpdate(progress, (double)bytesTransferred / (DateTime.Now - startTime).TotalSeconds);
                        }
                    });
                    stream.Close();
                    Messager.Logger.Info(bytes + " bytes received of file " + attachment.Name);
                }
                else if (attachment.IsDirectory)
                {
                    string text = string.Format("{0:x}:{1:x}", message.PacketNo, attachment.ID);
                    Message getDirFilesMsg = Messager.CreateMessage(CommandID.IPMSG_GETDIRFILES, text);
                    byte[] data = Message.Encode(getDirFilesMsg);

                    Connection.Send(data);
                    Messager.Logger.Info("Receive directoy " + attachment.Name);

                    //string folder = Path.Combine(RootFolder, attachment.Name);
                    //Directory.CreateDirectory(folder);

                    byte[] peekdata = Connection.Receive();
                    ReceiveDirectory(RootFolder, peekdata);

                    Messager.Logger.Info("Receive directoy " + attachment.Name + " finished.");
                }
                Connection.Close();
            }
        }

        private void ReceiveDirectory(string folder, byte[] peekdata)
        {
            if (peekdata == null) return;
            int index = Array.IndexOf<byte>(peekdata, (byte)':');
            if (index > 0)
            {
                int headerSize = int.Parse(Encoding.ASCII.GetString(peekdata, 0, index), System.Globalization.NumberStyles.HexNumber);
                if (headerSize > peekdata.Length)
                {
                    List<byte> header = peekdata.ToList();
                    while (headerSize > header.Count)
                    {
                        header.AddRange(Connection.Receive());
                    }
                    peekdata = header.ToArray();
                }
                Attachment subFile = Attachment.DecodeHeader(peekdata, index + 1, headerSize);
                ReceivedSubFile(folder, peekdata, headerSize, subFile);
            }
        }

        private void ReceivedSubFile(string folder, byte[] peekdata, int headerSize, Attachment subFile)
        {
            int usedSize = headerSize;
            if (subFile.IsFile)
            {
                if (ProgressStart != null)
                {
                    ProgressStart(subFile.Name);
                }
                string filePath = Path.Combine(folder, subFile.Name);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                if (subFile.Size > peekdata.Length - headerSize)
                {
                    DateTime startTime = DateTime.Now;
                    stream.Write(peekdata, headerSize, peekdata.Length - headerSize);
                    Connection.Receive(stream, subFile.Size - stream.Length, delegate(long bytesTransferred)
                    {
                        double progress = (double)bytesTransferred / subFile.Size;
                        if ((int)(progress * 100) % 5 == 0)
                        {
                            ProgressUpdate(progress, (double)bytesTransferred / (DateTime.Now - startTime).TotalSeconds);
                        }
                    });
                }
                else
                {
                    stream.Write(peekdata, headerSize, (int)subFile.Size);
                }
                stream.Close();
                usedSize += (int)subFile.Size;
            }
            else if (subFile.IsDirectory)
            {
                folder = Path.Combine(folder, subFile.Name);
                Directory.CreateDirectory(folder);
            }
            else if (subFile.IsReturnParent)
            {
                folder = Path.GetDirectoryName(folder);
                if (folder == RootFolder)
                {
                    return; // finished
                }
            }
            if (usedSize < peekdata.Length)
            {
                byte[] buffer = new byte[peekdata.Length - usedSize];
                Array.Copy(peekdata, usedSize, buffer, 0, buffer.Length);
                peekdata = buffer;
            }
            else
            {
                peekdata = Connection.Receive();
            }
            ReceiveDirectory(folder, peekdata);
        }

        private static bool WhetherOverWrite(string filePath)
        {
            if (File.Exists(filePath))
            {
                var result = MessageBox.Show(
                    filePath + " already exists, overwirte or not?", "Note",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
