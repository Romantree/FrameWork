using System;
using System.Threading;
using TS.FW.Dac.Transaction;

namespace TS.FW.Dac.Core
{
    [RIALabTransaction]
    public abstract class IBizRTxBase : RIALabServicedComponent, IDisposable
    {
        private static ReaderWriterLockSlim _WaitEvnet = new ReaderWriterLockSlim();

        public IBizRTxBase()
        {
            _WaitEvnet.EnterWriteLock();
        }

        public new void Dispose()
        {
            base.Dispose();
            _WaitEvnet.ExitWriteLock();
        }

        public void SetAbort()
        {
            ContextUtil.SetAbort();
        }
    }
}
