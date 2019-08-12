using System;
using System.Globalization;
using System.Windows.Data;

namespace Common.Converts
{
    public class OwnershipTypeToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "无";
            }
            var state = value.ToString();
            if ("1".Equals(state))
            {
                return "调";
            }
            if ("2".Equals(state))
            {
                return "补";
            }
            return "无";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
