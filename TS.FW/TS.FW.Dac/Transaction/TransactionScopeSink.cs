using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace TS.FW.Dac.Transaction
{
    /// <summary>
    /// 설  명 : Context객체와의 MessageSink 처리
    /// </summary>
    internal class TransactionScopeSink : IMessageSink
    {
        /// <summary>
        /// 싱크 체인의 다음 메시지 싱크를 가져옵니다.
        /// </summary>
        public IMessageSink NextSink { get; private set; }

        public TransactionScopeSink(IMessageSink nextSink)
        {
            this.NextSink = nextSink;
        }

        /// <summary>
        /// 지정된 된 메시지를 비동기적으로 처리 합니다.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="replySink"></param>
        /// <returns></returns>
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new NotSupportedException("비동기는 지원하지 않습니다.");
        }

        /// <summary>
        /// 지정된 된 메시지를 동기적으로 처리 합니다.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public IMessage SyncProcessMessage(IMessage msg)
        {
            TrasactionScopeProperty prop = null;

            try
            {
                var ctorMsg = msg as IConstructionCallMessage;

                if (ctorMsg == null)
                {
                    var list = Thread.CurrentContext.ContextProperties;
                    var iprop = Thread.CurrentContext.GetProperty(TrasactionScopeProperty.PROPERTY_NAME);
                    prop = iprop as TrasactionScopeProperty;

                    prop.Open();
                }

                var retMsg = this.NextSink.SyncProcessMessage(msg);
                this.InspectReturnMessageAndLogException(retMsg);

                if (ctorMsg == null) prop.SetComplete();

                return retMsg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 설  명 : Exception 발생시 해당 내역에 대한 Log 처리 및 Transaction 취소 처리
        /// </summary>
        /// <param name="retmsg"></param>
        private void InspectReturnMessageAndLogException(IMessage retmsg)
        {
            var mrm = new MethodReturnMessageWrapper((IMethodReturnMessage)retmsg);

            if (mrm.Exception != null)
            {
                var iprop = Thread.CurrentContext.GetProperty(TrasactionScopeProperty.PROPERTY_NAME);
                var prop = iprop as TrasactionScopeProperty;
                prop.SetAbort();
            }
        }
    }
}
