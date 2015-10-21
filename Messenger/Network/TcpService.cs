using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Messenger.Network
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="tcpConnection">The TCP connection.</param>
    public delegate void TcpConnectionAcceptedDelegate(TcpConnection tcpConnection);

    /// <summary>
    /// TcpService
    /// </summary>
    public class TcpService
    {
        /// <summary>
        /// The end point IP of the current user
        /// </summary>
        IPEndPoint selfIPEndPoint;
        /// <summary>
        /// The TCP listener
        /// </summary>
        TcpListener tcpListener;
        /// <summary>
        /// The listening
        /// </summary>
        bool listening = false;

        /// <summary>
        /// The TCP connection accepted
        /// </summary>
        public TcpConnectionAcceptedDelegate TcpConnectionAccepted;

        /// <summary>
        /// Starts the specified end point IP.
        /// </summary>
        /// <param name="ipEndPoint">The end point IP.</param>
        public void Start(IPEndPoint ipEndPoint)
        {
            selfIPEndPoint = ipEndPoint;
            //tcpListener = new TcpListener(selfIPEndPoint);
            tcpListener = new TcpListener(selfIPEndPoint);
            tcpListener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            tcpListener.Server.NoDelay = true;
            tcpListener.Start();
            listening = true;
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(OnAcceptTcpClient), null);
        }

        /// <summary>
        /// Called when the TCP service accepted a client.
        /// </summary>
        /// <param name="result">The result.</param>
        void OnAcceptTcpClient(IAsyncResult result)
        {
            if (listening)
            {
                TcpClient client = tcpListener.EndAcceptTcpClient(result);
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                client.Client.NoDelay = true;
                if (TcpConnectionAccepted != null)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(
                        delegate(object obj)
                        {
                            TcpConnectionAccepted((TcpConnection)obj);
                        }));
                    thread.Start(new TcpConnection(client));
                }
            }
            if (listening)
            {
                tcpListener.BeginAcceptTcpClient(new AsyncCallback(OnAcceptTcpClient), null);
            }
        }

        /// <summary>
        /// Stops the current TCP service.
        /// </summary>
        public void Stop()
        {
            listening = false;
            if (tcpListener != null)
            {
                tcpListener.Stop();
            }
        }
    }
}
