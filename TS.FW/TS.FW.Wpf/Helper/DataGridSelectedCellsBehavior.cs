using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TS.FW.Wpf.Helper
{
    public class DataGridSelectedCellsBehavior
    {
        public static readonly DependencyProperty SelectedCellsProperty
            = DependencyProperty.RegisterAttached("SelectedCells", typeof(IList<DataGridCellInfo>), typeof(DataGridSelectedCellsBehavior)
                , new UIPropertyMetadata(null, OnSelectedCellsPropertyChanged));

        public static readonly DependencyProperty OnSelectedCellsChangedProperty
            = DependencyProperty.RegisterAttached("OnSelectedCellsChanged", typeof(SelectedCellsChangedEventHandler), typeof(DataGridSelectedCellsBehavior)
                , new UIPropertyMetadata(null));

        public static IList<DataGridCellInfo> GetSelectedCells(DependencyObject obj)
        {
            return (IList<DataGridCellInfo>)obj.GetValue(SelectedCellsProperty);
        }

        public static void SetSelectedCells(DependencyObject obj, IList<DataGridCellInfo> value)
        {
            obj.SetValue(SelectedCellsProperty, value);
        }

        public static SelectedCellsChangedEventHandler GetOnSelectedCellsChanged(DependencyObject obj)
        {
            return (SelectedCellsChangedEventHandler)obj.GetValue(OnSelectedCellsChangedProperty);
        }

        public static void SetOnSelectedCellsChanged(DependencyObject obj, SelectedCellsChangedEventHandler value)
        {
            obj.SetValue(OnSelectedCellsChangedProperty, value);
        }

        private static void OnSelectedCellsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if ((d is DataGrid) == false) return;
                if (GetOnSelectedCellsChanged(d) != null)
                {
                    if(((IList)e.NewValue).Count == 0)
                    {
                        (d as DataGrid).SelectedCells.Clear();
                    }

                    return;
                }

                var control = d as DataGrid;

                foreach (var item in GetSelectedCells(control))
                {
                    control.SelectedCells.Add(item);
                }

                SetOnSelectedCellsChanged(control, OnSelectedCellsChangedEvent);
                control.SelectedCellsChanged += GetOnSelectedCellsChanged(control);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataGridSelectedCellsBehavior), ex);
            }
        }

        private static void OnSelectedCellsChangedEvent(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if ((sender is DataGrid) == false) return;

                var control = sender as DataGrid;

                SetSelectedCells(control, control.SelectedCells);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(DataGridSelectedCellsBehavior), ex);
            }
        }
    }
}
