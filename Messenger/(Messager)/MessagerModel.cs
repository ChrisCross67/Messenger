using Messenger.Protocol;
using Messenger.Utils;
using System.Collections.ObjectModel;

namespace Messenger
{
    public class MessagerModel : NotifyPropertyChangedBase
    {
        private Member _member;
        private ObservableCollection<Attachment> _attachments = new ObservableCollection<Attachment>();
        private string _messageToSend;
        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();

        public ObservableCollection<Message> Messages
        {
            get
            {
                return _messages;
            }

            set
            {
                _messages = value; OnPropertyChanged();
            }
        }

        public string MessageToSend
        {
            get
            {
                return _messageToSend;
            }

            set
            {
                _messageToSend = value; OnPropertyChanged();
            }
        }

        public Member Member
        {
            get
            {
                return _member;
            }

            set
            {
                _member = value; OnPropertyChanged();
            }
        }

        public ObservableCollection<Attachment> Attachments
        {
            get
            {
                return _attachments;
            }

            set
            {
                _attachments = value; OnPropertyChanged();
            }
        }
    }
}
