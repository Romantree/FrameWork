using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.JF.DTO
{
    public class HomeSettingInfo
    {
        /// <summary>
        /// Home 방향 CW, CCW
        /// </summary>
        public enDir Dir { get; set; }
        /// <summary>
        /// MoveSpeed
        /// </summary>
        public double StartVel { get; set; }
        /// <summary>
        /// MoveSpeed * 2
        /// </summary>
        public double MaxVel { get; set; }
        /// <summary>
        /// SearchSpeed
        /// </summary>
        public double HomeVel { get; set; }
        /// <summary>
        /// Acceleration
        /// </summary>
        public int Acc { get; set; }
        /// <summary>
        /// Home Mode ( 소스에서는 사용하지 않고 있음 )
        /// </summary>
        public int Mode { get; set; }
    }
}
