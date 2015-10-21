using System;
using System.Text;
using System.Windows;

namespace Messenger.Utils.WPF
{
    public class MessageLogger : Logger
    {
        StringBuilder _receiver;

        public MessageLogger()
        {
            _receiver = new StringBuilder();
        }

        public override string Messages
        {
            get { return _receiver.ToString(); }
        }

        public override void Log(MessageType msgType, string text)
        {
            string type = null;
            if (msgType != MessageType.Info)
            {
                type = msgType.ToString() + ": ";
            }

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logtext = string.Format("{0} {1}{2}\r\n", time, type, text);

            Action logAction = delegate
            {
                _receiver.AppendLine(logtext);
            };

            Application.Current.Dispatcher.Invoke(logAction);
        }

        public override void Clear()
        {
            Action logAction = delegate
            {
                _receiver.Clear();
            };

            Application.Current.Dispatcher.Invoke(logAction);
        }
    }

}
