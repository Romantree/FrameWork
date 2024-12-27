using System;
using System.Runtime.Remoting.Contexts;
using System.Transactions;

namespace TS.FW.Dac.Transaction
{
    /// <summary>
    /// 설  명 : RIALabTransactionAttribute는 System.Transaction 에 있는 
    ///          TransactionScope를 이용한 Transaction을 자동처리 해주는 Attribute를 생성해 줍니다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    internal class RIALabTransactionAttribute : Attribute, IContextAttribute, IDisposable
    {
        /// <summary>
        /// 설  명 : TransactionOption에 대한 옵션
        /// </summary>
        public TransactionScopeOption Option { get; private set; }

        public RIALabTransactionAttribute() : this(TransactionScopeOption.Required)
        {

        }

        public RIALabTransactionAttribute(TransactionScopeOption option)
        {
            this.Option = option;
        }

        /// <summary>
        /// 설  명 : NewContext 발생시 TransactionScopeProperty를 생성시킨다.
        /// </summary>
        /// <param name="msg"></param>
        public void GetPropertiesForNewContext(System.Runtime.Remoting.Activation.IConstructionCallMessage msg)
        {
            // Transaction Property 셋팅
            var prop = new TrasactionScopeProperty(this.Option);
            msg.ContextProperties.Add(prop);
        }

        /// <summary>
        /// 지정된 된 컨텍스트 컨텍스트 특성의 요구 사항을 충족 하는지 여부를 나타내는 부울 값을 반환 합니다.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool IsContextOK(Context ctx, System.Runtime.Remoting.Activation.IConstructionCallMessage msg)
        {
            var prop = ctx.GetProperty(TrasactionScopeProperty.PROPERTY_NAME) as TrasactionScopeProperty;
            if (prop == null) return false;

            return true;
        }

        /// <summary>
        /// 관리되지 않는 리소스의 확보, 해제 또는 다시 설정과 관련된 응용 프로그램 정의 작업을 수행합니다.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
