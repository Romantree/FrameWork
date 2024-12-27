using System;
using System.Windows;
using System.Windows.Input;

namespace TS.FW.Wpf
{
    public class ControlProperty<T>
    {
        public DependencyProperty ToProperty(string name, Type propertyType, object defaultValue, FrameworkPropertyMetadataOptions option = FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        {
            return DependencyProperty.RegisterAttached(name, propertyType, typeof(T), new FrameworkPropertyMetadata(defaultValue, option, OnDependencyProeprtyChanged));
        }
        public DependencyProperty ToProperty(string name, Type propertyType, object defaultValue, FrameworkPropertyMetadataOptions option, PropertyChangedCallback callback)
        {
            return DependencyProperty.RegisterAttached(name, propertyType, typeof(T), new FrameworkPropertyMetadata(defaultValue, option, callback));
        }

        public DependencyProperty ToCommand(string name)
        {
            return DependencyProperty.RegisterAttached(name, typeof(ICommand), typeof(T));
        }

        public DependencyProperty ToCommandParameter(string name)
        {
            return DependencyProperty.RegisterAttached(name, typeof(object), typeof(T));
        }

        private void OnDependencyProeprtyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as FrameworkElement;
            if (control == null) return;

            if (e.OldValue != null && e.NewValue != null)
            {
                if (e.OldValue.Equals(e.NewValue)) return;
            }

            control.SetValue(e.Property, e.NewValue);
        }
    }
}
