using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TS.FW.Wpf.v2.Controls.InPut;

namespace TS.FW.Wpf.v2.Controls
{
    /// <summary>
    /// ValueBtn.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ValueBtn : Button
    {
        private static readonly ControlProperty<ValueBtn> DP = new ControlProperty<ValueBtn>();

        public static readonly DependencyProperty MinProperty = DP.ToProperty<object>();
        public static readonly DependencyProperty MaxProperty = DP.ToProperty<object>();

        public static readonly DependencyProperty MarginContentProperty = DP.ToProperty(new Thickness(0));
        public static readonly DependencyProperty CornerRadiusProperty = DP.ToProperty(new CornerRadius(3));

        public static readonly DependencyProperty PasswordProperty = DP.ToProperty(false);

        public static readonly DependencyProperty ShadowProperty = DP.ToProperty(true);

        public static readonly DependencyProperty RemarkStyleProperty = DP.ToProperty<Style>();

        public static readonly DependencyPropertyDescriptor ContentDescriptor = DP.ToPropertyDescriptor(Button.ContentProperty);
        public static readonly DependencyPropertyDescriptor MinDescriptor = DP.ToPropertyDescriptor(ValueBtn.MinProperty);
        public static readonly DependencyPropertyDescriptor MaxDescriptor = DP.ToPropertyDescriptor(ValueBtn.MaxProperty);

        private PasswordBox xPassword;
        private ContentPresenter xContent;
        private TextBlock xRemark;

        public object Min { get => (object)this.GetValue(MinProperty); set => this.SetValue(MinProperty, value); }

        public object Max { get => (object)this.GetValue(MaxProperty); set => this.SetValue(MaxProperty, value); }

        public Thickness MarginContent { get => (Thickness)this.GetValue(MarginContentProperty); set => this.SetValue(MarginContentProperty, value); }

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public bool Password { get => (bool)this.GetValue(PasswordProperty); set => this.SetValue(PasswordProperty, value); }

        public bool Shadow { get => (bool)this.GetValue(ShadowProperty); set => this.SetValue(ShadowProperty, value); }

        public Style RemarkStyle { get => (Style)this.GetValue(RemarkStyleProperty); set => this.SetValue(RemarkStyleProperty, value); }

        public ValueBtn()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            try
            {
                this.xPassword = (PasswordBox)this.Template.FindName("xPassword", this);
                this.xContent = (ContentPresenter)this.Template.FindName("xContent", this);
                this.xRemark = (TextBlock)this.Template.FindName("xRemark", this);

                if (this.xPassword != null)
                {
                    this.xPassword.Visibility = this.Password ? Visibility.Visible : Visibility.Hidden;
                    this.xPassword.Password = $"{this.Content}";
                }

                if (this.xContent != null)
                {
                    this.xContent.Visibility = this.Password ? Visibility.Hidden : Visibility.Visible;
                }

                if (this.xRemark != null)
                {
                    this.xRemark.Text = this.ToRemark();
                }

                ContentDescriptor.AddValueChanged(this, OnContentChanged);
                MinDescriptor.AddValueChanged(this, OnMinMaxChanged);
                MaxDescriptor.AddValueChanged(this, OnMinMaxChanged);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnApplyTemplate();
            }
        }

        private void OnContentChanged(object sender, EventArgs e)
        {
            try
            {
                this.xPassword.Password = $"{this.Content}";
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void OnMinMaxChanged(object sender, EventArgs e)
        {
            try
            {
                this.xRemark.Text = this.ToRemark();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private string ToRemark()
        {
            if (Min == null && Max == null) return string.Empty;

            var list = new List<string>();

            if (Min != null)
            {
                list.Add($"Min : {Min}");
            }

            if (Max != null)
            {
                list.Add($"Max : {Max}");
            }

            return string.Join(" ", list);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            try
            {
                var binding = this.GetBindingExpression(Button.ContentProperty);
                if (binding == null) return;

                var model = binding.ResolvedSource;
                var name = binding.ResolvedSourcePropertyName;

                var property = model.GetType().GetProperty(name);

                if (property.PropertyType == typeof(sbyte))
                {
                    var old = (sbyte)property.GetValue(model);
                    var value = KeyPad.Show(old, this.Min, this.Max);

                    property.SetValue(model, value);
                }
                else if (property.PropertyType == typeof(byte))
                {
                    var old = (byte)property.GetValue(model);
                    var value = KeyPad.Show(old, this.Min, this.Max);

                    property.SetValue(model, value);
                }
                else if (property.PropertyType == typeof(short))
                {
                    var old = (short)property.GetValue(model);
                    var value = KeyPad.Show(old, this.Min, this.Max);

                    property.SetValue(model, value);
                }
                else if (property.PropertyType == typeof(ushort))
                {
                    var old = (ushort)property.GetValue(model);
                    var value = KeyPad.Show(old, this.Min, this.Max);

                    property.SetValue(model, value);
                }
                else if (property.PropertyType == typeof(int))
                {
                    var old = (int)property.GetValue(model);
                    var value = KeyPad.Show(old, this.Min, this.Max);

                    property.SetValue(model, value);
                }
                else if (property.PropertyType == typeof(uint))
                {
                    var old = (uint)property.GetValue(model);
                    var value = KeyPad.Show(old, this.Min, this.Max);

                    property.SetValue(model, value);
                }
                else if (property.PropertyType == typeof(long))
                {
                    var old = (long)property.GetValue(model);
                    var value = KeyPad.Show(old, this.Min, this.Max);

                    property.SetValue(model, value);
                }
                else if (property.PropertyType == typeof(ulong))
                {
                    var old = (ulong)property.GetValue(model);
                    var value = KeyPad.Show(old, this.Min, this.Max);

                    property.SetValue(model, value);
                }
                else if (property.PropertyType == typeof(float))
                {
                    var old = (float)property.GetValue(model);
                    var value = KeyPad.Show(old, this.Min, this.Max);

                    property.SetValue(model, value);
                }
                else if (property.PropertyType == typeof(double))
                {
                    var old = (double)property.GetValue(model);
                    var value = KeyPad.Show(old, this.Min, this.Max);

                    property.SetValue(model, value);
                }
                else if (property.PropertyType == typeof(string))
                {
                    var old = (string)property.GetValue(model);
                    var value = this.Password ? KeyboardPad.ShowPassword(old) : KeyboardPad.Show(old);

                    property.SetValue(model, value);
                }

                this.Command?.Execute(this.CommandParameter);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnMouseDown(e);
            }
        }
    }
}
