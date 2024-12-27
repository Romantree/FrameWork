using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// Label3D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Label3D : Label
    {
        private static ControlProperty<Label3D> Property = new ControlProperty<Label3D>();

        public static readonly DependencyProperty IsPressedProperty = Property.ToProperty("IsPressed", typeof(bool), false);
        public static readonly DependencyProperty PasswordProperty = Property.ToProperty("Password", typeof(bool), false);
        public static readonly DependencyProperty CornerRadiusProperty = Property.ToProperty("CornerRadius", typeof(CornerRadius), new CornerRadius(0));
        public static readonly DependencyProperty ContentMarginProperty = Property.ToProperty("ContentMargin", typeof(Thickness), new Thickness(0));

        public static readonly DependencyPropertyDescriptor ContentPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(Label.ContentProperty, typeof(Label));

        private PasswordBox _password;

        public bool IsPressed { get => (bool)this.GetValue(IsPressedProperty); set => this.SetValue(IsPressedProperty, value); }

        public bool Password { get => (bool)this.GetValue(PasswordProperty); set => this.SetValue(PasswordProperty, value); }

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public Thickness ContentMargin { get => (Thickness)this.GetValue(ContentMarginProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public Label3D()
        {
            InitializeComponent();

            ContentPropertyDescriptor.AddValueChanged(this, ContentChanged);
        }

        public override void OnApplyTemplate()
        {
            try
            {
                if (this._password == null) this._password = (PasswordBox)this.Template.FindName("xPassword", this);
                if (this._password == null) return;

                if (this.Content is string)
                {
                    this._password.Password = this.Content as string;
                }
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

        private void ContentChanged(object sender, EventArgs e)
        {
            try
            {
                if (this._password == null) this._password = (PasswordBox)this.Template.FindName("xPassword", this);
                if (this._password == null) return;

                if (this.Content is string)
                {
                    this._password.Password = this.Content as string;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }
}
