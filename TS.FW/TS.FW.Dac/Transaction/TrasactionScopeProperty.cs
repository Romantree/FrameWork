using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Transactions;

namespace TS.FW.Dac.Transaction
{
    /// <summary>
    /// 설  명 : Transaction Property, Attribute 가 활성화 되었을때 , Attribute의 값을 저장하고 있을 프로퍼티
    /// </summary>
    internal class TrasactionScopeProperty : IContextProperty, IContributeServerContextSink, IDisposable
    {
        public const string PROPERTY_NAME = "TransactionScopeProperty";

        public TransactionScope Transaction { get; private set; }

        public TransactionScopeOption Option { get; private set; }

        public TransactionScopeStatus Status { get; private set; }

        /// <summary>
        /// 추가 됩니다 컨텍스트 속성의 이름을 가져옵니다.
        /// </summary>
        public string Name { get => TrasactionScopeProperty.PROPERTY_NAME; }

        public TrasactionScopeProperty(TransactionScopeOption option)
        {
            this.Option = option;
        }

        /// <summary>
        /// 설  명 : Transaction 생성
        /// </summary>
        public void Open()
        {

            if (this.Status == TransactionScopeStatus.None || this.Status == TransactionScopeStatus.Destruct)
            {
                this.Transaction = new TransactionScope(this.Option, TimeSpan.FromSeconds(30));
                this.Status = TransactionScopeStatus.Construct;
            }
            else
            {
                throw new Exception("활성화된 트랜잭션이 Dispose가 안된상태에서 활성화 하려는 오류가 발생했습니다.");
            }

        }

        /// <summary>
        /// 컨텍스트 고정 되 면 호출 됩니다.
        /// </summary>
        /// <param name="newContext"></param>
        public void Freeze(Context newContext)
        {

        }

        /// <summary>
        /// 컨텍스트 속성은 새 컨텍스트 호환 여부를 나타내는 부울 값을 반환 합니다.
        /// </summary>
        /// <param name="newCtx"></param>
        /// <returns></returns>
        public bool IsNewContextOK(Context newCtx)
        {
            var prop = newCtx.GetProperty(TrasactionScopeProperty.PROPERTY_NAME) as TrasactionScopeProperty;

            if (prop == null) return false;
            if (prop.Transaction != this.Transaction) return false;

            return true;
        }

        /// <summary>
        /// 지금까지 구성 된 싱크 체인에서 첫 번째 싱크를 가져온 다음 해당 메시지 싱크 앞 이미 구성 된 체인에 연결
        /// </summary>
        /// <param name="nextSink"></param>
        /// <returns></returns>
        public IMessageSink GetServerContextSink(IMessageSink nextSink)
        {
            return new TransactionScopeSink(nextSink);
        }

        /// <summary>
        /// 설  명 : Transaction Complete 처리
        /// </summary>
        public void SetComplete()
        {
            //// 현 프로퍼티 상태를 체크            
            if (Status == TransactionScopeStatus.Construct)
            {
                this.Transaction.Complete();
                this.Dispose();
            }
        }

        /// <summary>
        /// 설  명 : Transaction Abort 처리
        /// </summary>
        public void SetAbort()
        {
            this.Dispose();
        }

        /// <summary>
        /// 설  명 : Transaction 소멸 처리
        /// </summary>
        public void Dispose()
        {
            this.Transaction.Dispose();
            this.Status = TransactionScopeStatus.Destruct;
        }
    }
}
