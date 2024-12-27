using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Utils;

namespace TS.FW.JF.DIO
{
    public class DInOutByte : IDInOutBase
    {
        internal DInOutByte(int moduleNo, int startBitNo, byte data) : base(moduleNo, startBitNo, 8)
        {
            this.SetData(data.ToBinary());
        }
    }
}
