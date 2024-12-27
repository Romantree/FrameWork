using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace TS.FW.Wpf.v2
{
    /// <summary>
    /// DependencyProperty 동록시 이름 마지막에 꼭 Property를 써줘야 된다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ControlProperty<T> where T : FrameworkElement
    {
        public DependencyProperty ToProperty<TP>(TP defaultValue = default(TP), [CallerMemberName] string name = null)
            => DependencyProperty.RegisterAttached(Replace(name), typeof(TP), typeof(T), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDependencyProeprtyChanged));

        public DependencyProperty ToProperty<TP>(PropertyChangedCallback callback, TP defaultValue = default(TP), [CallerMemberName] string name = null)
            => DependencyProperty.RegisterAttached(Replace(name), typeof(TP), typeof(T), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, callback));

        public DependencyProperty ToCommand([CallerMemberName] string name = null) => DependencyProperty.RegisterAttached(Replace(name), typeof(ICommand), typeof(T));

        public DependencyProperty ToCommandParameter([CallerMemberName] string name = null) => DependencyProperty.RegisterAttached(Replace(name), typeof(object), typeof(T));

        public DependencyPropertyDescriptor ToPropertyDescriptor(DependencyProperty proeprty) => DependencyPropertyDescriptor.FromProperty(proeprty, typeof(T));

        private string Replace(string name) => name.Substring(0, name.IndexOf("Property"));

        private void OnDependencyProeprtyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cnt = d as T;
            if (cnt == null) return;

            if (e.OldValue != null && e.NewValue != null)
            {
                if (e.OldValue.Equals(e.NewValue)) return;
            }

            cnt.GetType().GetProperty(e.Property.Name).SetValue(cnt, e.NewValue);
        }
    }
}
