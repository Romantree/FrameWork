using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TS.FW.Wpf.v2.Core;

namespace TS.FW.Wpf.v2.Controls.InPut
{
    /// <summary>
    /// KeyPad.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class KeyPad : Window
    {
        public static double Scale { get; set; } = 1;

        private readonly KeyPadCheck _checker;

        private KeyPadModel _View => this.DataContext as KeyPadModel;

        public KeyPad(KeyPadCheck checker = null)
        {
            InitializeComponent();

            this.Width *= Scale;
            this.Height *= Scale;

            this._checker = checker;
            this.xValue.Title = $"{this._checker}";
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
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
                    this._View.OnCommand.Execute("BACK");
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
                if (this._checker == null || (this._checker.Min == null && this._checker.Max == null))
                {
                    this.DialogResult = true;
                }
                else
                {
                    var check = this._checker.Check(_View.Value);
                    if (check == false)
                    {
                        this.xTitle.Text = $"This is a value that cannot be entered.";
                        return;
                    }

                    this.DialogResult = true;
                }
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

        public static sbyte Show(sbyte old, object min = null, object max = null)
        {
            var chcker = new KeyPadCheck() { Type = typeof(sbyte), Min = min, Max = max };
            var model = new KeyPadModel();
            var view = new KeyPad(chcker) { DataContext = model };

            if (view.ShowDialog() == false) return old;
            if (sbyte.TryParse(model.Value, out sbyte value) == false) return old;

            return value;
        }

        public static short Show(short old, object min = null, object max = null)
        {
            var chcker = new KeyPadCheck() { Type = typeof(short), Min = min, Max = max };
            var model = new KeyPadModel();
            var view = new KeyPad(chcker) { DataContext = model };

            if (view.ShowDialog() == false) return old;
            if (short.TryParse(model.Value, out short value) == false) return old;

            return value;
        }

        public static int Show(int old, object min = null, object max = null)
        {
            var chcker = new KeyPadCheck() { Type = typeof(int), Min = min, Max = max };
            var model = new KeyPadModel();
            var view = new KeyPad(chcker) { DataContext = model };

            if (view.ShowDialog() == false) return old;
            if (int.TryParse(model.Value, out int value) == false) return old;

            return value;
        }

        public static long Show(long old, object min = null, object max = null)
        {
            var chcker = new KeyPadCheck() { Type = typeof(long), Min = min, Max = max };
            var model = new KeyPadModel();
            var view = new KeyPad(chcker) { DataContext = model };

            if (view.ShowDialog() == false) return old;
            if (long.TryParse(model.Value, out long value) == false) return old;

            return value;
        }

        public static byte Show(byte old, object min = null, object max = null)
        {
            var chcker = new KeyPadCheck() { Type = typeof(byte), Min = min, Max = max };
            var model = new KeyPadModel();
            var view = new KeyPad(chcker) { DataContext = model };

            if (view.ShowDialog() == false) return old;
            if (byte.TryParse(model.Value, out byte value) == false) return old;

            return value;
        }

        public static ushort Show(ushort old, object min = null, object max = null)
        {
            var chcker = new KeyPadCheck() { Type = typeof(ushort), Min = min, Max = max };
            var model = new KeyPadModel();
            var view = new KeyPad(chcker) { DataContext = model };

            if (view.ShowDialog() == false) return old;
            if (ushort.TryParse(model.Value, out ushort value) == false) return old;

            return value;
        }

        public static uint Show(uint old, object min = null, object max = null)
        {
            var chcker = new KeyPadCheck() { Type = typeof(uint), Min = min, Max = max };
            var model = new KeyPadModel();
            var view = new KeyPad(chcker) { DataContext = model };

            if (view.ShowDialog() == false) return old;
            if (uint.TryParse(model.Value, out uint value) == false) return old;

            return value;
        }

        public static ulong Show(ulong old, object min = null, object max = null)
        {
            var chcker = new KeyPadCheck() { Type = typeof(ulong), Min = min, Max = max };
            var model = new KeyPadModel();
            var view = new KeyPad(chcker) { DataContext = model };

            if (view.ShowDialog() == false) return old;
            if (ulong.TryParse(model.Value, out ulong value) == false) return old;

            return value;
        }

        public static float Show(float old, object min = null, object max = null)
        {
            var chcker = new KeyPadCheck() { Type = typeof(float), Min = min, Max = max };
            var model = new KeyPadModel();
            var view = new KeyPad(chcker) { DataContext = model };

            if (view.ShowDialog() == false) return old;
            if (float.TryParse(model.Value, out float value) == false) return old;

            return value;
        }

        public static double Show(double old, object min = null, object max = null)
        {
            var chcker = new KeyPadCheck() { Type = typeof(double), Min = min, Max = max };
            var model = new KeyPadModel();
            var view = new KeyPad(chcker) { DataContext = model };

            if (view.ShowDialog() == false) return old;
            if (double.TryParse(model.Value, out double value) == false) return old;

            return value;
        }
    }

    internal class KeyPadModel : IModel
    {
        public string Value { get => this.GetValue<string>(); set => this.SetValue(value); }

        public NormalCommand OnCommand => new NormalCommand(Command);

        public KeyPadModel()
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
                    case "ESC": break;
                    case "BACK":
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

    public class KeyPadCheck
    {
        public Type Type { get; set; }

        public object Min { get; set; }

        public object Max { get; set; }

        public bool Check(string value)
        {
            if (this.Type == typeof(sbyte))
            {
                if (sbyte.TryParse(value, out sbyte result) == false) return false;
                if (sbyte.TryParse($"{Min}", out sbyte min) && result < min) return false;
                if (sbyte.TryParse($"{Max}", out sbyte max) && result > max) return false;

                return true;
            }
            else if (this.Type == typeof(byte))
            {
                if (byte.TryParse(value, out byte result) == false) return false;
                if (byte.TryParse($"{Min}", out byte min) && result < min) return false;
                if (byte.TryParse($"{Max}", out byte max) && result > max) return false;

                return true;
            }

            else if (this.Type == typeof(short))
            {
                if (short.TryParse(value, out short result) == false) return false;
                if (short.TryParse($"{Min}", out short min) && result < min) return false;
                if (short.TryParse($"{Max}", out short max) && result > max) return false;

                return true;
            }
            else if (this.Type == typeof(ushort))
            {
                if (ushort.TryParse(value, out ushort result) == false) return false;
                if (ushort.TryParse($"{Min}", out ushort min) && result < min) return false;
                if (ushort.TryParse($"{Max}", out ushort max) && result > max) return false;

                return true;
            }

            else if (this.Type == typeof(int))
            {
                if (int.TryParse(value, out int result) == false) return false;
                if (int.TryParse($"{Min}", out int min) && result < min) return false;
                if (int.TryParse($"{Max}", out int max) && result > max) return false;

                return true;
            }
            else if (this.Type == typeof(uint))
            {
                if (uint.TryParse(value, out uint result) == false) return false;
                if (uint.TryParse($"{Min}", out uint min) && result < min) return false;
                if (uint.TryParse($"{Max}", out uint max) && result > max) return false;

                return true;
            }

            else if (this.Type == typeof(long))
            {
                if (long.TryParse(value, out long result) == false) return false;
                if (long.TryParse($"{Min}", out long min) && result < min) return false;
                if (long.TryParse($"{Max}", out long max) && result > max) return false;

                return true;
            }
            else if (this.Type == typeof(ulong))
            {
                if (ulong.TryParse(value, out ulong result) == false) return false;
                if (ulong.TryParse($"{Min}", out ulong min) && result < min) return false;
                if (ulong.TryParse($"{Max}", out ulong max) && result > max) return false;

                return true;
            }

            else if (this.Type == typeof(float))
            {
                if (float.TryParse(value, out float result) == false) return false;
                if (float.TryParse($"{Min}", out float min) && result < min) return false;
                if (float.TryParse($"{Max}", out float max) && result > max) return false;

                return true;
            }
            else if (this.Type == typeof(double))
            {
                if (double.TryParse(value, out double result) == false) return false;
                if (double.TryParse($"{Min}", out double min) && result < min) return false;
                if (double.TryParse($"{Max}", out double max) && result > max) return false;

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            if (Min == null && Max == null) return "Value";

            var list = new List<string>();

            if (Min != null)
            {
                list.Add($"Min : {Min}");
            }

            if (Max != null)
            {
                list.Add($"Max : {Max}");
            }

            return string.Join(" ", list);
        }
    }
}
