using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TS.FW.Wpf.v2.Core;

namespace TS.FW.Wpf.v2.Controls
{
    /// <summary>
    /// EventBtn.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EventBtn : Button
    {
        private static readonly ControlProperty<EventBtn> DP = new ControlProperty<EventBtn>();

        public static readonly DependencyProperty LeftDownCommandProperty = DP.ToCommand();
        public static readonly DependencyProperty RightDownCommandProperty = DP.ToCommand();
        public static readonly DependencyProperty LeftUpCommandProperty = DP.ToCommand();
        public static readonly DependencyProperty RightUpCommandProperty = DP.ToCommand();

        public static readonly DependencyProperty MarginContentProperty = DP.ToProperty(new Thickness(0));
        public static readonly DependencyProperty CornerRadiusProperty = DP.ToProperty(new CornerRadius(3));
        public static readonly DependencyProperty DelayTimeProperty = DP.ToProperty(0.0D);

        public static readonly DependencyProperty IconProperty = DP.ToProperty<Visual>();
        public static readonly DependencyProperty IconSizeProperty = DP.ToProperty(16.0D);
        public static readonly DependencyProperty MarginIconProperty = DP.ToProperty(new Thickness(5));
        public static readonly DependencyProperty HorizontalIconAlignmentProperty = DP.ToProperty(HorizontalAlignment.Left);
        public static readonly DependencyProperty VerticalIconAlignmentProperty = DP.ToProperty(VerticalAlignment.Center);

        public static readonly DependencyProperty ShadowProperty = DP.ToProperty(true);

        private readonly VisualBrush _visual = new VisualBrush() { Stretch = Stretch.Fill };
        private ProgressBar xProgress;
        private Rectangle xIcon;
        private DateTime? _startTime = null;

        public ICommand LeftDownCommand { get => (ICommand)this.GetValue(LeftDownCommandProperty); set => this.SetValue(LeftDownCommandProperty, value); }

        public ICommand RightDownCommand { get => (ICommand)this.GetValue(RightDownCommandProperty); set => this.SetValue(RightDownCommandProperty, value); }

        public ICommand LeftUpCommand { get => (ICommand)this.GetValue(LeftUpCommandProperty); set => this.SetValue(LeftUpCommandProperty, value); }

        public ICommand RightUpCommand { get => (ICommand)this.GetValue(RightUpCommandProperty); set => this.SetValue(RightUpCommandProperty, value); }

        public Thickness MarginContent { get => (Thickness)this.GetValue(MarginContentProperty); set => this.SetValue(MarginContentProperty, value); }

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public double DelayTime { get => (double)this.GetValue(DelayTimeProperty); set => this.SetValue(DelayTimeProperty, value); }

        public Visual Icon { get => (Visual)this.GetValue(IconProperty); set { this.SetValue(IconProperty, value); this._visual.Visual = value; } }

        public double IconSize { get => (double)this.GetValue(IconSizeProperty); set => this.SetValue(IconSizeProperty, value); }

        public Thickness MarginIcon { get => (Thickness)this.GetValue(MarginIconProperty); set => this.SetValue(MarginIconProperty, value); }

        public HorizontalAlignment HorizontalIconAlignment { get => (HorizontalAlignment)this.GetValue(HorizontalIconAlignmentProperty); set => this.SetValue(HorizontalIconAlignmentProperty, value); }

        public VerticalAlignment VerticalIconAlignment { get => (VerticalAlignment)this.GetValue(VerticalIconAlignmentProperty); set => this.SetValue(VerticalIconAlignmentProperty, value); }

        public bool Shadow { get => (bool)this.GetValue(ShadowProperty); set => this.SetValue(ShadowProperty, value); }

        public EventBtn()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            try
            {
                if (this.xProgress == null)
                {
                    this.xProgress = (ProgressBar)this.Template.FindName("xProgress", this);
                }

                if (this.xIcon == null)
                {
                    this.xIcon = (Rectangle)this.Template.FindName("xIcon", this);
                    this.xIcon.OpacityMask = this._visual;
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

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            try
            {
                if (this.DelayTime <= 0) return;

                e.Handled = true;

                this.xProgress.Visibility = Visibility.Visible;
                this.xProgress.Maximum = this.DelayTime;

                this._startTime = DateTime.Now;

                do
                {
                    this.xProgress.Value = (DateTime.Now - this._startTime.Value).TotalSeconds;
                    if (this.xProgress.Value >= this.DelayTime)
                    {
                        this.Command?.Execute(this.CommandParameter);
                        break;
                    }

                    DoEvent.DoEvents();

                    Thread.Sleep(10);

                } while (this._startTime != null);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
            finally
            {
                this._startTime = null;
                this.xProgress.Visibility = Visibility.Hidden;
                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            try
            {
                this._startTime = null;
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
                this._startTime = null;
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
                this.LeftDownCommand?.Execute(this.CommandParameter);
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

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            try
            {
                this.RightDownCommand?.Execute(this.CommandParameter);
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

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            try
            {
                this.LeftUpCommand?.Execute(this.CommandParameter);
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

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            try
            {
                this.RightUpCommand?.Execute(this.CommandParameter);
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
    }
}
