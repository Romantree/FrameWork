using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Dac.Transaction
{
    internal enum TransactionScopeStatus
    {
        None,       // 상태없음
        Construct,  // 생성처리
        Destruct    // 소멸처리
    }
}
