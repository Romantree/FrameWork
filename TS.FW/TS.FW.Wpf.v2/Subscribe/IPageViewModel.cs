using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using TS.FW.Wpf.v2.Core;

namespace TS.FW.Wpf.v2.Subscribe
{
    public abstract class IPageViewModel : IViewModel
    {
        private static Dictionary<string, Type[]> _tList = new Dictionary<string, Type[]>();

        public abstract int No { get; }

        public abstract string Name { get; }

        public abstract FrameworkElement View { get; }

        public virtual Visual Icon { get; }

        public virtual Visibility Visibility { get => this.GetValue<Visibility>(); set => this.SetValue(value); }

        public bool IsSelect { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public bool IsEnabled { get => this.GetValue<bool>(); set => this.SetValue(value); }

        public IPageViewModel()
        {
            this.Visibility = Visibility.Visible;
            this.IsSelect = false;
            this.IsEnabled = true;
        }

        public virtual void Init() { }

        public virtual void Show() => this.IsSelect = true;

        public virtual void Hide() => this.IsSelect = false;

        public virtual void Update() { }

        public override string ToString() => $"{this.No}. {this.Name}";

        public static List<T> ToPageViewList<T>()  where T : IPageViewModel
        {
            var type = typeof(T);
            var list = new List<T>();

            var assembly = Assembly.GetEntryAssembly();

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
