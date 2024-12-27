using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace TS.FW.Wpf.v2.Helpers
{
    public class ResourceHelper
    {
        public static readonly ResourceHelper Ins;

        static ResourceHelper() => Ins = new ResourceHelper();

        private readonly Dictionary<string, ResourceDictionary> _list = new Dictionary<string, ResourceDictionary>();

        public object this[string name, string dic] => this.GetValue(name, dic);

        private ResourceHelper()
        {
            var list = Application.Current.Resources.MergedDictionaries.OfType<ResourceDictionary>().Where(t => t != null && t.Source != null);

            foreach (var item in list)
            {
                _list.Add(Path.GetFileNameWithoutExtension(item.Source.OriginalString), item);
            }
        }

        public object GetValue(string name, string dic)
        {
            if (_list.ContainsKey(dic) == false) return null;

            return _list[dic][name];
        }

        public ResourceDictionary GetDictionary(string dic)
        {
            if (_list.ContainsKey(dic) == false) return null;

            return _list[dic];
        }
    }
}
