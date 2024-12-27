using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TS.FW.Wpf.Core;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// MenuList.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuList : ItemsControl
    {
        private static ControlProperty<MenuList> Property = new ControlProperty<MenuList>();

        public new static readonly DependencyProperty ItemsSourceProperty = Property.ToProperty("ItemsSource", typeof(IEnumerable<IMenuViewModel>), null);

        public static readonly DependencyProperty SelectedMenuProperty = Property.ToProperty("SelectedMenu", typeof(IMenuViewModel), null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedMenuProeprtyChanged);
        public static readonly DependencyProperty OrientationProperty = Property.ToProperty("Orientation", typeof(Orientation), Orientation.Vertical);

        public static readonly DependencyProperty MenuStyleProperty = Property.ToProperty("MenuStyle", typeof(Style), new Style(typeof(ToggleButton3D)));

        public new IEnumerable<IMenuViewModel> ItemsSource { get => (IEnumerable<IMenuViewModel>)this.GetValue(ItemsSourceProperty); set => this.SetValue(ItemsSourceProperty, value); }

        public IMenuViewModel SelectedMenu { get => (IMenuViewModel)this.GetValue(SelectedMenuProperty); set => this.SetValue(SelectedMenuProperty, value); }

        public Orientation Orientation { get => (Orientation)this.GetValue(OrientationProperty); set => this.SetValue(OrientationProperty, value); }

        public Style MenuStyle { get => (Style)this.GetValue(MenuStyleProperty); set => this.SetValue(MenuStyleProperty, value); }

        public MenuList()
        {
            InitializeComponent();
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var cnt = sender as ToggleButton3D;
                if (cnt == null) return;

                var model = cnt.DataContext as IMenuViewModel;
                if (model == null) return;

                if (model == this.SelectedMenu)
                {
                    this.SelectedMenu.IsSelect = true;
                    return;
                }

                this.SelectedMenu = model;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private static void OnSelectedMenuProeprtyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                var control = d as MenuList;
                if (control == null) return;

                var oItem = e.OldValue as IMenuViewModel;
                var nItem = e.NewValue as IMenuViewModel;

                if (oItem != null) oItem.Hide();
                if (nItem != null) nItem.Show();

                control.SetValue(e.Property, e.NewValue);

                if (CommandLog.IsTracking == false) return;

                if (oItem != null) Logger.Write(typeof(MenuList), string.Format("Hide - {0}", oItem.Name));
                if (nItem != null) Logger.Write(typeof(MenuList), string.Format("Show - {0}", nItem.Name));
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(MenuList), ex);
            }
        }
    }
}
