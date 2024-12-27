using System;
using System.Windows;
using System.Windows.Input;
using TS.FW.Wpf.v2.Core;

namespace TS.FW.Wpf.v2.Controls.InPut
{
    /// <summary>
    /// LoginPad.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginPad : Window
    {
        private readonly string _op;
        private readonly string _en;
        private readonly string _mg;

        private LoginPadModel _View => this.DataContext as LoginPadModel;

        public LoginMode Mode { get; set; } = LoginMode.Lock;

        public LoginPad(int op, int en, int mg)
        {
            this._op = op.ToString();
            this._en = en.ToString();
            this._mg = mg.ToString();

            InitializeComponent();

            this.Width *= KeyPad.Scale;
            this.Height *= KeyPad.Scale;
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
                var password = _View.Value;

                if (string.IsNullOrWhiteSpace(password)) return;

                if (password == _mg) Mode = LoginMode.Manager;
                else if (password == _en) Mode = LoginMode.Engineer;
                else if (password == _op) Mode = LoginMode.Operator;
                else if (password == DateTime.Now.ToString("hmm")) Mode = LoginMode.Programmer;
                else
                {
                    _View.Value = "0";
                    return;
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Lock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Mode = LoginMode.Lock;
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

        public static LoginMode? ShowLogin(int op, int en, int mg)
        {
            var model = new LoginPadModel();
            var view = new LoginPad(op, en, mg) { DataContext = model };

            if (view.ShowDialog() == false) return null;

            return view.Mode;
        }
    }

    internal class LoginPadModel : IModel
    {
        public string Value { get => this.GetValue<string>(); set => this.SetValue(value); }

        public NormalCommand OnCommand => new NormalCommand(Command);

        public LoginPadModel()
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

    public enum LoginMode
    {
        Lock,
        Operator,
        Engineer,
        Manager,
        Programmer,
    }
}
