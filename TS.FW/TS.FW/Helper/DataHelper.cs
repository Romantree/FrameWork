using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Helper
{
    public class DataHelper
    {
        public static double ToDigit(double voltage, double minVoltage, double maxVoltage, double maxDigit = ushort.MaxValue)
        {
            return maxDigit * ((voltage - minVoltage) / (maxVoltage - minVoltage));
        }
    }
}
