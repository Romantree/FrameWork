using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Core;
using TS.FW.Dac.Transaction;

namespace TS.FW.Dac.Cfg
{
    public class ConfigRTx : IBizRTxBase
    {
        public Response Upsert(string group, string name, object value)
        {
            try
            {
                var item = value == null ? string.Empty : value.ToString();

                using (var dac = new ConfigDac())
                {
                    var entity = dac.SelectedForName(group, name);
                    if (entity == null)
                    {
                        dac.Insert(new ConfigEntity() { GroupName = group, Name = name, Value = item });
                    }
                    else
                    {
                        dac.Update(group, name, item);
                    }
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
