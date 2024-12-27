using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace TS.FW.Wpf.Converters
{
    public class OnOffColorConverter : IValueConverter
    {
        public readonly static Dictionary<string, OnOffColor> List = new Dictionary<string, OnOffColor>();

        private readonly static SolidColorBrush UNKNOWN = new SolidColorBrush(Colors.Transparent);
        private readonly static SolidColorBrush DEFAULT_ON = new SolidColorBrush(Colors.Lime);
        private readonly static SolidColorBrush DEFAULT_OFF = new SolidColorBrush(Colors.Red);

        static OnOffColorConverter()
        {
            InitColorList();
        }

        public static void InitColorList(params OnOffColor[] list)
        {
            try
            {
                var temp = new List<OnOffColor>();
                if (list != null && list.Length > 0) temp.AddRange(list);

                temp.AddRange(ToInitList());

                InitColorList(temp);

            }
            catch (Exception ex)
            {
                Logger.Write(typeof(OnOffColorConverter), ex);
            }
        }

        public static IEnumerable<OnOffColor> ToInitList()
        {
            yield return OnOffColor.ToSolidColor("MENU", Colors.Lime, Colors.WhiteSmoke);
            yield return OnOffColor.ToSolidColor("SELECT", Colors.Gray, Color.FromArgb(0xFF, 0x64, 0x64, 0x64));
            yield return OnOffColor.ToRadialColor("INOUT", Colors.Lime, Colors.LimeGreen, Colors.Orange, Colors.OrangeRed);
            yield return OnOffColor.ToRadialColor("MOT", Colors.Lime, Colors.LimeGreen, Colors.LightGray, Colors.Gray);
            yield return OnOffColor.ToLinearColor("USED", Colors.Lime, Colors.LimeGreen, Colors.LightGray, Colors.Gray);
            yield return OnOffColor.ToSolidColor("OUTPUT", Colors.DimGray, Colors.LightSlateGray);
            yield return OnOffColor.ToSolidColor("OUTPUT_F", Colors.WhiteSmoke, Colors.Navy);
            yield return OnOffColor.ToSolidColor("LAMP", Colors.Lime, Colors.LightGray);
            yield return OnOffColor.ToRadialColor("ALARM_2", Colors.Orange, Colors.OrangeRed, Colors.LightGray, Colors.Gray);
            yield return OnOffColor.ToSolidColor("ALARM", Colors.Red, Colors.LightGray);
            yield return OnOffColor.ToSolidColor("LIMIT", Colors.Red, Colors.LimeGreen);
            yield return OnOffColor.ToSolidColor("ENABLE", Colors.Green, Colors.DimGray);
            yield return OnOffColor.ToSolidColor("SERVO", Colors.WhiteSmoke, Colors.Yellow);

            yield return OnOffColor.ToSolidColor("RED", Colors.Red, Colors.LightGray);
            yield return OnOffColor.ToSolidColor("YELLOW", Colors.Yellow, Colors.LightGray);
            yield return OnOffColor.ToSolidColor("GREEN", Colors.Green, Colors.LightGray);


            yield return OnOffColor.ToSolidColor("WAFER", Colors.Blue, Colors.White);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((parameter is string) == false) return ToColor(value, DEFAULT_ON, DEFAULT_OFF);

            var key = (parameter as string).ToUpper();
            if (string.IsNullOrWhiteSpace(key) || List.ContainsKey(key) == false) return ToColor(value, DEFAULT_ON, DEFAULT_OFF);

            return ToColor(value, List[key].OnColor, List[key].OffColor);
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

        private static void InitColorList(IEnumerable<OnOffColor> list)
        {
            try
            {
                if (list == null || list.Count() <= 0) return;

                List.Clear();

                foreach (var item in list)
                {
                    if (List.ContainsKey(item.Key)) continue;

                    var key = item.Key.ToUpper();

                    List.Add(key, item);
                    List.Add(key + "_R", item.ToReverse());
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(OnOffColorConverter), ex);
            }
        }
    }

    public class OnOffColor
    {
        public string Key { get; set; }

        public Brush OnColor { get; set; }

        public Brush OffColor { get; set; }

        public OnOffColor(string key)
        {
            this.Key = key;
            this.OnColor = new SolidColorBrush(Colors.Lime);
            this.OffColor = new SolidColorBrush(Colors.Red);
        }

        public OnOffColor ToReverse()
        {
            return new OnOffColor(this.Key + "_R")
            {
                OnColor = OffColor,
                OffColor = OnColor,
            };
        }

        public static OnOffColor ToSolidColor(string key, Color on, Color off)
        {
            return new OnOffColor(key)
            {
                OnColor = new SolidColorBrush(on),
                OffColor = new SolidColorBrush(off),
            };
        }

        public static OnOffColor ToRadialColor(string key, Color onStart, Color onEnd, Color offStart, Color offEnd)
        {
            return new OnOffColor(key)
            {
                OnColor = ToRadialGradientBrush(onStart, onEnd),
                OffColor = ToRadialGradientBrush(offStart, offEnd),
            };
        }

        public static OnOffColor ToLinearColor(string key, Color onStart, Color onEnd, Color offStart, Color offEnd)
        {
            return new OnOffColor(key)
            {
                OnColor = ToLinearGradientBrush(onStart, onEnd),
                OffColor = ToLinearGradientBrush(offStart, offEnd),
            };
        }

        public static RadialGradientBrush ToRadialGradientBrush(Color start, Color end)
        {
            return new RadialGradientBrush()
            {
                Transform = new ScaleTransform(1, 0.65),
                GradientStops = new GradientStopCollection(new GradientStop[]
                {
                    new GradientStop(start,0.5),
                    new GradientStop(end,1),
                }),
            };
        }

        public static LinearGradientBrush ToLinearGradientBrush(Color start, Color end)
        {
            return new LinearGradientBrush()
            {
                Transform = new RotateTransform(-5, 0, 0),
                GradientStops = new GradientStopCollection(new GradientStop[]
                {
                    new GradientStop(start,-0.4),
                    new GradientStop(end,1),
                }),
            };
        }
    }
}
