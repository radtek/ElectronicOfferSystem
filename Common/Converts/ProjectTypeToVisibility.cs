using Common.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Common.Converts
{
    public class ProjectTypeToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }
            var type = value.ToString();
            if (((int)EProjectType.RealEstate).ToString().Equals(type))
            {
                
                return Visibility.Visible;
            }
            if (((int)EProjectType.Registration).ToString().Equals(type))
            {
                return Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
