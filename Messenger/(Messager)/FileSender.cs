using System.Linq;
using System.IO;
using System.Text;
using Messenger.Utils;
using Messenger.Network;
using Messenger.Protocol;

namespace Messenger
{
    class FileSender
    {
        TcpConnection Connection;

        public FileSender(TcpConnection tcpConnection)
        {
            Connection = tcpConnection;
        }

        public void SendFile(string file, long offset)
        {
            FileStream stream = new FileStream(file, FileMode.Open);
            stream.Position = offset;
            Connection.Send(stream, null);
            stream.Close();
        }

        public void SendDirectory(string directory)
        {
            DirectoryWalker walker = new DirectoryWalker();
            walker.BeforeVisitDirectory += SendDirectory;
            walker.VisitFile += SendFile;
            walker.AfterVisitDirectory += SendReturnParent;
            walker.Traverse(directory);
        }

        private void SendFile(FileInfo fileInfo)
        {
            Attachment subFile = new Attachment();
            subFile.Name = fileInfo.Name;
            subFile.Size = fileInfo.Length;
            subFile.IsFile = true;
            SendHeader(subFile);
            FileStream stream = fileInfo.OpenRead();
            Connection.Send(stream, null);
            stream.Close();
        }

        private void SendDirectory(DirectoryInfo dirInfo)
        {
            Attachment subFile = new Attachment();
            subFile.Name = dirInfo.Name;
            subFile.IsDirectory = true;
            SendHeader(subFile);
        }

        private void SendReturnParent(DirectoryInfo dirInfo)
        {
            Attachment subFile = new Attachment();
            subFile.Name = ".";
            subFile.IsReturnParent = true;
            SendHeader(subFile);
        }

        private void SendHeader(Attachment subFile)
        {
            byte[] header = Attachment.EncodeHeader(subFile);
            int headerSize = header.Length + 4;
            byte[] size = Encoding.ASCII.GetBytes(string.Format("{0:x4}", headerSize));
            header = size.Concat(header).ToArray();
            Connection.Send(header);
        }
    }
}
