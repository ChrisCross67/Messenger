using System.Windows;

namespace Messenger.Dialogs
{
    public static class Screen
    {
        public static double Width
        {
            get { return SystemParameters.WorkArea.Width; }
        }

        public static double Height
        {
            get { return SystemParameters.WorkArea.Height; }
        }
    }
}
