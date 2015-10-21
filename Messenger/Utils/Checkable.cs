namespace Messenger.Utils
{
    public class Checkable<T>
    {
        public bool Checked { get; set; }

        public T Item { get; set; }

        public Checkable(T item)
        {
            Item = item;
            Checked = true;
        }

        public Checkable(T item, bool ischecked)
        {
            Item = item;
            Checked = ischecked;
        }
    }
}
