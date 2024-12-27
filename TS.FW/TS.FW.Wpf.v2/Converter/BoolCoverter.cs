using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TS.FW.Wpf.v2.Converter
{
    public  class BoolCoverter : IValueConverter
    {
        public eBoolType Type { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (Type)
            {
                case eBoolType.Reverse: return ((bool)value) == false;
                case eBoolType.Visibility: return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
                case eBoolType.Visibility_R: return ((bool)value) ? Visibility.Collapsed : Visibility.Visible;
                case eBoolType.NullValue: return value != null;
                case eBoolType.NullValue_R: return value == null;
                case eBoolType.NullVisibility: return value != null ? Visibility.Visible : Visibility.Collapsed;
                case eBoolType.NullVisibility_R: return value != null ? Visibility.Collapsed : Visibility.Visible;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public enum eBoolType
    {
        Reverse,
        Visibility,
        Visibility_R,
        NullValue,
        NullValue_R,
        NullVisibility,
        NullVisibility_R,
    }
}
