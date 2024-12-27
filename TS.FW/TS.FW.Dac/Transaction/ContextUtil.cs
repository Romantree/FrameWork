using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TS.FW.Dac.Transaction
{
    /// <summary>
    /// 설  명 : Context 관리를 위한 Class
    /// </summary>
    public class ContextUtil
    {
        /// <summary>
        /// 설  명 : Transaction 을 중지하고 롤백 시킨다.
        /// </summary>
        public static void SetAbort()
        {
            var iprop = Thread.CurrentContext.GetProperty(TrasactionScopeProperty.PROPERTY_NAME);
            var prop = iprop as TrasactionScopeProperty;
            prop.SetAbort();
        }

        /// <summary>
        /// 설  명 : Context개체 내의 내역을 Complete 시킨다.
        /// </summary>
        public static void SetComplete()
        {
            var iprop = Thread.CurrentContext.GetProperty(TrasactionScopeProperty.PROPERTY_NAME);
            var prop = iprop as TrasactionScopeProperty;
            prop.SetComplete();
        }
    }
}
