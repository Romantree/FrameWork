using System;
using System.Globalization;
using System.Windows.Data;

namespace TS.FW.Wpf.v2.Converter
{
    public class StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((parameter is string) == false) return string.Empty;

            var token = (parameter as string).Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (token.Length <= 1) return string.Empty;

            return ((bool)value) ? token[0] : token[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
