using System;
using System.Windows;
using System.Windows.Controls;
using TS.FW.Wpf.v2.Subscribe;

namespace TS.FW.Wpf.v2.Controls
{
    /// <summary>
    /// MenuList.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuList : ItemsControl
    {
        private static readonly ControlProperty<MenuList> DP = new ControlProperty<MenuList>();

        public static readonly DependencyProperty OrientationProperty = DP.ToProperty(Orientation.Horizontal);
        public static readonly DependencyProperty MenuStyleProperty = DP.ToProperty<Style>();
        public static readonly DependencyProperty SelectedMenuProperty = DP.ToProperty<IPageViewModel>();
        public static readonly DependencyProperty LineProperty = DP.ToProperty(Visibility.Collapsed);

        public Orientation Orientation { get => (Orientation)this.GetValue(OrientationProperty); set => this.SetValue(OrientationProperty, value); }

        public Style MenuStyle { get => (Style)this.GetValue(MenuStyleProperty); set => this.SetValue(MenuStyleProperty, value); }

        public IPageViewModel SelectedMenu { get => (IPageViewModel)this.GetValue(SelectedMenuProperty); set { SelectedMenuChange(SelectedMenu, value); this.SetValue(SelectedMenuProperty, value); } }

        public Visibility Line { get => (Visibility)this.GetValue(LineProperty); set => this.SetValue(LineProperty, value); }

        public MenuList()
        {
            InitializeComponent();
        }

        private void EventBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cnt = sender as EventBtn;
                if (cnt == null) return;

                var model = cnt.DataContext as IPageViewModel;
                if (model == null) return;

                this.SelectedMenu = model;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void SelectedMenuChange(IPageViewModel oldValue, IPageViewModel newValue)
        {
            try
            {
                if (oldValue == newValue) return;

                if (oldValue != null) oldValue.Hide();
                if (newValue != null) newValue.Show();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
