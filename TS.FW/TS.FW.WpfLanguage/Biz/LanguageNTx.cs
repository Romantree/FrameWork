using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Core;

namespace TS.FW.WpfLanguage
{
    public class LanguageNTx : IBizNTxBase
    {
        public ResponseList<LanguageEntity> GetLanguageList()
        {
            try
            {
                using (var dac = new LanguageDac())
                {
                    return new ResponseList<LanguageEntity>(dac.SelectedAll());
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }

        public Response<LanguageEntity> GetLanguage(string code)
        {
            try
            {
                using (var dac = new LanguageDac())
                {
                    return new Response<LanguageEntity>(dac.Selected(code));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return ex;
            }
        }
    }
}
