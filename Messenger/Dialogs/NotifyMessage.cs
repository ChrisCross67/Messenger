namespace Messenger.Dialogs
{
    public class NotifyMessage
    {
        private readonly string _popupText;
        private readonly string _titleText;
        private readonly MessageType _type;

        public NotifyMessage(string titleText,string popupText,MessageType type = MessageType.Normal)
        {
            _titleText = titleText;
            _popupText = popupText;
            _type = type;
        }

        public string PopupText
        {
            get { return _popupText; }
        }

        public string TitleText
        {
            get { return _titleText; }
        }
        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <value>
        /// The type of the message.
        /// </value>
        public MessageType Type
        {
            get { return _type; }
        }
    }
}


