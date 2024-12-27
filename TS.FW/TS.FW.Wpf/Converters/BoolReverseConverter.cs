using System;
using System.Globalization;
using System.Windows.Data;

namespace TS.FW.Wpf.Converters
{
    public class BoolReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isBool = System.Convert.ToBoolean(value);
            return !isBool;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isBool = System.Convert.ToBoolean(value);
            return !isBool;
        }
    }
}
