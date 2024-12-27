using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TS.FW.Wpf.v2.Controls.InPut;

namespace TS.FW.Wpf.v2.Controls
{
    /// <summary>
    /// DatePickerBtn.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DatePickerBtn : DatePicker
    {
        private static readonly ControlProperty<DatePickerBtn> DP = new ControlProperty<DatePickerBtn>();

        public static readonly DependencyProperty ShadowProperty = DP.ToProperty(true);
        public static readonly DependencyProperty CornerRadiusProperty = DP.ToProperty(new CornerRadius(3));
        public static readonly DependencyProperty StringFormatProperty = DP.ToProperty("{0:yyyy-MM-dd tt hh:mm}");

        public static readonly DependencyProperty HourProperty = DP.ToProperty(0);
        public static readonly DependencyProperty MinuteProperty = DP.ToProperty(0);

        public static readonly DependencyProperty DatePickerFormatProperty = DP.ToProperty(DatePickerFormat.DateTime);

        public static readonly DependencyPropertyDescriptor DisplayDateDescriptor = DP.ToPropertyDescriptor(DatePicker.DisplayDateProperty);

        public CornerRadius CornerRadius { get => (CornerRadius)this.GetValue(CornerRadiusProperty); set => this.SetValue(CornerRadiusProperty, value); }

        public bool Shadow { get => (bool)this.GetValue(ShadowProperty); set => this.SetValue(ShadowProperty, value); }

        public string StringFormat { get => (string)this.GetValue(StringFormatProperty); set => this.SetValue(StringFormatProperty, value); }

        public int Hour { get => (int)this.GetValue(HourProperty); set => this.SetValue(HourProperty, value); }

        public int Minute { get => (int)this.GetValue(MinuteProperty); set => this.SetValue(MinuteProperty, value); }

        public DatePickerFormat DatePickerFormat { get => (DatePickerFormat)this.GetValue(DatePickerFormatProperty); set => this.SetValue(DatePickerFormatProperty, value); }

        public DatePickerBtn()
        {
            InitializeComponent();

            DisplayDateDescriptor.AddValueChanged(this, DisplayDateChanged);
        }

        private void DisplayDateChanged(object sender, EventArgs e)
        {
            try
            {
                this.Hour = this.DisplayDate.Hour;
                this.Minute = this.DisplayDate.Minute;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Hour_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hour = KeyPad.Show(this.Hour, 0, 23);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Minute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Minute = KeyPad.Show(this.Minute, 0, 60);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.SelectedDate.HasValue == false) return;

                this.DisplayDate = this.SelectedDate.Value.Date
                    .AddHours(this.Hour)
                    .AddMinutes(this.Minute);

                this.IsDropDownOpen = false;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            try
            {
                this.SelectedDate = this.DisplayDate;
                this.Focus();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.DatePickerFormat != DatePickerFormat.Date) return;

                this.DisplayDate = this.SelectedDate.Value;

                this.IsDropDownOpen = false;
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
            }
        }
    }

    public enum DatePickerFormat
    {
        Date,
        Time,
        DateTime,
    }
}
