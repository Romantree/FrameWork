using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Media;
using TS.FW.Wpf.v2.Core;
using TS.FW.Wpf.v2.Helpers;

namespace TS.FW.Wpf.v2.Converter
{
    public class ColorConverter : IValueConverter
    {
        public static readonly Dictionary<string, TsColorData> LIST = new Dictionary<string, TsColorData>();

        private readonly static SolidColorBrush DEFAULT_ON = new SolidColorBrush(Colors.Lime);
        private readonly static SolidColorBrush DEFAULT_OFF = new SolidColorBrush(Colors.Red);

        public static Brush ON_3D { get; private set; }

        public static Brush OFF_3D { get; private set; }

        static ColorConverter()
        {
            ON_3D = ToRadialGradientBrush(Colors.White, Colors.Green, ToColor("FF066506"));
            OFF_3D = ToRadialGradientBrush(Colors.White, Colors.Red, ToColor("FFD80C0C"));

            if (IViewModel.IsDesignMode) return;

            foreach (DictionaryEntry item in ResourceHelper.Ins.GetDictionary("ColorData"))
            {
                if ((item.Key is string) == false ||  (item.Value is TsColorData) == false) continue;

                var data = item.Value as TsColorData;

                LIST.Add($"{item.Key as string}".ToUpper(), data);
                LIST.Add($"{item.Key as string}_R".ToUpper(), new TsColorData() { On = data.Off, Off = data.On });
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (IViewModel.IsDesignMode) return ToColor(value, DEFAULT_ON, DEFAULT_OFF);

            if ((parameter is string) == false) return ToColor(value, DEFAULT_ON, DEFAULT_OFF);

            var key = (parameter as string).ToUpper();
            if (string.IsNullOrWhiteSpace(key) || LIST.ContainsKey(key) == false) return ToColor(value, DEFAULT_ON, DEFAULT_OFF);

            return ToColor(value, LIST[key].On, LIST[key].Off);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private Brush ToColor(object value, Brush on, Brush off)
        {
            if (value == null) return off;
            if (value is bool == false) return off;

            return (bool)value ? on : off;
        }

        public static RadialGradientBrush ToRadialGradientBrush(Color start, Color end, Color stop)
        {
            var bursh = new RadialGradientBrush();
            bursh.RelativeTransform = new TranslateTransform(0.1, -0.1);

            bursh.GradientStops.Add(new GradientStop(start, 0));
            bursh.GradientStops.Add(new GradientStop(end, 0.7));
            bursh.GradientStops.Add(new GradientStop(stop, 0.94));

            return bursh;
        }

        public static Color ToColor(string color)
        {
            if (color.Length != 8) return Colors.Transparent;

            var a = System.Convert.ToByte(color.Substring(0,2),16);
            var r = System.Convert.ToByte(color.Substring(2,2),16);
            var g = System.Convert.ToByte(color.Substring(4,2),16);
            var b = System.Convert.ToByte(color.Substring(6,2),16);

            return Color.FromArgb(a, r, g, b);
        }
    }

    public class TsColorData
    {
        public Brush On { get; set; }

        public Brush Off { get; set; }
    }
}
