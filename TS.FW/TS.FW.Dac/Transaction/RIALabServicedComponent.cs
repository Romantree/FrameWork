using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Dac.Transaction
{
    /// <summary>
    /// 설  명 : COM+의 ServiceComponet 와 같은 Base Clsss
    /// </summary>
    public class RIALabServicedComponent : ContextBoundObject, IDisposable
    {
        /// <summary>
        /// 관리되지 않는 리소스의 확보, 해제 또는 다시 설정과 관련된 응용 프로그램 정의 작업을 수행합니다.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
