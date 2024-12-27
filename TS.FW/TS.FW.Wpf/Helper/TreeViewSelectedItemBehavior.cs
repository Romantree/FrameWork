using System;
using System.Windows;
using System.Windows.Controls;

namespace TS.FW.Wpf.Helper
{
    public class TreeViewSelectedItemBehavior
    {
        public static readonly DependencyProperty TreeViewSelectedItemProperty
            = DependencyProperty.RegisterAttached("TreeViewSelectedItem", typeof(object), typeof(TreeViewSelectedItemBehavior)
                , new UIPropertyMetadata(new object(), OnDependencyPropertyChanged));

        public static readonly DependencyProperty TreeViewSelectedItemHandlerProperty
            = DependencyProperty.RegisterAttached("TreeViewSelectedItemHandler", typeof(bool), typeof(TreeViewSelectedItemBehavior)
                , new UIPropertyMetadata(false));

        public static object GetTreeViewSelectedItem(DependencyObject obj)
        {
            return obj.GetValue(TreeViewSelectedItemProperty);
        }

        public static void SetTreeViewSelectedItem(DependencyObject obj, object value)
        {
            obj.SetValue(TreeViewSelectedItemProperty, value);
        }

        public static bool GetTreeViewSelectedItemHandler(DependencyObject obj)
        {
            return (bool)obj.GetValue(TreeViewSelectedItemHandlerProperty);
        }

        public static void SetTreeViewSelectedItemHandler(DependencyObject obj, bool value)
        {
            obj.SetValue(TreeViewSelectedItemHandlerProperty, value);
        }

        private static void OnDependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if ((d is TreeView) == false) return;

                var handler = GetTreeViewSelectedItemHandler(d);
                if (handler == true) return;

                var control = d as TreeView;
                control.SelectedItemChanged += Control_SelectedItemChanged;

                SetTreeViewSelectedItemHandler(d, true);

            }
            catch (Exception ex)
            {
                Logger.Write(typeof(TreeViewSelectedItemBehavior), ex);
            }
        }

        private static void Control_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (e.NewValue == e.OldValue) return;

                SetTreeViewSelectedItem(sender as TreeView, e.NewValue);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(TreeViewSelectedItemBehavior), ex);
            }
        }
    }
}
