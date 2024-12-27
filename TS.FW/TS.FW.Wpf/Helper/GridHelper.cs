using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TS.FW.Wpf.Helper
{
    public static class GridHelper
    {
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.RegisterAttached("RowCount", typeof(int), typeof(GridHelper), new PropertyMetadata(1, OnRowCountPropertyChanged));

        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.RegisterAttached("ColumnCount", typeof(int), typeof(GridHelper), new PropertyMetadata(1, OnColumnCountPropertyChanged));

        public static int GetRowCount(DependencyObject obj)
        {
            return (int)obj.GetValue(RowCountProperty);
        }

        public static void SetRowCount(DependencyObject obj, int value)
        {
            obj.SetValue(RowCountProperty, value);
        }

        public static int GetColumnCount(DependencyObject obj)
        {
            return (int)obj.GetValue(ColumnCountProperty);
        }

        public static void SetColumnCount(DependencyObject obj, int value)
        {
            obj.SetValue(ColumnCountProperty, value);
        }

        public static Point GetColumnRow(this Grid obj, Point relativePoint) { return new Point(GetColumn(obj, relativePoint.X), GetRow(obj, relativePoint.Y)); }

        private static int GetRow(this Grid obj, double relativeY) { return GetData(obj.RowDefinitions, relativeY); }

        private static int GetColumn(this Grid obj, double relativeX) { return GetData(obj.ColumnDefinitions, relativeX); }

        private static int GetData<T>(this IEnumerable<T> list, double value) where T : DefinitionBase
        {
            var start = 0.0;
            var result = 0;

            var property = typeof(T).GetProperties().FirstOrDefault(p => p.Name.StartsWith("Actual"));
            if (property == null) { return result; }

            foreach (var definition in list)
            {
                start += (double)property.GetValue(definition);
                if (value < start) { break; }

                result++;
            }

            return result;
        }

        private static void OnRowCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if ((d is Grid) == false || (int)e.NewValue < 0) return;

                var grid = d as Grid;
                var count = (int)e.NewValue;
                grid.RowDefinitions.Clear();

                for (int i = 0; i < count; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static void OnColumnCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if ((d is Grid) == false || (int)e.NewValue < 0) return;

                var grid = d as Grid;
                var count = (int)e.NewValue;
                grid.ColumnDefinitions.Clear();

                for (int i = 0; i < count; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
