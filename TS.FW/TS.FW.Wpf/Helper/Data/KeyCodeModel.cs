using System.Runtime.CompilerServices;
using TS.FW.Wpf.Core;

namespace TS.FW.Wpf.Helper.Data
{
    internal class KeyCodeModel : ModelBase
    {
        public string Korea { get => this.GetValue<string>(); set => this.SetValue(value); }

        public string Shift { get => this.GetValue<string>(); set => this.SetValue(value); }

        public bool IsShift { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public string Contents => this.ToContents();

        public KeyCodeModel(string korea, string shift)
        {
            this.Korea = korea;
            this.Shift = shift;

            this.IsShift = false;
        }

        protected virtual string ToContents()
        {
            if (this.IsShift == false)
            {
                return this.Korea;
            }
            else
            {
                return this.Shift;
            }
        }

        protected override void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            try
            {
                base.SetValue(value, propertyName);
            }
            finally
            {
                if (string.Equals(propertyName, nameof(this.IsShift)))
                {
                    this.OnNotifyPropertyChanged(nameof(this.Contents));
                }
            }
        }
    }

    internal class KeyCodeEnglishModel : KeyCodeModel
    {
        public string English { get => this.GetValue<string>(); set => this.SetValue(value); }

        public bool IsKorea { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public KeyCodeEnglishModel(string korea, string shift, string english) : base(korea, shift)
        {
            this.English = english;

            this.IsKorea = false;
        }

        protected override string ToContents()
        {
            if (this.IsKorea)
            {
                return this.IsShift ? this.Shift : this.Korea;
            }
            else
            {
                return this.IsShift ? this.English.ToUpper() : this.English.ToLower();
            }
        }

        protected override void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            try
            {
                base.SetValue(value, propertyName);
            }
            finally
            {
                if (string.Equals(propertyName, nameof(this.IsKorea)) || string.Equals(propertyName, nameof(this.IsShift)))
                {
                    this.OnNotifyPropertyChanged(nameof(this.Contents));
                }
            }
        }
    }
}
