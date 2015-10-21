namespace Messenger.Utils
{
    public enum MessageType { Info, Error, Warnning }

    public abstract class Logger
    {
        public abstract void Log(MessageType msgType, string text);

        public abstract string Messages { get; }

        public abstract void Clear();

        public virtual void Info(string text)
        {
            Log(MessageType.Info, text);
        }

        public virtual void Error(string text)
        {
            Log(MessageType.Error, text);
        }

        public virtual void Warnning(string text)
        {
            Log(MessageType.Warnning, text);
        }
    }
}
