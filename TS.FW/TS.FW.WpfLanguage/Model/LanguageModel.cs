using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.WpfLanguage
{
    public class LanguageModel
    {
        public string Code { get; set; }

        public string Korea { get; set; }

        public string English { get; set; }

        public string China { get; set; }

        public static implicit operator LanguageModel(LanguageEntity item)
        {
            if (item == null) return null;

            return new LanguageModel()
            {
                Code = item.Code,
                Korea = item.Korea,
                English = item.English,
                China = item.China,
            };
        }

        public static implicit operator LanguageEntity(LanguageModel item)
        {
            if (item == null) return null;

            return new LanguageEntity()
            {
                Code = item.Code,
                Korea = item.Korea,
                English = item.English,
                China = item.China,
            };
        }
    }
}
