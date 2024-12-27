using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TS.FW.Wpf.Core;

namespace TS.FW.Wpf.Controls.Pu
{
    /// <summary>
    /// MsgBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MsgBox : Window
    {
        public MsgBox()
        {
            InitializeComponent();
        }

        private void Label3D_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Released) return;

                this.DragMove();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Button3D_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = sender as Button3D;

                switch (btn.Content as string)
                {
                    case "YES":
                        {
                            this.DialogResult = true;
                        }
                        break;
                    case "NO":
                        {
                            this.DialogResult = false;
                        }
                        break;
                    case "OK":
                        {
                            this.Close();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public static bool? ShowMsg(string message, bool isYesNo = false)
        {
            var model = new MsgBoxModel()
            {
                Message = message,
                Ok = isYesNo == false ? Visibility.Visible : Visibility.Collapsed,
                YesNo = isYesNo == true ? Visibility.Visible : Visibility.Collapsed,
            };

            var view = new MsgBox() { DataContext = model };
            return view.ShowDialog();
        }

        public static bool? ShowMsgInvoke(string message, bool isYesNo = false)
        {
            if(isYesNo)
            {
                return MessageBox.Show(message, "MsgBox", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
            }
            else
            {
                MessageBox.Show(message, "MsgBox", MessageBoxButton.OK);
                return null;
            }

            var color = new LinearGradientBrush()
            {
                EndPoint = new Point(0.5, 1),
                StartPoint = new Point(0.5, 1)
            };

            color.GradientStops.Add(new GradientStop(Color.FromRgb(0x41, 0x53, 0x69), 0));
            color.GradientStops.Add(new GradientStop(Color.FromRgb(0x39, 0x49, 0x5C), 0.5));
            color.GradientStops.Add(new GradientStop(Color.FromRgb(0x29, 0x34, 0x42), 1));

            return Application.Current.Dispatcher.Invoke<bool?>(() =>
            {
                var model = new MsgBoxModel()
                {
                    Message = message,
                    Ok = isYesNo == false ? Visibility.Visible : Visibility.Collapsed,
                    YesNo = isYesNo == true ? Visibility.Visible : Visibility.Collapsed,
                    TitleColor = color,
                };

                var view = new MsgBox() { DataContext = model };
                return view.ShowDialog();
            });
        }
        public static bool? ShowErrMsg(string message, bool isYesNo = false)
        {
            var colorErr = new LinearGradientBrush()
            {
                EndPoint = new Point(0.5, 1),
                StartPoint = new Point(0.5, 1)
            };

            colorErr.GradientStops.Add(new GradientStop(Color.FromRgb(0xFF, 0x00, 0x00), 0));
            colorErr.GradientStops.Add(new GradientStop(Color.FromRgb(0xD9, 0x20, 0x00), 0.5));
            colorErr.GradientStops.Add(new GradientStop(Color.FromRgb(0xC9, 0x34, 0x42), 1));

            var model = new MsgBoxModel()
            {
                Message = message,
                Ok = isYesNo == false ? Visibility.Visible : Visibility.Collapsed,
                YesNo = isYesNo == true ? Visibility.Visible : Visibility.Collapsed,
                TitleColor = colorErr,
            };

            var view = new MsgBox() { DataContext = model };
            return view.ShowDialog();
        }
        public static bool? ShowErrMsgInvoke(string message, bool isYesNo = false)
        {
            if (isYesNo)
            {
                return MessageBox.Show(message, "MsgBox", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
            }
            else
            {
                MessageBox.Show(message, "MsgBox", MessageBoxButton.OK);
                return null;
            }

            var colorErr = new LinearGradientBrush()
            {
                EndPoint = new Point(0.5, 1),
                StartPoint = new Point(0.5, 1)
            };

            colorErr.GradientStops.Add(new GradientStop(Color.FromRgb(0xFF, 0x00, 0x00), 0));
            colorErr.GradientStops.Add(new GradientStop(Color.FromRgb(0xD9, 0x20, 0x00), 0.5));
            colorErr.GradientStops.Add(new GradientStop(Color.FromRgb(0xC9, 0x34, 0x42), 1));

            return Application.Current.Dispatcher.Invoke<bool?>(() =>
            {
                var model = new MsgBoxModel()
                {
                    Message = message,
                    Ok = isYesNo == false ? Visibility.Visible : Visibility.Collapsed,
                    YesNo = isYesNo == true ? Visibility.Visible : Visibility.Collapsed,
                    TitleColor = colorErr,
                };

                var view = new MsgBox() { DataContext = model };
                return view.ShowDialog();
            });
        }

        public static void ShowMsgEx(string message, string name = "")
        {
            var model = new MsgBoxModel()
            {
                Message = message,
                Ok = Visibility.Visible,
                YesNo = Visibility.Collapsed,
            };

            var view = new MsgBox() { Tag = name, DataContext = model };
            view.Show();
        }

        public static void ShowMsgInvokeEx(string message, string name = "")
        {
            MessageBox.Show(message, "MsgBox", MessageBoxButton.OK);
            return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                var model = new MsgBoxModel()
                {
                    Message = message,
                    Ok = Visibility.Visible,
                    YesNo = Visibility.Collapsed,
                };

                var view = new MsgBox() { Tag = name, DataContext = model };
                view.Show();
            });
        }
    }

    internal class MsgBoxModel : ModelBase
    {
        public string Message { get => this.GetValue<string>(); set => this.SetValue(value); }

        public Brush TitleColor { get => this.GetValue<Brush>(); set => this.SetValue(value); }

        public Visibility Ok { get => this.GetValue<Visibility>(); set => this.SetValue(value); }

        public Visibility YesNo { get => this.GetValue<Visibility>(); set => this.SetValue(value); }

        public MsgBoxModel()
        {
            var dColor = new LinearGradientBrush()
            {
                EndPoint = new Point(0.5, 1),
                StartPoint = new Point(0.5, 1)
            };

            dColor.GradientStops.Add(new GradientStop(Color.FromRgb(0x41,0x53,0x69), 0));
            dColor.GradientStops.Add(new GradientStop(Color.FromRgb(0x39, 0x49, 0x5C), 0.5));
            dColor.GradientStops.Add(new GradientStop(Color.FromRgb(0x29, 0x34, 0x42), 1));

            this.TitleColor = dColor;
        }
    }
}
