using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Messenger.Network
{
    /// <summary>
    /// TcpConnection
    /// </summary>
    public class TcpConnection
    {
        /// <summary>
        /// The self ip end point
        /// </summary>
        IPEndPoint selfIPEndPoint;
        /// <summary>
        /// The TCP client
        /// </summary>
        TcpClient tcpClient;
        /// <summary>
        /// The network stream
        /// </summary>
        NetworkStream networkStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpConnection" /> class.
        /// </summary>
        /// <param name="ipEndPoint">The ip end point.</param>
        public TcpConnection(IPEndPoint ipEndPoint)
        {
            selfIPEndPoint = ipEndPoint;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpConnection" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public TcpConnection(TcpClient client)
        {
            tcpClient = client;
            networkStream = tcpClient.GetStream();
        }

        /// <summary>
        /// Gets the remote end point.
        /// </summary>
        /// <value>
        /// The remote end point.
        /// </value>
        public IPEndPoint RemoteEndPoint
        {
            get { return (IPEndPoint)tcpClient.Client.RemoteEndPoint; }
        }

        /// <summary>
        /// Opens the specified end point IP.
        /// </summary>
        /// <param name="ipEndPoint">The end point IP.</param>
        /// <returns></returns>
        public bool Open(IPEndPoint ipEndPoint)
        {
            //tcpClient = new TcpClient(selfIPEndPoint);
            tcpClient = new TcpClient();
            tcpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            tcpClient.Connect(ipEndPoint);
            if (tcpClient.Connected)
            {
                networkStream = tcpClient.GetStream();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Closes the current TCP connection.
        /// </summary>
        public void Close()
        {
            tcpClient.Client.Shutdown(SocketShutdown.Both);
            networkStream.Close();
            tcpClient.Close();
        }

        /// <summary>
        /// Sends the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Send(byte[] data)
        {
            networkStream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Sends the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="progressUpdate">The progress update.</param>
        public void Send(Stream stream, Action<long> progressUpdate)
        {
            long bytesTransferred = 0;
            int bytesRead = 1;
            byte[] buffer = new byte[tcpClient.SendBufferSize];
            while (stream.CanRead && bytesRead > 0)
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                networkStream.Write(buffer, 0, bytesRead);
                bytesTransferred += bytesRead;

                if (progressUpdate != null)
                {
                    progressUpdate(bytesTransferred);
                }
            }
        }

        /// <summary>
        /// Receives data from network.
        /// </summary>
        /// <returns></returns>
        public byte[] Receive()
        {
            int loop = 0;
            while (loop < 1000)
            {
                if (networkStream.DataAvailable)
                {
                    byte[] data = new byte[tcpClient.Available];
                    networkStream.Read(data, 0, data.Length);
                    return data;
                }
                System.Threading.Thread.Sleep(1);
                loop++;
            }
            return null;
        }

        /// <summary>
        /// Receives the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="bytesToReceive">The bytes to receive.</param>
        /// <param name="progressUpdate">The progress update.</param>
        /// <returns></returns>
        public long Receive(Stream stream, long bytesToReceive, Action<long> progressUpdate)
        {
            long bytesTransferred = 0;
            int bytesRead = 0;
            byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
            int loop = 0;
            while (loop < 1000 && bytesTransferred < bytesToReceive)
            {
                while (networkStream.DataAvailable && bytesTransferred < bytesToReceive)
                {
                    loop = 0;
                    int bytesToRead = (int)(bytesToReceive - bytesTransferred);
                    if (bytesToRead > buffer.Length)
                    {
                        bytesToRead = buffer.Length;
                    }
                    bytesRead = networkStream.Read(buffer, 0, bytesToRead);
                    stream.Write(buffer, 0, bytesRead);
                    bytesTransferred += bytesRead;

                    if (progressUpdate != null)
                    {
                        progressUpdate(bytesTransferred);
                    }
                }
                System.Threading.Thread.Sleep(1);
                loop++;
            }
            stream.Flush();
            return bytesTransferred;
        }
    }
}
