using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TS.FW.Wpf.Controls;

namespace TS.FW.Wpf.Helper
{
    public static class DataHelper
    {
        class ChageItem
        {
            public string Group { get; set; }

            public object Content { get; set; }

            public Brush Foreground { get; set; }
        }

        private static readonly Dictionary<Button, string> _bList = new Dictionary<Button, string>();
        private static readonly Dictionary<UIElement, ChageItem> _list = new Dictionary<UIElement, ChageItem>();
        private static readonly SolidColorBrush _change = new SolidColorBrush(Colors.Red);

        public static readonly DependencyPropertyDescriptor ContentProperty = DependencyPropertyDescriptor.FromProperty(ContentControl.ContentProperty, typeof(ContentControl));
        public static readonly DependencyPropertyDescriptor ComboBoxTextProperty = DependencyPropertyDescriptor.FromProperty(ComboBox.TextProperty, typeof(ComboBox));
        public static readonly DependencyPropertyDescriptor ComboButton3DContentProperty = DependencyPropertyDescriptor.FromProperty(ComboButton3D.ContentProperty, typeof(ComboButton3D));
        public static readonly DependencyPropertyDescriptor CheckButton3DIsCheckedProperty = DependencyPropertyDescriptor.FromProperty(CheckButton3D.IsCheckedProperty, typeof(CheckButton3D));
        public static readonly DependencyPropertyDescriptor OnOffControlIsCheckedProperty = DependencyPropertyDescriptor.FromProperty(OnOffControl.IsCheckedProperty, typeof(OnOffControl));

        public static readonly DependencyProperty GroupProperty =
            DependencyProperty.RegisterAttached("Group", typeof(string), typeof(DataHelper), new PropertyMetadata(string.Empty, OnPropertyChanged));

        public static readonly DependencyProperty SaveProperty =
            DependencyProperty.RegisterAttached("Save", typeof(string), typeof(DataHelper), new PropertyMetadata(string.Empty, OnButtonPropertyChanged));

        public static string GetGroup(DependencyObject obj)
        {
            return (string)obj.GetValue(GroupProperty);
        }

        public static void SetGroup(DependencyObject obj, string value)
        {
            obj.SetValue(GroupProperty, value);
        }

        public static string GetSave(DependencyObject obj)
        {
            return (string)obj.GetValue(SaveProperty);
        }

        public static void SetSave(DependencyObject obj, string value)
        {
            obj.SetValue(SaveProperty, value);
        }

        public static void Save(string group)
        {
            try
            {
                var list = _list.Where(t => t.Value.Group == group).ToList();

                foreach (var item in list)
                {
                    if (item.Key is ComboButton3D)
                    {
                        item.Value.Content = (item.Key as ComboButton3D).Content;
                    }
                    else if(item.Key is OnOffControl)
                    {
                        item.Value.Content = (item.Key as OnOffControl).IsChecked;
                    }
                    else if (item.Key is CheckButton3D)
                    {
                        item.Value.Content = (item.Key as CheckButton3D).IsChecked;
                    }
                    else if (item.Key is ContentControl)
                    {
                        item.Value.Content = (item.Key as ContentControl).Content;
                    }
                    else if (item.Key is ComboBox)
                    {
                        item.Value.Content = (item.Key as ComboBox).Text;
                    }
                    else
                    {
                        continue;
                    }

                    if (item.Key is ComboButton3D)
                    {
                        var cnt = item.Key as ComboButton3D;

                        if (item.Value.Foreground == null)
                        {
                            item.Value.Foreground = cnt.Foreground;
                        }

                        cnt.Foreground = item.Value.Foreground;
                    }
                    else if (item.Key is OnOffControl)
                    {
                        var cnt = item.Key as OnOffControl;

                        if (item.Value.Foreground == null)
                        {
                            item.Value.Foreground = cnt.Foreground;
                        }

                        cnt.Foreground = item.Value.Foreground;
                    }
                    else if (item.Key is CheckButton3D)
                    {
                        var cnt = item.Key as CheckButton3D;

                        if (item.Value.Foreground == null)
                        {
                            item.Value.Foreground = cnt.Foreground;
                        }

                        cnt.Foreground = item.Value.Foreground;
                    }
                    else if (item.Key is ContentControl)
                    {
                        var cnt = item.Key as ContentControl;

                        if (item.Value.Foreground == null)
                        {
                            item.Value.Foreground = cnt.Foreground;
                        }

                        cnt.Foreground = item.Value.Foreground;
                    }
                    else if (item.Key is Control)
                    {
                        var cnt = item.Key as Control;

                        if (item.Value.Foreground == null)
                        {
                            item.Value.Foreground = cnt.Foreground;
                        }

                        cnt.Foreground = item.Value.Foreground;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataHelper), ex);
            }
        }

        public static void Clear(string group)
        {
            try
            {
                var list = _list.Where(t => t.Value.Group == group).ToList();

                foreach (var item in list)
                {
                    _list.Remove(item.Key);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataHelper), ex);
            }
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (d is ComboButton3D)
                {
                    var cnt = d as ComboButton3D;

                    if (_list.ContainsKey(cnt)) return;

                    if (ComboButton3DContentProperty != null) ComboButton3DContentProperty.AddValueChanged(cnt, OnComboButtonChangedEventHandler);

                    _list.Add(cnt, new ChageItem() { Group = e.NewValue.ToString(), Content = cnt.Content });
                }
                else if(d is OnOffControl)
                {
                    var cnt = d as OnOffControl;

                    if (_list.ContainsKey(cnt)) return;

                    if (OnOffControlIsCheckedProperty != null) OnOffControlIsCheckedProperty.AddValueChanged(cnt, OnOnOffControlChangedEventHandler);

                    _list.Add(cnt, new ChageItem() { Group = e.NewValue.ToString(), Content = cnt.IsChecked });
                }
                else if (d is CheckButton3D)
                {
                    var cnt = d as CheckButton3D;

                    if (_list.ContainsKey(cnt)) return;

                    if (CheckButton3DIsCheckedProperty != null) CheckButton3DIsCheckedProperty.AddValueChanged(cnt, OnCheckButtonChangedEventHandler);

                    _list.Add(cnt, new ChageItem() { Group = e.NewValue.ToString(), Content = cnt.IsChecked });
                }
                else if (d is ContentControl)
                {
                    var cnt = d as ContentControl;

                    if (_list.ContainsKey(cnt)) return;

                    if (ContentProperty != null) ContentProperty.AddValueChanged(cnt, OnContentChangedEventHandler);

                    _list.Add(cnt, new ChageItem() { Group = e.NewValue.ToString(), Content = cnt.Content });
                }
                else if (d is ComboBox)
                {
                    var cnt = d as ComboBox;

                    if (_list.ContainsKey(cnt)) return;

                    if (ComboBoxTextProperty != null) ComboBoxTextProperty.AddValueChanged(cnt, OnComboBoxTextChangedEventHandler);

                    _list.Add(cnt, new ChageItem() { Group = e.NewValue.ToString(), Content = cnt.Text });
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataHelper), ex);
            }
        }

        private static void OnButtonPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (d is Button == false) return;

                var cnt = d as Button;

                if (_bList.ContainsKey(cnt)) return;

                cnt.Click += SaveButton_Click;

                _bList.Add(cnt, e.NewValue.ToString());
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataHelper), ex);
            }
        }

        private static void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cnt = sender as Button;
                if (_bList.ContainsKey(cnt) == false) return;

                var group = _bList[cnt];

                Save(group);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataHelper), ex);
            }
        }

        private static void OnContentChangedEventHandler(object sender, EventArgs e)
        {
            try
            {
                var cnt = sender as ContentControl;
                if (_list.ContainsKey(cnt) == false) return;

                var item = _list[cnt];

                if (Equals(item.Content, null))
                {
                    item.Content = cnt.Content;
                    item.Foreground = cnt.Foreground;
                }

                SetForeground(cnt, cnt.Content, item);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataHelper), ex);
            }
        }

        private static void OnComboButtonChangedEventHandler(object sender, EventArgs e)
        {
            try
            {
                var cnt = sender as ComboButton3D;
                if (_list.ContainsKey(cnt) == false) return;

                var item = _list[cnt];

                if (Equals(item.Content, null))
                {
                    item.Content = cnt.Content;
                    item.Foreground = cnt.Foreground;
                }

                SetForeground(cnt, cnt.Content, item);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataHelper), ex);
            }
        }

        private static void OnCheckButtonChangedEventHandler(object sender, EventArgs e)
        {
            try
            {
                var cnt = sender as CheckButton3D;
                if (_list.ContainsKey(cnt) == false) return;

                var item = _list[cnt];

                if (Equals(item.Content, null))
                {
                    item.Content = cnt.IsChecked;
                    item.Foreground = cnt.Foreground;
                }

                SetForeground(cnt, cnt.IsChecked, item);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataHelper), ex);
            }
        }

        private static void OnOnOffControlChangedEventHandler(object sender, EventArgs e)
        {
            try
            {
                var cnt = sender as OnOffControl;
                if (_list.ContainsKey(cnt) == false) return;

                var item = _list[cnt];

                if (Equals(item.Content, null))
                {
                    item.Content = cnt.IsChecked;
                    item.Foreground = cnt.Foreground;
                }

                SetForeground(cnt, cnt.IsChecked, item);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataHelper), ex);
            }
        }

        private static void OnComboBoxTextChangedEventHandler(object sender, EventArgs e)
        {
            try
            {
                var cnt = sender as ComboBox;
                if (_list.ContainsKey(cnt) == false) return;

                var item = _list[cnt];

                if (Equals(item.Content, ""))
                {
                    item.Content = cnt.Text;
                    item.Foreground = cnt.Foreground;
                }

                SetForeground(cnt, cnt.Text, item);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataHelper), ex);
            }
        }

        private static void SetForeground(Control cnt, object newValue, ChageItem item)
        {
            try
            {
                if (Equals(newValue, item.Content))
                {
                    if (item.Foreground == null) item.Foreground = cnt.Foreground;

                    cnt.Foreground = item.Foreground;
                }
                else
                {
                    if (item.Foreground == null) item.Foreground = cnt.Foreground;

                    cnt.Foreground = _change;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataHelper), ex);
            }
        }

        private static void SetForeground(ComboButton3D cnt, object newValue, ChageItem item)
        {
            try
            {
                if (Equals(newValue, item.Content))
                {
                    if (item.Foreground == null) item.Foreground = cnt.Foreground;

                    cnt.Foreground = item.Foreground;
                }
                else
                {
                    if (item.Foreground == null) item.Foreground = cnt.Foreground;

                    cnt.Foreground = _change;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataHelper), ex);
            }
        }

        private new static bool Equals(object a, object b)
        {
            if (object.Equals(a, null) && object.Equals(b, null)) return true;
            if (object.Equals(a, null)) return false;
            if (object.Equals(b, null)) return false;

            return a.GetHashCode() == b.GetHashCode();
        }

        public static T DeppCopy<T>(this T item)
        {
            var json = TS.FW.Serialization.Serialization.JsonSerializer(item);
            if (json == false)
            {
                Logger.Write(typeof(DataHelper), json.Comment, Logger.LogEventLevel.Error);
                return default(T);
            }

            var res = TS.FW.Serialization.Serialization.JsonDeserializer<T>(json.Result);
            if (res == false)
            {
                Logger.Write(typeof(DataHelper), res.Comment, Logger.LogEventLevel.Error);
                return default(T);
            }

            return res.Result;
        }

        public static bool Check<T>(this T itemA, T itemB)
        {
            var aJson = TS.FW.Serialization.Serialization.JsonSerializer(itemA);
            var bJson = TS.FW.Serialization.Serialization.JsonSerializer(itemB);

            if (aJson == false || bJson == false) return false;

            return aJson.Result != bJson.Result;

        }
    }
}
