using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace TS.FW.Wpf.Core
{
    /// <summary>
    /// Model 베이스 클래스
    ///  - 모든 Model 클래스는 해당 클래스를 상속 받아야 한다.
    /// </summary>
    [DataContract]
    public abstract class ModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 프로퍼티 리스트
        /// </summary>
        protected Dictionary<string, object> _PropertyList = new Dictionary<string, object>();

        /// <summary>
        /// 프로퍼티 변경 알림 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 디자인 모드 여부
        /// </summary>
        public bool IsDesignMode => DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());

        /// <summary>
        /// 프로퍼티 값 설정 여부
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool HasValue([CallerMemberName] string propertyName = null)
        {
            if (this._PropertyList == null) return false;
            if (this._PropertyList.ContainsKey(propertyName) == false) return false;

            return this._PropertyList[propertyName] != null;
        }

        /// <summary>
        /// 프로퍼티 값 설정하기
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        protected virtual void SetValue(object value, [CallerMemberName] string propertyName = null)
        {
            if (this._PropertyList == null) this._PropertyList = new Dictionary<string, object>();

            if (this._PropertyList.ContainsKey(propertyName))
            {
                //if (this._PropertyList[propertyName] == value) return;
                if (this.CheckData(this._PropertyList[propertyName], value)) return;

                this._PropertyList[propertyName] = value;
            }
            else
            {
                this._PropertyList.Add(propertyName, value);
            }

            this.OnNotifyPropertyChanged(propertyName);
        }

        private bool CheckData(object a, object b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;

            return a.Equals(b);
        }

        /// <summary>
        /// 프로퍼티 값 가져오기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            if (this._PropertyList == null) this._PropertyList = new Dictionary<string, object>();

            if (this._PropertyList.ContainsKey(propertyName) == false) return default(T);

            return (T)this._PropertyList[propertyName];
        }

        /// <summary>
        /// 프로퍼티 변경 알림
        /// </summary>
        /// <param name="propertyName">프로퍼티 명 -  CallerMember로 인수 없이 사용가능</param>
        protected void OnNotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged == null || string.IsNullOrWhiteSpace(propertyName)) return;

            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
