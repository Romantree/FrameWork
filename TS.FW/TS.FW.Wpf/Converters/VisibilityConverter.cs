using System;
using System.Windows;
using System.Windows.Data;

namespace TS.FW.Wpf.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isVisible = System.Convert.ToBoolean(value);
            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isVisible = (Visibility)value;
            return isVisible == Visibility.Visible;
        }
    }

    public class HVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isVisible = System.Convert.ToBoolean(value);
            return isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isVisible = (Visibility)value;
            return isVisible == Visibility.Visible;
        }
    }

    public class VisibilityReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isVisible = System.Convert.ToBoolean(value);
            return isVisible ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isVisible = (Visibility)value;
            return isVisible == Visibility.Collapsed;
        }
    }

    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return "0.0000";
            var getdata = 0.0;
            if(double.TryParse(value.ToString(), out getdata))
            {
                return getdata.ToString("f4");
            }
            return "0.0000";
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack should never be called");
        }
    }

    public class ToleranceUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string ps = "(±)" + double.Parse(value.ToString());
            return ps;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack should never be called");
        }
    }
    
}
