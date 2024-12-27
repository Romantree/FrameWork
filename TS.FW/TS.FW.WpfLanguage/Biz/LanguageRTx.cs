using System;
using TS.FW.Dac.Core;
using TS.FW.Dac.Transaction;

namespace TS.FW.WpfLanguage
{
    public class LanguageRTx : IBizRTxBase
    {
        public Response Insert(LanguageEntity item)
        {
            try
            {
                using (var dac = new LanguageDac())
                {
                    dac.Insert(item);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                ContextUtil.SetAbort();
                return ex;
            }
        }

        public Response Update(LanguageEntity item)
        {
            try
            {
                using (var dac = new LanguageDac())
                {
                    dac.Update(item);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                ContextUtil.SetAbort();
                return ex;
            }
        }

        public Response Delete(string code)
        {
            try
            {
                using (var dac = new LanguageDac())
                {
                    dac.Delete(code);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                ContextUtil.SetAbort();
                return ex;
            }
        }

        public Response DeleteAll()
        {
            try
            {
                using (var dac = new LanguageDac())
                {
                    dac.DeleteAll();
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                ContextUtil.SetAbort();
                return ex;
            }
        }
    }
}
