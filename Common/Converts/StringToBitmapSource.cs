using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Common.Converts
{
    public class StringToBitmapSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string url = value.ToString();
            BitmapImage bim = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
            return bim;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
