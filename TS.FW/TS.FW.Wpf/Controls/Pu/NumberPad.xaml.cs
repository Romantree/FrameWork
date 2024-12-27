using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TS.FW.Wpf.Core;

namespace TS.FW.Wpf.Controls.Pu
{
    /// <summary>
    /// NumberPad.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NumberPad : Window
    {
        public static double Scale { get; set; } = 1;

        private NumberPadModel _View => this.DataContext as NumberPadModel;

        public NumberPad()
        {
            InitializeComponent();

            this.Loaded += NumberPad_Loaded;
        }

        private void NumberPad_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Width *= Scale;
                this.Height *= Scale;
                this.FontSize += Scale;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
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

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            try
            {
                var key = e.Key.Equals(Key.ImeProcessed) ? e.ImeProcessedKey : e.Key;

                if (key == Key.Enter)
                {
                    this.OK_Click(this, new RoutedEventArgs());
                }
                else if (key == Key.Escape)
                {
                    this.Cancel_Click(this, new RoutedEventArgs());
                }
                else if (key == Key.Back)
                {
                    this._View.OnCommand.Execute("DELETE");
                }
                else if (key == Key.OemPlus || key == Key.Add)
                {
                    this._View.OnCommand.Execute("+");
                }
                else if (key == Key.OemMinus || key == Key.Subtract)
                {
                    this._View.OnCommand.Execute("-");
                }
                else if (key >= Key.D0 && key <= Key.D9)
                {
                    this._View.OnCommand.Execute(key.ToString().Replace("D", ""));
                }
                else if (key >= Key.NumPad0 && key <= Key.NumPad9)
                {
                    this._View.OnCommand.Execute(key.ToString().Replace("NumPad", ""));
                }
                else if (key == Key.Decimal || key == Key.OemPeriod)
                {
                    this._View.OnCommand.Execute(".");
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnPreviewKeyDown(e);
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public static double Show(double old, string title = "Number Pad")
        {
            try
            {
                var model = new NumberPadModel();
                model.TitleMsg = title;
                var view = new NumberPad() { DataContext = model };

                if (view.ShowDialog() == false) return old;

                return Convert.ToDouble(model.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(NumberPad), ex);
                return old;
            }
        }

        public static float Show(float old)
        {
            try
            {
                var model = new NumberPadModel();
                var view = new NumberPad() { DataContext = model };

                if (view.ShowDialog() == false) return old;

                return Convert.ToSingle(model.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(NumberPad), ex);
                return old;
            }
        }

        public static sbyte Show(sbyte old)
        {
            try
            {
                var model = new NumberPadModel();
                var view = new NumberPad() { DataContext = model };

                if (view.ShowDialog() == false) return old;

                return Convert.ToSByte(model.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(NumberPad), ex);
                return old;
            }
        }

        public static short Show(short old)
        {
            try
            {
                var model = new NumberPadModel();
                var view = new NumberPad() { DataContext = model };

                if (view.ShowDialog() == false) return old;

                return Convert.ToInt16(model.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(NumberPad), ex);
                return old;
            }
        }

        public static int Show(int old)
        {
            try
            {
                var model = new NumberPadModel();
                var view = new NumberPad() { DataContext = model };

                if (view.ShowDialog() == false) return old;

                return Convert.ToInt32(model.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(NumberPad), ex);
                return old;
            }
        }

        public static long Show(long old)
        {
            try
            {
                var model = new NumberPadModel();
                var view = new NumberPad() { DataContext = model };

                if (view.ShowDialog() == false) return old;

                return Convert.ToInt64(model.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(NumberPad), ex);
                return old;
            }
        }

        public static byte Show(byte old)
        {
            try
            {
                var model = new NumberPadModel();
                var view = new NumberPad() { DataContext = model };

                if (view.ShowDialog() == false) return old;

                return Convert.ToByte(model.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(NumberPad), ex);
                return old;
            }
        }

        public static ushort Show(ushort old)
        {
            try
            {
                var model = new NumberPadModel();
                var view = new NumberPad() { DataContext = model };

                if (view.ShowDialog() == false) return old;

                return Convert.ToUInt16(model.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(NumberPad), ex);
                return old;
            }
        }

        public static uint Show(uint old)
        {
            try
            {
                var model = new NumberPadModel();
                var view = new NumberPad() { DataContext = model };

                if (view.ShowDialog() == false) return old;

                return Convert.ToUInt32(model.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(NumberPad), ex);
                return old;
            }
        }

        public static ulong Show(ulong old)
        {
            try
            {
                var model = new NumberPadModel();
                var view = new NumberPad() { DataContext = model };

                if (view.ShowDialog() == false) return old;

                return Convert.ToUInt64(model.Value);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(NumberPad), ex);
                return old;
            }
        }
    }

    internal class NumberPadModel : ModelBase
    {
        public string Value { get => this.GetValue<string>(); set => this.SetValue(value); }

        public string TitleMsg { get => this.GetValue<string>(); set => this.SetValue(value); }

        public NormalCommand OnCommand => new NormalCommand(Command);

        public NumberPadModel()
        {
            this.Value = "0";
        }

        private void Command(object param)
        {
            try
            {
                switch (param as string)
                {
                    case "OK": break;
                    case "CANCEL": break;
                    case "DELETE":
                        {
                            if (this.Value == "0") return;

                            if (this.Value.Length == 1)
                            {
                                this.Value = "0";
                            }
                            else if (this.Value.Length == 2 && this.Value.Contains("-"))
                            {
                                this.Value = "0";
                            }
                            else
                            {
                                this.Value = this.Value.Substring(0, this.Value.Length - 1);
                            }
                        }
                        break;
                    case "+":
                        {
                            this.Value = this.Value.Replace("-", "");
                        }
                        break;
                    case "-":
                        {
                            if (this.Value.Contains("-")) return;

                            this.Value = "-" + this.Value;
                        }
                        break;
                    case ".":
                        {
                            this.Value = this.Value + ".";
                        }
                        break;
                    default:
                        {
                            var value = Convert.ToInt32(param);

                            if (value == 0 && this.Value == "0") return;

                            if (this.Value == "0")
                            {
                                this.Value = value.ToString();
                            }
                            else if (this.Value == "-0")
                            {
                                this.Value = "-" + value.ToString();
                            }
                            else
                            {
                                this.Value += value.ToString();
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
