using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using TS.FW.Wpf.Helper;

namespace TS.FW.WpfLanguage
{
    public static class Language
    {
        private readonly static List<LanguageModel> LanguageList = new List<LanguageModel>();
        private readonly static Dictionary<ContentControl, string> ControlList = new Dictionary<ContentControl, string>();
        private readonly static object _ControlLock = new object();

        public static readonly DependencyProperty CodeProperty =
            DependencyProperty.RegisterAttached("Code", typeof(string), typeof(Language), new PropertyMetadata(string.Empty, OnPropertyChanged));

        public static event EventHandler OnLanguageTypeChanged;

        public static enLanguageType LanguageType { get; private set; }

        static Language()
        {

        }

        public static void LoadDatabase()
        {
            try
            {
                using (var biz = new LanguageNTx())
                {
                    var res = biz.GetLanguageList();
                    if (res == false) return;

                    foreach (LanguageModel item in res.Result)
                    {
                        if (item == null) continue;

                        LanguageList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(Language), ex);
            }
        }

        public static void SetLanguageType(enLanguageType type)
        {
            try
            {
                LanguageType = type;

                UpdateControl();

                OnLanguageTypeChanged?.Invoke(typeof(Language), new EventArgs());
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(Language), ex);
            }
        }

        public static string ToLanguage(string code)
        {
            UpsertLanguage(code);

            var item = LanguageList.FirstOrDefault(t => t.Code == code);
            if (item == null) return code;

            var value = string.Empty;

            switch (LanguageType)
            {
                case enLanguageType.English: value = item.English; break;
                case enLanguageType.China: value = item.China; break;
                case enLanguageType.Korea:
                default: value = item.Korea; break;
            }

            if (string.IsNullOrWhiteSpace(value)) return code;

            return value;
        }

        private static void UpdateControl()
        {
            try
            {
                lock (_ControlLock)
                {
                    var list = ControlList.ToList();

                    foreach (var item in list)
                    {
                        item.Key.Content = ToLanguage(item.Value);

                        DoEvent.DoEvents();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(Language), ex);
            }
        }

        public static string GetCode(DependencyObject obj)
        {
            return (string)obj.GetValue(CodeProperty);
        }

        public static void SetCode(DependencyObject obj, string value)
        {
            obj.SetValue(CodeProperty, value);
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) == true) return;

                var cnt = d as ContentControl;
                if (cnt == null) return;

                lock (_ControlLock)
                {
                    if (ControlList.ContainsKey(cnt)) return;

                    var code = e.NewValue.ToString();
                    if (string.IsNullOrWhiteSpace(code)) return;

                    cnt.Content = ToLanguage(code);
                    ControlList.Add(cnt, code);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(Language), ex);
            }
        }

        private static void UpsertLanguage(string code)
        {
            try
            {
                if (ExistsCode(code)) return;

                using (var biz = new LanguageRTx())
                {
                    biz.Insert(new LanguageEntity() { Code = code });
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(Language), ex);
            }
        }

        private static bool ExistsCode(string code)
        {
            using (var biz = new LanguageNTx())
            {
                var res = biz.GetLanguage(code);
                if (res == false) return false;

                return res.Result != null;
            }
        }
    }
}
