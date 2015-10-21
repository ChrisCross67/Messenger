using System;
using System.Globalization;
using System.Windows.Data;

namespace Messenger.Converters
{
    public class FlagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
        object parameter, CultureInfo culture)
        {
            return new Uri(string.Format("/Messenger;component/Icons/{0}.png", value), UriKind.RelativeOrAbsolute);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
