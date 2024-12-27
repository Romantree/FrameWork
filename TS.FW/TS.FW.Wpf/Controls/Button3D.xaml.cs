using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TS.FW.Wpf.Helper;

namespace TS.FW.Wpf.Controls
{
    /// <summary>
    /// Button3D.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Button3D : Button
    {
        private static ControlProperty<Button3D> Property = new ControlProperty<Button3D>();

        public static readonly DependencyProperty LeftButtonDownProperty = Property.ToCommand("LeftButtonDown");
        public static readonly DependencyProperty LeftButtonUpProperty = Property.ToCommand("LeftButtonUp");
        public static readonly DependencyProperty RightButtonDownProperty = Property.ToCommand("RightButtonDown");
        public static readonly DependencyProperty RightButtonUpProperty = Property.ToCommand("RightButtonUp");
        public static readonly DependencyProperty DoubleButtonProperty = Property.ToCommand("DoubleButton");

        public static readonly DependencyProperty DelayMscProperty = Property.ToProperty("DelayMsc", typeof(int), 0);
        public static readonly DependencyProperty CornerRadiusProperty = Property.ToProperty("CornerRadius", typeof(CornerRadius), new CornerRadius(0));
        public static readonly DependencyProperty ContentMarginProperty = Property.ToProperty("ContentMargin", typeof(Thickness), new Thickness(0));

        public static readonly DependencyProperty IconProperty = Property.ToProperty("Icon", typeof(eIconType), eIconType.None);
        public static readonly DependencyProperty IconMarginProperty = Property.ToProperty("IconMargin", typeof(Thickness), new Thickness(2));
        public static readonly DependencyProperty IconWidthProperty = Property.ToProperty("IconWidth", typeof(double), 32.0D);
        public static readonly DependencyProperty IconHeightProperty = Property.ToProperty("IconHeight", typeof(double), 32.0D);
        public static readonly DependencyProperty HorizontalIconAlignmentProperty = Property.ToProperty("HorizontalIconAlignment", typeof(HorizontalAlignment), HorizontalAlignment.Left);
        public static readonly DependencyProperty VerticalIconAlignmentProperty = Property.ToProperty("VerticalIconAlignment", typeof(VerticalAlignment), VerticalAlignment.Center);
        public static readonly DependencyProperty VisualProperty = Property.ToProperty("Visual", typeof(Visual), null);

        public static readonly DependencyProperty TitleProperty = Property.ToProperty("Title", typeof(string), "");
        public static readonly DependencyProperty TitleFontSizeProperty = Property.ToProperty("TitleFontSize", typeof(double), 10.0D);
        public static readonly DependencyProperty TitleColorSizeProperty = Property.ToProperty("TitleColor", typeof(Brush), new SolidColorBrush(Colors.Gray));

        private readonly object _locker = new object();
        private DateTime? _delayTime = null;
        private System.Windows.Controls.Primitives.Popup _puDelay;
        private ProgressBar _pbDelay;

        public ICommand LeftButtonDown { get => (ICommand)this.GetValue(LeftButtonDownProperty); set => this.SetValue(LeftButtonDownProperty, value); }

        public ICommand LeftButtonUp { get => (ICommand)this.GetValue(LeftButtonUpProperty); set => this.SetValue(LeftButtonUpProperty, value); }

        public ICommand RightButtonDown { get => (ICommand)this.GetValue(RightButtonDownProperty); set => this.SetValue(RightButtonDownProperty, value); }

        public ICommand RightButtonUp { get => (ICommand)this.GetValue(RightButtonUpProperty); set => this.SetValue(RightButtonUpProperty, value); }

        public ICommand DoubleButton { get => (ICommand)this.GetValue(DoubleButtonProperty); set => this.SetValue(DoubleButtonProperty, value); }

        public int DelayMsc { get => (int)this.GetValue(DelayMscProperty); set => this.SetValue(DelayMscProperty, value); }

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public Thickness ContentMargin { get => (Thickness)this.GetValue(ContentMarginProperty); set => this.SetValue(ContentMarginProperty, value); }

        public eIconType Icon { get => (eIconType)this.GetValue(IconProperty); set => this.SetValue(IconProperty, value); }

        public Thickness IconMargin { get => (Thickness)this.GetValue(IconMarginProperty); set => this.SetValue(IconMarginProperty, value); }

        public double IconWidth { get => (double)this.GetValue(IconWidthProperty); set => this.SetValue(IconWidthProperty, value); }

        public double IconHeight { get => (double)this.GetValue(IconHeightProperty); set => this.SetValue(IconHeightProperty, value); }

        public HorizontalAlignment HorizontalIconAlignment { get => (HorizontalAlignment)this.GetValue(HorizontalIconAlignmentProperty); set => this.SetValue(HorizontalIconAlignmentProperty, value); }

        public VerticalAlignment VerticalIconAlignment { get => (VerticalAlignment)this.GetValue(VerticalIconAlignmentProperty); set => this.SetValue(VerticalIconAlignmentProperty, value); }

        public Visual Visual { get => (Visual)this.GetValue(VisualProperty); set => this.SetValue(VisualProperty, value); }

        public string Title { get => (string)this.GetValue(TitleProperty); set => this.SetValue(TitleProperty, value); }

        public double TitleFontSize { get => (double)this.GetValue(TitleFontSizeProperty); set => this.SetValue(TitleFontSizeProperty, value); }

        public Brush TitleColor { get => (Brush)this.GetValue(TitleColorSizeProperty); set => this.SetValue(TitleColorSizeProperty, value); }

        public Button3D()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            try
            {
                var delayMsc = this.DelayMsc;
                if (delayMsc <= 0) return;

                e.Handled = true;

                this._delayTime = DateTime.Now;

                this.DelayPopupOpen(delayMsc);

                while (true)
                {
                    lock (this._locker)
                    {
                        if (this._delayTime.HasValue == false) return;

                        var time = (DateTime.Now - this._delayTime.Value).TotalMilliseconds;
                        if (time >= delayMsc) break;

                        this._pbDelay.Value = time;

                        DoEvent.DoEvents();
                    }

                    Thread.Sleep(10);
                }

                this.Command?.Execute(this.CommandParameter);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                if (this._puDelay != null) this._puDelay.IsOpen = false;
                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            try
            {
                lock (this._locker) this._delayTime = null;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnMouseUp(e);
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            try
            {
                lock (this._locker) this._delayTime = null;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnMouseLeave(e);
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            try
            {
                this.LeftButtonDown?.Execute(this.CommandParameter);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnMouseLeftButtonDown(e);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            try
            {
                this.LeftButtonUp?.Execute(this.CommandParameter);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnMouseLeftButtonUp(e);
            }
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            try
            {
                this.RightButtonDown?.Execute(this.CommandParameter);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnMouseRightButtonDown(e);
            }
        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            try
            {
                this.RightButtonUp?.Execute(this.CommandParameter);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnMouseRightButtonUp(e);
            }
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            try
            {
                this.DoubleButton?.Execute(this.CommandParameter);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                base.OnMouseDoubleClick(e);
            }
        }

        private void DelayPopupOpen(int delayMsc)
        {
            if (this._puDelay == null || this._pbDelay == null)
            {
                this._puDelay = (System.Windows.Controls.Primitives.Popup)this.Template.FindName("xpuDelay", this);
                this._pbDelay = (ProgressBar)this.Template.FindName("xpbDelay", this);

                var width = this.ActualWidth;
                var height = this.ActualHeight / 2;

                this._puDelay.Width = width;
                this._puDelay.Height = height;

                this._pbDelay.Width = width;
                this._pbDelay.Height = height;

                this._puDelay.UpdateLayout();
            }

            this._puDelay.IsOpen = true;

            this._pbDelay.Minimum = 0;
            this._pbDelay.Maximum = delayMsc;
        }
    }
}