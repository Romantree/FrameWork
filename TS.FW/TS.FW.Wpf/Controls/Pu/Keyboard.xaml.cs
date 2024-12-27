using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TS.FW.Wpf.Core;
using TS.FW.Wpf.Helper;
using TS.FW.Wpf.Helper.Data;

namespace TS.FW.Wpf.Controls.Pu
{
    /// <summary>
    /// Keyboard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Keyboard : Window
    {
        public static double Scale { get; set; } = 1;

        private bool isShift = false;

        private KeyboardModel _View => this.DataContext as KeyboardModel;

        public Keyboard()
        {
            InitializeComponent();

            this.Loaded += Keyboard_Loaded;
        }

        private void Keyboard_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Width *= Scale;
                this.Height *= Scale;
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
                if (this._View.IsReadOnly == false) return;

                var key = e.Key.Equals(Key.ImeProcessed) ? e.ImeProcessedKey : e.Key;

                if ((System.Windows.Input.Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                {
                    if (key == Key.C)
                    {
                        var text = this._View.Text;

                        Clipboard.SetText(text);
                    }
                    else if (key == Key.V)
                    {
                        var text = Clipboard.GetText();

                        this._View.SetTextCommand(text);

                    }
                }
                else if (key == Key.Enter)
                {
                    this.OK_Click(this, new RoutedEventArgs());
                }
                else if (key == Key.Escape)
                {
                    this.Cancel_Click(this, new RoutedEventArgs());
                }
                else if (key == Key.Space)
                {
                    this._View.SpaceCommand(null);
                }
                else if (key == Key.Back)
                {
                    this._View.BackSpaceCommand(null);
                }
                else if (key == Key.LeftShift || key == Key.RightShift)
                {
                    if (this.isShift == true) return;

                    this._View.ShiftCommand(null);

                    this.isShift = true;
                }
                else if (key == Key.HangulMode)
                {
                    this._View.LanguageChangeCommand(null);
                }
                else if (key >= Key.A && key <= Key.Z)
                {
                    this._View.SetKeyComand(key);
                }
                else if (key >= Key.NumPad0 && key <= Key.NumPad9)
                {
                    this._View.SetTextCommand(key.ToString().Replace("NumPad", ""));
                }
                else if (key == Key.Decimal)
                {
                    this._View.SetTextCommand(".");
                }
                else if (key == Key.Divide)
                {
                    this._View.SetTextCommand("/");
                }
                else if (key == Key.Multiply)
                {
                    this._View.SetTextCommand("*");
                }
                else if (key == Key.Subtract)
                {
                    this._View.SetTextCommand("-");
                }
                else if (key == Key.Add)
                {
                    this._View.SetTextCommand("+");
                }
                else if (key == Key.Oem3)
                {
                    this._View.SetCodeCommand("`");
                }
                else if (key >= Key.D0 && key <= Key.D9)
                {
                    this._View.SetCodeCommand(key.ToString().Replace("D", ""));
                }
                else if (key == Key.OemMinus)
                {
                    this._View.SetCodeCommand("-");
                }
                else if (key == Key.OemPlus)
                {
                    this._View.SetCodeCommand("=");
                }
                else if (key == Key.OemOpenBrackets)
                {
                    this._View.SetCodeCommand("[");
                }
                else if (key == Key.Oem6)
                {
                    this._View.SetCodeCommand("]");
                }
                else if (key == Key.Oem5)
                {
                    this._View.SetCodeCommand(@"\");
                }
                else if (key == Key.Oem1)
                {
                    this._View.SetCodeCommand(";");
                }
                else if (key == Key.OemQuotes)
                {
                    this._View.SetCodeCommand("'");
                }
                else if (key == Key.OemComma)
                {
                    this._View.SetCodeCommand(",");
                }
                else if (key == Key.OemPeriod)
                {
                    this._View.SetCodeCommand(".");
                }
                else if (key == Key.OemQuestion)
                {
                    this._View.SetCodeCommand("/");
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

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            try
            {
                if (this._View.IsReadOnly == false) return;

                var key = e.Key.Equals(Key.ImeProcessed) ? e.ImeProcessedKey : e.Key;

                if (key == Key.LeftShift || key == Key.RightShift)
                {
                    this._View.ShiftCommand(null);

                    this.isShift = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnPreviewKeyUp(e);
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

        public static string Show(string old = "", bool keyEvent = true)
        {
            try
            {
                var model = new KeyboardModel();
                model.IsReadOnly = keyEvent;

                if (string.IsNullOrEmpty(old))
                    old = string.Empty;

                foreach (var item in old)
                {
                    model.SetTextCommand(item);
                }

                var view = new Keyboard() { DataContext = model };

                if (view.ShowDialog() == false) return old;

                return model.Text;
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(NumberPad), ex);
                return old;
            }
        }
    }

    internal class KeyboardModel : ModelBase
    {
        private KeyboardHelper _helper = new KeyboardHelper();

        public string Text { get => this.GetValue<string>(); set => this.SetValue(value); }

        public bool IsReadOnly { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public KeyCodeModel[] Code { get => this.GetValue<KeyCodeModel[]>(); set => this.SetValue(value); }

        public NormalCommand OnCommand => new NormalCommand(Command);

        public NormalCommand OnSpaceCommand => new NormalCommand(this.SpaceCommand);

        public NormalCommand OnBackSpaceCommand => new NormalCommand(this.BackSpaceCommand);

        public NormalCommand OnShiftCommand => new NormalCommand(this.ShiftCommand);

        public NormalCommand OnLanguageChangeCommand => new NormalCommand(this.LanguageChangeCommand);

        public KeyboardModel()
        {
            this._helper.OnTextChanged += _helper_OnTextChanged;
            this.Code = this.InitKeyboardItem().ToArray();

            this.Text = string.Empty;
        }

        private void _helper_OnTextChanged(object sender, string e)
        {
            try
            {
                this.Text = e;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Command(object commandParameter)
        {
            try
            {
                var contents = (commandParameter as string)[0];

                this._helper.SetContents(contents);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetKeyComand(Key key)
        {
            try
            {
                var model = this.Code.Where(t => t is KeyCodeEnglishModel).Select(t => t as KeyCodeEnglishModel).FirstOrDefault(t => t.English == key.ToString().ToLower());
                if (model == null) return;

                this._helper.SetContents(model.Contents[0]);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetTextCommand(object param)
        {
            try
            {
                this._helper.SetContents(param.ToString());
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SetCodeCommand(object param)
        {
            try
            {
                var code = param as string;
                var model = this.Code.FirstOrDefault(t => t.Korea == code);
                if (model == null) return;

                this._helper.SetContents(model.Contents);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void SpaceCommand(object param)
        {
            try
            {
                this._helper.SetSpace();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void BackSpaceCommand(object param)
        {
            try
            {
                this._helper.SetBackSpace();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void ShiftCommand(object param)
        {
            try
            {
                foreach (var item in this.Code)
                {
                    item.IsShift = !item.IsShift;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        public void LanguageChangeCommand(object param)
        {
            try
            {
                foreach (var item in this.Code.Where(t => t is KeyCodeEnglishModel).Select(t => t as KeyCodeEnglishModel).ToList())
                {
                    item.IsKorea = !item.IsKorea;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private IEnumerable<KeyCodeModel> InitKeyboardItem()
        {
            yield return new KeyCodeModel("`", "~");
            yield return new KeyCodeModel("1", "!");
            yield return new KeyCodeModel("2", "@");
            yield return new KeyCodeModel("3", "#");
            yield return new KeyCodeModel("4", "$");
            yield return new KeyCodeModel("5", "%");
            yield return new KeyCodeModel("6", "^");
            yield return new KeyCodeModel("7", "&");
            yield return new KeyCodeModel("8", "*");
            yield return new KeyCodeModel("9", "(");
            yield return new KeyCodeModel("0", ")");
            yield return new KeyCodeModel("-", "_");
            yield return new KeyCodeModel("=", "+");

            yield return new KeyCodeEnglishModel("ㅂ", "ㅃ", "q");
            yield return new KeyCodeEnglishModel("ㅈ", "ㅉ", "w");
            yield return new KeyCodeEnglishModel("ㄷ", "ㄸ", "e");
            yield return new KeyCodeEnglishModel("ㄱ", "ㄲ", "r");
            yield return new KeyCodeEnglishModel("ㅅ", "ㅆ", "t");
            yield return new KeyCodeEnglishModel("ㅛ", "ㅛ", "y");
            yield return new KeyCodeEnglishModel("ㅕ", "ㅕ", "u");
            yield return new KeyCodeEnglishModel("ㅑ", "ㅑ", "i");
            yield return new KeyCodeEnglishModel("ㅐ", "ㅒ", "o");
            yield return new KeyCodeEnglishModel("ㅔ", "ㅖ", "p");
            yield return new KeyCodeModel("[", "{");
            yield return new KeyCodeModel("]", "}");

            yield return new KeyCodeEnglishModel("ㅁ", "ㅁ", "a");
            yield return new KeyCodeEnglishModel("ㄴ", "ㄴ", "s");
            yield return new KeyCodeEnglishModel("ㅇ", "ㅇ", "d");
            yield return new KeyCodeEnglishModel("ㄹ", "ㄹ", "f");
            yield return new KeyCodeEnglishModel("ㅎ", "ㅎ", "g");
            yield return new KeyCodeEnglishModel("ㅗ", "ㅗ", "h");
            yield return new KeyCodeEnglishModel("ㅓ", "ㅓ", "j");
            yield return new KeyCodeEnglishModel("ㅏ", "ㅏ", "k");
            yield return new KeyCodeEnglishModel("ㅣ", "ㅣ", "l");
            yield return new KeyCodeModel(";", ":");
            yield return new KeyCodeModel("'", "\"");
            yield return new KeyCodeModel(@"\", "|");

            yield return new KeyCodeEnglishModel("ㅋ", "ㅋ", "z");
            yield return new KeyCodeEnglishModel("ㅌ", "ㅌ", "x");
            yield return new KeyCodeEnglishModel("ㅊ", "ㅊ", "c");
            yield return new KeyCodeEnglishModel("ㅍ", "ㅍ", "v");
            yield return new KeyCodeEnglishModel("ㅠ", "ㅠ", "b");
            yield return new KeyCodeEnglishModel("ㅜ", "ㅜ", "n");
            yield return new KeyCodeEnglishModel("ㅡ", "ㅡ", "m");
            yield return new KeyCodeModel(",", "<");
            yield return new KeyCodeModel(".", ">");
            yield return new KeyCodeModel("/", "?");
        }
    }
}
