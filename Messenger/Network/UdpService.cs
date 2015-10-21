using System;
using System.Net;
using System.Net.Sockets;

namespace Messenger.Network
{
    /// <summary>
    /// Message received delegate
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="remoteEP">The remote ep.</param>
    public delegate void UdpMessageReceivedDelegate(byte[] message, IPEndPoint remoteEP);

    /// <summary>
    /// UdpService
    /// </summary>
    public class UdpService
    {
        /// <summary>
        /// The port number
        /// </summary>
        public int PortNumber;

        /// <summary>
        /// The listening
        /// </summary>
        bool listening = false;

        /// <summary>
        /// The UDP client
        /// </summary>
        UdpClient udpClient;

        /// <summary>
        /// The message received delegate
        /// </summary>
        public UdpMessageReceivedDelegate MessageReceived;

        /// <summary>
        /// Starts the service UPD from the specified end point IP.
        /// </summary>
        /// <param name="ipEndPoint">The end point IP.</param>
        public void Start(IPEndPoint ipEndPoint)
        {
            if (udpClient != null)
            {
                Stop();
            }
            PortNumber = ipEndPoint.Port;
            udpClient = new UdpClient(ipEndPoint);
            //updClient = new UdpClient(new IPEndPoint(IPAddress.Any, ipEndPoint.Port));
            udpClient.EnableBroadcast = true;
            listening = true;
            udpClient.BeginReceive(new AsyncCallback(OnReceive), null);
        }

        /// <summary>
        /// Stops the current UDP service.
        /// </summary>
        public void Stop()
        {
            listening = false;
            if (udpClient != null)
            {
                udpClient.Client.Shutdown(SocketShutdown.Both);
                udpClient.Close();
            }
        }

        /// <summary>
        /// Called when data received from the remote host.
        /// </summary>
        /// <param name="result">The result.</param>
        void OnReceive(IAsyncResult result)
        {
            if (listening)
            {
                try
                {
                    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, PortNumber);
                    byte[] data = udpClient.EndReceive(result, ref remoteEP);
                    if (MessageReceived != null)
                    {
                        MessageReceived(data, remoteEP);
                    }
                }
                catch (ArgumentException)
                {
                }
            }
            if (listening)
            {
                udpClient.BeginReceive(new AsyncCallback(OnReceive), null);
            }
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(byte[] message)
        {
            SendMessage(message, new IPEndPoint(IPAddress.Broadcast, PortNumber));
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="target">The target.</param>
        public void SendMessage(byte[] data, IPEndPoint target)
        {
            udpClient.Send(data, data.Length, target);
        }
    }
}
