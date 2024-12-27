using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TS.FW.Wpf.Core;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// MenuView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuView : ItemsControl
    {
        private static ControlProperty<MenuView> Property = new ControlProperty<MenuView>();

        public new static readonly DependencyProperty ItemsSourceProperty = Property.ToProperty("ItemsSource", typeof(IEnumerable<IMenuViewModel>), null);

        public new IEnumerable<IMenuViewModel> ItemsSource { get => (IEnumerable<IMenuViewModel>)this.GetValue(ItemsSourceProperty); set => this.SetValue(ItemsSourceProperty, value); }

        public MenuView()
        {
            InitializeComponent();
        }
    }
}
