using System.Net;
using System.ComponentModel;

namespace Messenger
{
    public class Member : INotifyPropertyChanged
    {
        public string UserName { get; set; }

        public string MachineName { get; set; }

        public IPEndPoint IPEndPoint { get; set; }

        public IPAddress IPAddress
        {
            get { return IPEndPoint.Address; }
        }

        public string Version { get; set; }

        byte[] icon;
        public byte[] Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged("Icon");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
