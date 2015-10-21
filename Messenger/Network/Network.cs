using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace Messenger.Network
{
    public class Network
    {
        public static List<IPAddress> GetIPv4NetworkInterfaces()
        {
            List<IPAddress> IPv4Addresses = new List<IPAddress>();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback
                    || adapter.OperationalStatus != OperationalStatus.Up)
                {
                    continue;
                }
                // Only get information for interfaces that support IPv4.
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                    foreach (UnicastIPAddressInformation ipAddress in adapterProperties.UnicastAddresses)
                    {
                        if (ipAddress.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            IPv4Addresses.Add(ipAddress.Address);
                        }
                    }
                }
            }
            return IPv4Addresses;
        }

        public static string PresentTransferSpeed(double bytesPerSecond)
        {
            string speed = PresentDataSize(bytesPerSecond);
            return string.Format("{0}/S", speed);
        }

        public static string PresentDataSize(double bytes)
        {
            double K = 1024;
            double M = K * 1024;
            double G = M * 1024;
            string unit = "B";
            double size = bytes;
            if (size >= G)
            {
                unit = "GB";
                size = Math.Round(size / G, 2);
            }
            else if (size >= 1024 * 1024)
            {
                unit = "MB";
                size = Math.Round(size / M, 2);
            }
            else if (size >= 1024)
            {
                unit = "KB";
                size = Math.Round(size / K, 2);
            }
            return string.Format("{0}{1}", size, unit);
        }
    }
}
