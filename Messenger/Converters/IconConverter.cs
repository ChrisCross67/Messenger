using System;
using System.IO;
using System.Drawing;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Messenger.Converters
{
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                byte[] data = (byte[])value;
                if (data != null)
                {
                    IntPtr hIcon = new Icon(new MemoryStream(data)).Handle;
                    return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                        hIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
            }
            catch { }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
