using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace TS.FW.Wpf.v2.Core
{
    [DataContract]
    public abstract class IModel : INotifyPropertyChanged
    {
        protected readonly Dictionary<string,object> _properties = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SetValue(object value, [CallerMemberName] string name = null)
        {
            if (this.CheckData(name, value)) return;

            this._properties[name] = value;
            this.OnPropertyChanged(name);
        }

        protected T GetValue<T>([CallerMemberName] string name = null)
        {
            if (this._properties.ContainsKey(name) == false)
            {
                if(typeof(T) == typeof(string))
                {
                    this._properties[name] = string.Empty;
                }
                else
                {
                    this._properties[name] = default(T);
                }
            }

            return (T)this._properties[name];
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private bool CheckData(string name, object value)
        {
            if (this._properties.ContainsKey(name) == false) return false;

            var old = this._properties[name];

            if (old == null && value == null) return true;
            if (old == null || value == null) return false;

            return old.Equals(value);
        }
    }
}