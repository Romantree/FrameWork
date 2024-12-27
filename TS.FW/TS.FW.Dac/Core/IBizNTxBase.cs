using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Transaction;

namespace TS.FW.Dac.Core
{
    public abstract class IBizNTxBase : RIALabServicedComponent
    {
        public IBizNTxBase()
        {
        }

        public new void Dispose()
        {
            this.Dispose();
        }
    }
}
