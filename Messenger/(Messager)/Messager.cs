using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows;
using System.Windows.Threading;
using Messenger.Network;
using Messenger.Protocol;
using Messenger.Utils;

namespace Messenger
{
    public class Messager
    {
        public static int PortNumber = 2425;
        private static int PacketNumber = 100000;
        private static UdpService udpService = new UdpService();
        private static TcpService tcpService = new TcpService();
        public static string UserName = Environment.UserName;
        public static Member Self = new Member();
        public static Encoding TextEncoding = Encoding.Default;
        public static MemberList Members = new MemberList();
        public static Dictionary<int, Attachment> FileMap = new Dictionary<int, Attachment>();
        public static ObservableCollection<Attachment> Files = new ObservableCollection<Attachment>();
        public static Logger Logger;
        private static Dispatcher UIDispatcher;

        public static void StartListening(IPAddress ipAddress, Dispatcher dispatcher)
        {
            Self.UserName = System.Environment.UserName;
            Self.MachineName = System.Environment.MachineName;
            Self.IPEndPoint = new IPEndPoint(ipAddress, PortNumber);
            UIDispatcher = dispatcher;
            udpService.MessageReceived = delegate(byte[] data, IPEndPoint remoteEP)
            {
                Message message = Message.Decode(data, remoteEP);
                LogMessage(message);
                HandleMessage(message);
            };
            tcpService.TcpConnectionAccepted = SendFile;
            try
            {
                Logger.Info("Start UdpService");
                udpService.Start(Self.IPEndPoint);
                Logger.Info("Start TcpService");
                tcpService.Start(Self.IPEndPoint);
            }
            catch (Exception error)
            {
                LogError(string.Format("Start network service failed. {0}", error.Message));
            }
        }

        public static void StopListening()
        {
            tcpService.Stop();
            udpService.Stop();
        }

        private static void LogMessage(Message message)
        {
            Logger.Info(string.Format(
                "Message received from: {0} ({1})\r\nPacket:{2}\tCommand:{3:X}\tOption:{4:X}\tHasAttachement:{5}\r\nContent:{6}",
                message.SenderName, message.SenderHost, message.PacketNo, message.Command, message.Option, message.HasAttachment, message.Content
                ));
        }

        private static void LogMessage(Message message, Member member)
        {
            Logger.Info(string.Format(
                "Message send to: {0} ({1})\r\nPacket:{2}\tCommand:{3:X}\tOption:{4:X}\tHasAttachement:{5}\r\nContent:{6}",
                member.UserName, member.MachineName, message.PacketNo, message.Command, message.Option, message.HasAttachment, message.Content
                ));
        }

        public static void LogError(string errorMsg)
        {
            Logger.Error(errorMsg);
            MessageBox.Show(errorMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private static void HandleMessage(Message message)
        {
            switch (message.Command)
            {
                case CommandID.IPMSG_BR_ENTRY:
                    AddNewMember(message, true);
                    break;
                case CommandID.IPMSG_ANSENTRY:
                    AddNewMember(message, false);
                    break;
                case CommandID.IPMSG_BR_EXIT:
                    RemoveMember(message);
                    break;
                case CommandID.IPMSG_SENDMSG:
                    ReceivedMessage(message);
                    break;
                case CommandID.IPMSG_RECVMSG:
                    break;
                case CommandID.IPMSG_RELEASEFILES:
                    FilesDropped(message);
                    break;
                case CommandID.IPMSG_GETINFO:
                    SendInfo(message);
                    break;
                case CommandID.IPMSG_SENDINFO:
                    GetMemberInfo(message);
                    break;
            }
        }

        private static Member CreateMember(Message message)
        {
            Member member = new Member();
            member.UserName = message.SenderName;
            member.MachineName = message.SenderHost;
            member.IPEndPoint = message.SenderIP;
            return member;
        }

        public static void RefreshMembers()
        {
            UIDispatcher.Invoke(new Action(delegate
            {
                Members.Clear();
            }
            ));
            BroadcastSelf();
        }

        private static void AddNewMember(Message message, bool answer)
        {
            Member member = GetMember(message);
            if (message.Content.Trim().Length > 0)
            {
                member.UserName = message.Content;
            }
            if (member.Icon == null)
            {
                RequestIconInfo(member);
            }

            UIDispatcher.Invoke(new Action(delegate
            {
                Members.AddMember(member);
            }
            ));
            if (answer && !member.IPAddress.Equals(Self.IPAddress))
            {
                AnswerNewMember(member);
            }
        }

        private static void RemoveMember(Message message)
        {
            UIDispatcher.Invoke(new Action(delegate
            {
                Members.RemoveMember(message.SenderIP.Address);
            }
            ));
        }

        public static Member GetMember(Message message)
        {
            Member member = Members.GetMember(message.SenderIP.Address);
            if (member != null)
            {
                return member;
            }
            else
            {
                return CreateMember(message);
            }
        }

        public static Message CreateMessage(int command, string text)
        {
            return CreateMessage(command, text, null);
        }

        public static Message CreateMessage(int command, string text, List<Attachment> attachments)
        {
            Message message = new Message();
            message.PacketNo = PacketNumber++;
            message.Command = (uint)command;
            message.SenderName = Self.UserName;
            message.SenderHost = Self.MachineName;
            message.Date = DateTime.Now;
            message.Content = text;
            message.Attachments = attachments;
            return message;
        }

        private static void BroadcastSelf()
        {
            SendMessage(CommandID.IPMSG_BR_ENTRY, UserName);
        }

        public static void BroadcastExit()
        {
            SendMessage(CommandID.IPMSG_BR_EXIT, UserName);
        }

        private static void AnswerNewMember(Member member)
        {
            SendMessage(member, CommandID.IPMSG_ANSENTRY, UserName, null);
        }

        private static void SendInfo(Message message)
        {
            if (message.Content == "ICON")
            {
                SendIconInfo(message);
            }
            else
            {
                SendVersionInfo(message);
            }
        }

        private static void SendVersionInfo(Message message)
        {
            Version version = App.GetVersion();
            ReplyMessage(message, CommandID.IPMSG_SENDINFO, version.ToString());
        }

        private static void SendIconInfo(Message message)
        {
            if (Self.Icon == null) return;
            Message msg = CreateMessage(CommandID.IPMSG_SENDINFO, null);
            msg.Option = (uint)CommandOption.IPMSG_FILEATTACHOPT;
            msg.AdditionalSection = Self.Icon;
            SendMessage(msg, message.SenderIP);
            Logger.Info(string.Format("Send ICON to {0}", message.Sender));
        }

        private static void RequestIconInfo(Member member)
        {
            SendMessage(member, CommandID.IPMSG_GETINFO, "ICON", null);
        }

        private static void GetMemberInfo(Message message)
        {
            Member member = GetMember(message);
            if (message.HasIconData)
            {
                member.Icon = message.AdditionalSection;
            }
            else
            {
                member.Version = message.Content;
            }
        }

        public static void SendMessage(Member member, string text, List<Attachment> attachments)
        {
            //SendMessage(member, CommandID.IPMSG_NOOPERATION, null, null);
            SendMessage(member, CommandID.IPMSG_SENDMSG, text, attachments);
        }

        private static void AddToFileMap(Attachment attachment)
        {
            int id = FileMap.Count;
            while (FileMap.ContainsKey(id))
            {
                id++;
            }
            attachment.ID = id;
            FileMap.Add(id, attachment);
            Files.Add(attachment);
        }

        public static void RemoveFromFileMap(Attachment attachment)
        {
            FileMap.Remove(attachment.ID);
            UIDispatcher.Invoke(new Action(delegate
            {
                Files.Remove(attachment);
            }
            ));
        }

        public static void DropFiles(Message message)
        {
            ReplyMessage(message, CommandID.IPMSG_RELEASEFILES, message.PacketNo.ToString());
        }

        private static void FilesDropped(Message message)
        {
            int packetNo = int.Parse(message.Content);
            var attachments = Files.Where(file => file.PacketNo == packetNo).ToArray();
            foreach (var attachment in attachments)
            {
                RemoveFromFileMap(attachment);
            }
        }

        private static void SendMessage(int command, string text)
        {
            Message message = CreateMessage(command, text);
            SendMessage(message, null);
        }

        private static void SendMessage(Member member, int command, string text, List<Attachment> attachments)
        {
            Message message = CreateMessage(command, text, attachments);
            if (message.Command == CommandID.IPMSG_SENDMSG)
            {
                message.ConfirmReceive = true;
                if (message.Attachments != null && message.Attachments.Count > 0)
                {
                    message.HasAttachment = true;
                    foreach (var attachment in message.Attachments)
                    {
                        attachment.PacketNo = message.PacketNo;
                        AddToFileMap(attachment);
                    }
                }
            }
            SendMessage(message, member.IPEndPoint);
            LogMessage(message, member);
        }

        private static void ReplyMessage(Message message, int command, string text)
        {
            Message msg = CreateMessage(command, text);
            SendMessage(msg, message.SenderIP);
        }

        private static void SendMessage(Message message, IPEndPoint target)
        {
            byte[] data = Message.Encode(message);
            try
            {
                if (target == null)
                {
                    udpService.SendMessage(data);
                }
                else
                {
                    udpService.SendMessage(data, target);
                }
            }
            catch (Exception error)
            {
                LogError(string.Format("Send message failed({0}). {1}", message, error.Message));
            }
        }

        private static void ReceivedMessage(Message message)
        {
            Member member = GetMember(message);
            message.SenderName = member.UserName;

            if (message.ConfirmReceive)
            {
                ReplyMessage(message, CommandID.IPMSG_RECVMSG, message.PacketNo.ToString());
            }

            UIDispatcher.Invoke(new Action(delegate
            {
                var receivedMessage = GetWindowOpen<MessageWindow>(message.SenderName);
                message.Date = DateTime.Now;
                if (receivedMessage == null)
                {
                    receivedMessage = new MessageWindow();
                    receivedMessage.DataContext = message;
                    receivedMessage.Sender = GetMember(message);
                    receivedMessage.SentMessages.Add(message);
                    receivedMessage.Name = message.SenderName;
                    receivedMessage.Show();
                    if (Properties.Settings.Default.ActivateComingMessage)
                    {
                        receivedMessage.Activate();
                    }
                }
                else
                {
                    receivedMessage.SentMessages.Add(message);
                    receivedMessage.Activate();

                }
            }));
        }
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
        public static T GetWindowOpen<T>(string name = "") where T : Window
        {
            if (IsWindowOpen<T>(name))
                return string.IsNullOrEmpty(name)
                   ? Application.Current.Windows.OfType<T>().FirstOrDefault()
                   : Application.Current.Windows.OfType<T>().FirstOrDefault(w => w.Name.Equals(name));

            return null;
        }
        public static void ReceiveFiles(Message message, Attachment[] attachments, string folder,
            Action<string> progressStart, Action<double, double> progressUpdate, Action endProgress)
        {
            TcpConnection connection = new TcpConnection(Self.IPEndPoint);
            try
            {
                FileReceiver receiver = new FileReceiver(folder, connection, progressStart, progressUpdate);
                receiver.ReceiveFiles(message, attachments);
                endProgress();
            }
            catch (Exception error)
            {
                endProgress();
                LogError(string.Format("Receive files from {0} failed. {1}", message.SenderIP, error.Message));
            }
            ReplyMessage(message, CommandID.IPMSG_NOOPERATION, null);
        }

        private static void SendFile(TcpConnection tcpConnection)
        {
            byte[] data = tcpConnection.Receive();
            if (data == null)
            {
                Logger.Error("No request received in Tcp Connection when sending file");
                return;
            }
            Message message = Message.Decode(data, tcpConnection.RemoteEndPoint);
            string[] items = message.Content.Split(':');
            int packetNo = int.Parse(items[0], System.Globalization.NumberStyles.HexNumber);
            int id = int.Parse(items[1], System.Globalization.NumberStyles.HexNumber);

            if (FileMap.ContainsKey(id))
            {
                Attachment attachment = FileMap[id];
                if (attachment.PacketNo == packetNo)
                {
                    SendAttachment(tcpConnection, message, items, attachment);
                }
            }
        }

        private static void SendAttachment(TcpConnection tcpConnection, Message message, string[] items, Attachment attachment)
        {
            try
            {
                FileSender sender = new FileSender(tcpConnection);
                if (message.Command == CommandID.IPMSG_GETFILEDATA)
                {
                    if (attachment.IsFile)
                    {
                        Logger.Info("Send file " + attachment.Name);
                        long offset = long.Parse(items[2], System.Globalization.NumberStyles.HexNumber);
                        sender.SendFile(attachment.FullPath, offset);
                        RemoveFromFileMap(attachment);
                    }
                }
                else if (message.Command == CommandID.IPMSG_GETDIRFILES)
                {
                    if (attachment.IsDirectory)
                    {
                        Logger.Info("Send directoy " + attachment.Name);
                        sender.SendDirectory(attachment.FullPath);
                        RemoveFromFileMap(attachment);
                    }
                }
            }
            catch (Exception error)
            {
                LogError(string.Format("Send file {0} failed. {1}", attachment.FullPath, error.Message));
            }
            finally
            {
                tcpConnection.Close();
            }
        }
    }
}