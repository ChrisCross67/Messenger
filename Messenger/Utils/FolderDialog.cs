using System;

namespace Messenger.Utils
{
    public class FolderDialog
    {
        private class OldWindow : System.Windows.Forms.IWin32Window
        {
            IntPtr _handle;
            public OldWindow(IntPtr handle)
            {
                _handle = handle;
            }

            #region IWin32Window Members
            IntPtr System.Windows.Forms.IWin32Window.Handle
            {
                get { return _handle; }
            }
            #endregion
        }

        System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();

        public bool ShowDialog(System.Windows.Window window)
        {
            System.Windows.Interop.HwndSource source = System.Windows.PresentationSource.FromVisual(window) as System.Windows.Interop.HwndSource;
            System.Windows.Forms.IWin32Window win = new OldWindow(source.Handle);
            return fd.ShowDialog(win) == System.Windows.Forms.DialogResult.OK;
        }

        public string Folder
        {
            get { return fd.SelectedPath; }
            set { fd.SelectedPath = value; }
        }
    }
}
