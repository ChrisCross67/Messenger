using Messenger.Protocol;
using Messenger.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Messenger.Converters
{
    public class ToCheckableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value as List<Attachment> == null)
                return null;
            return (from file in value as List<Attachment>
                              select new Checkable<Attachment>(file))
                              .ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
