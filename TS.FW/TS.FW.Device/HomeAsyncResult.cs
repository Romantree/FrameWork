using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Device
{
    public class HomeAsyncResult : Response
    {
        public bool Complete { get; set; } = false;

        public bool IsStop { get; set; } = false;

        public HomeAsyncResult() : base() { }

        public HomeAsyncResult(Exception ex) : base(ex) { }

        public HomeAsyncResult(bool success, string comment) : base(success, comment) { }

        public HomeAsyncResult(bool success, string format, params object[] parm) : base(success, format, parm) { }
    }
}
