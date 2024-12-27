using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// ListMsgBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ListMsgBox : Window
    {
        private static Dictionary<string, ObservableCollection<ListMsgData>> _list = new Dictionary<string, ObservableCollection<ListMsgData>>();

        internal ListMsgViewModel ViewModle => this.DataContext as ListMsgViewModel;

        internal string Key => ViewModle != null ? ViewModle.Title : string.Empty;

        public ListMsgBox()
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
                    case "Clear":
                        {
                            if (MsgBox.ShowMsg("Do you want to delete all messages?", true) == false) return;

                            this.ViewModle.List.Clear();
                        }
                        break;
                    case "Close":
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

        public static void Show(string title, string message = "")
        {
            var view = ToListMsgBox(title);
            if (view != null)
            {
                if (string.IsNullOrWhiteSpace(message) == false)
                    view.ViewModle.List.Add(message);
            }
            else
            {
                var modle = new ListMsgViewModel(){ Title = title };
                modle.List = GetList(title);

                if (string.IsNullOrWhiteSpace(message) == false)
                    modle.List.Add(message);

                view = new ListMsgBox() { DataContext = modle };
                view.Show();
            }
        }

        public static void Clear(string title)
        {
            var list = GetList(title);
            list.Clear();
        }

        private static ObservableCollection<ListMsgData> GetList(string title)
        {
            if (_list.ContainsKey(title) == false) _list.Add(title, new ObservableCollection<ListMsgData>());

            return _list[title];
        }

        private static ListMsgBox ToListMsgBox(string title)
        {
            return Application.Current.Windows.OfType<Window>()
                .Where(t => t is ListMsgBox)
                .Select(t => t as ListMsgBox).FirstOrDefault(t => t.Key == title);            
        }
    }

    public class ListMsgViewModel : ViewModelBase
    {
        public string Title { get; set; }

        public ObservableCollection<ListMsgData> List { get; set; }

        public NormalCommand OnDeleteCmd => new NormalCommand(DeleteCmd);

        private void DeleteCmd(object param)
        {
            try
            {
                var item = param as ListMsgData;
                if(item == null) return;

                if (MsgBox.ShowMsg("Do you want to delete all messages?", true) == false) return;

                this.List.Remove(item);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public class ListMsgData
    {
        public string Time { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public string Message { get; set; }

        public static implicit operator ListMsgData(string message) => new ListMsgData() { Message = message };
    }
}
