using System;
using System.Windows.Data;
using Messenger.Protocol;

namespace Messenger.Converters
{
    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Attachment attachment = (Attachment)value;
            if (attachment != null && attachment.IsFile)
            {
                return Network.Network.PresentDataSize(attachment.Size);
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
