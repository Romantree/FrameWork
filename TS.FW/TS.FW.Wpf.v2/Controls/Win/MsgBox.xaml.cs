using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace TS.FW.Wpf.v2.Controls.Win
{
    /// <summary>
    /// MsgBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MsgBox : Window
    {
        private static Func<string, MsgBoxType, bool?> xHandler;

        static MsgBox()
        {
            xHandler = new Func<string, MsgBoxType, bool?>(ShowInvoke);
        }

        public MsgBox()
        {
            InitializeComponent();
        }

        private void xRoot_MouseMove(object sender, MouseEventArgs e)
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

        private void EventBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cnt = sender as EventBtn;
                if (cnt == null) return;

                switch (cnt.Content as string)
                {
                    case "YES":
                    case "OK":
                        {
                            this.DialogResult = true;
                        }
                        break;
                    case "NO":
                    case "CANCEL":
                        {
                            this.DialogResult = false;
                        }
                        break;
                    case "CLOSE":
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

        public static bool? Show(string msg, MsgBoxType type = MsgBoxType.CLOSE)
        {
            return (bool?)Application.Current.Dispatcher.Invoke(xHandler, msg, type);
        }

        private static bool? ShowInvoke(string msg, MsgBoxType type)
        {
            var view = new MsgBox();
            view.xMsg.Text = msg;

            switch (type)
            {
                case MsgBoxType.CLOSE:
                    {
                        view.xYes.Visibility = Visibility.Hidden;
                        view.xNo.Content = "CLOSE";
                    }
                    break;
                case MsgBoxType.OK_CANCEL:
                    {
                        view.xYes.Content = "OK";
                        view.xNo.Content = "CANCEL";
                    }
                    break;
            }

            return view.ShowDialog();
        }
    }

    public enum MsgBoxType
    {
        CLOSE,
        YES_NO,
        OK_CANCEL,
    }
}
