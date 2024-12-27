using System;
using System.Windows.Threading;

namespace TS.FW.Wpf.Helper
{
    public static class DoEvent
    {
        private static DispatcherOperationCallback exitFrameCallback = new DispatcherOperationCallback(ExitFrame);
        private static readonly object _lock = new object();

        public static void DoEvents()
        {
            try
            {
                lock (_lock)
                {
                    var nestedFrame = new DispatcherFrame();

                    var exitOperation = Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, exitFrameCallback, nestedFrame);

                    Dispatcher.PushFrame(nestedFrame);

                    if (exitOperation.Status != DispatcherOperationStatus.Completed) exitOperation.Abort();
                }
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(DoEvent), ex);
            }
        }

        private static Object ExitFrame(Object state)
        {
            var frame = state as DispatcherFrame;
            frame.Continue = false;

            return null;
        }

        public static void Sleep(int ms)
        {
            for (int i = 0; i < ms; i++)
            {
                DoEvents();

                System.Threading.Thread.Sleep(1);
            }
        }
    }
}
