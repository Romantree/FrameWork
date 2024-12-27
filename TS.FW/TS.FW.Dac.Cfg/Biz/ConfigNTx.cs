using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Core;

namespace TS.FW.Dac.Cfg
{
    public class ConfigNTx : IBizNTxBase
    {
        public Response<ConfigEntity> GetConfigByName(string group, string name)
        {
            try
            {
                using (var dac = new ConfigDac())
                {
                    return new Response<ConfigEntity>(dac.SelectedForName(group, name));
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
