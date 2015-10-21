using System;
using System.IO;
using System.Drawing;
using System.Windows;
using System.Windows.Data;
using System.Windows.Resources;
using System.Windows.Media.Imaging;
using Messenger.Utils;

namespace Messenger.Converters
{
    public class FileIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string filepath = (string)value;
            if (File.Exists(filepath))
            {
                IntPtr hIcon = Icon.ExtractAssociatedIcon(filepath).Handle;
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    hIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            else if (Directory.Exists(filepath))
            {
                IntPtr hIcon = Win32.GetFolderIcon();
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    hIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            else
            {
                Uri uri = new Uri(filepath, UriKind.Relative);
                StreamResourceInfo info = Application.GetResourceStream(uri);
                if (info != null)
                {
                    IntPtr hIcon = new Icon(info.Stream).Handle;
                    return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                        hIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
