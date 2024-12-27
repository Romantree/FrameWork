using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TS.FW.Wpf.Controls;

namespace TS.FW.Wpf.Core
{
    public abstract class IMenuViewModel : ViewModelBase
    {
        private static Dictionary<string, Type[]> _tList = new Dictionary<string, Type[]>();

        public abstract int No { get; }

        public abstract string Name { get; }

        public abstract ContentControl View { get; }

        public virtual eIconType Icon { get; private set; } = eIconType.Custom;

        public virtual Visual Visual { get; }

        public virtual Visibility Visibility { get => this.GetValue<Visibility>(); set => this.SetValue(value); }

        public bool IsSelect { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsEnabled { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public IMenuViewModel()
        {
            this.Visibility = Visibility.Visible;
            this.IsSelect = false;
            this.IsEnabled = true;
        }

        public virtual void Init() { }

        public virtual void Show()
        {
            this.IsSelect = true;
        }

        public virtual void Hide()
        {
            this.IsSelect = false;
        }

        public virtual void Update() { }

        public static List<T> ToMenuViewModelList<T>() where T : IMenuViewModel
        {
            var type = typeof(T);
            var list = new List<T>();

            var assembly = type.Assembly;

            if (_tList.ContainsKey(assembly.FullName) == false)
            {
                var tList = assembly.GetTypes();
                _tList.Add(assembly.FullName, tList);
            }

            foreach (var item in _tList[assembly.FullName].Where(t => t.IsAbstract == false && t.BaseType == type))
            {
                list.Add(Activator.CreateInstance(item) as T);
            }

            return list.OrderBy(t => t.No).ToList();
        }
    }
}
