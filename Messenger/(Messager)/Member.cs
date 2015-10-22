using System.Net;
using Messenger.Utils;

namespace Messenger
{
    public class Member : NotifyPropertyChangedBase
    {
        string userName;

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }
        string machineName;

        public string MachineName
        {
            get
            {
                return machineName;
            }

            set
            {
                machineName = value;
                OnPropertyChanged();
            }
        }
        IPEndPoint iPEndPoint;

        public IPEndPoint IPEndPoint
        {
            get
            {
                return iPEndPoint;
            }

            set
            {
                iPEndPoint = value;
                OnPropertyChanged();
            }
        }

        public IPAddress IPAddress
        {
            get { return IPEndPoint.Address; }
        }
        string version;

        public string Version
        {
            get
            {
                return version;
            }

            set
            {
                version = value;
                OnPropertyChanged();
            }
        }

        byte[] icon;
        public byte[] Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged();
            }
        }
    }
}
