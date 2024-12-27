using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace TS.FW.Wpf.Helper
{
    public static class ResourceHelper
    {
        private readonly static Dictionary<string, ResourceDictionary> _list = new Dictionary<string, ResourceDictionary>();

        static ResourceHelper()
        {
            var list = Application.Current.Resources.MergedDictionaries.OfType<ResourceDictionary>().Where(t => t != null && t.Source != null);

            foreach (var item in list)
            {
                _list.Add(item.Source.OriginalString, item);
            }
        }

        public static Visual ToVisual(string name, string dictionary)
        {
            if (_list.ContainsKey(dictionary) == false) return null;

            return (Visual)_list[dictionary][name];
        }
    }
}
