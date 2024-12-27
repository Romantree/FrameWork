using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Diagnostics;

namespace TS.FW.XCom.SDC
{
    public class SdcXComManager : XComManager
    {
        private static readonly string ROOT_CFG_PATH;
        private static readonly Assembly CurrentAssembly;

        static SdcXComManager()
        {
            ROOT_CFG_PATH = Path.Combine(Environment.CurrentDirectory, "Assets");
            CurrentAssembly = typeof(SdcXComManager).Assembly;
        }

        public SdcXComManager(string project, string version) : base(Path.Combine(ROOT_CFG_PATH, project, version, "XCom.cfg"), CurrentAssembly, string.Format("TS.FW.XCom.SDC.{0}.{1}", project, version))
        {
            
        }
    }
}
