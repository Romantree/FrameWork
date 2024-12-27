using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Dac.Transaction
{
    /// <summary>
    /// 설  명 : COM+ AutoComplete 를 사용하여 작성한 Method의 수정작업을 최소화 하기 위해
    ///          작동없는 Attribute 생성
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal class AutoCompleteAttribute : Attribute
    { }
}
