using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TS.FW.Diagnostics
{
    public class DelegateUtils
    {
        public DelegateUtils()
        {
        }
        /// <summary>
        /// </summary>
        /// <param name="dlg"></param>
        /// <returns></returns>
        public static System.Windows.Forms.Control GetControlTarget(Delegate dlg)
        {
            return dlg.Target as System.Windows.Forms.Control;
        }

        /// <summary>
        /// </summary>
        /// <param name="dlg"></param>
        /// <returns></returns>
        public static bool IsControlInstance(Delegate dlg)
        {
            return dlg.Target is System.Windows.Forms.Control;
        }
    }

    public class MyTimer_I<I>
    {
        private object obj;
        public Dictionary<string, I> keepings;
        private Dictionary<string, Timer> runnings;

        public delegate void DelegateTimeout(I o);
        public event DelegateTimeout OnTimeout;

        public int LiveCount => this.runnings.Count;

        public MyTimer_I()
        {
            obj = new object();
            runnings = new Dictionary<string, Timer>();
            keepings = new Dictionary<string, I>();
        }

        void CheckPrevSchedule(string id)
        {
            if (keepings.ContainsKey(id))
            {
                Stop(id);
            }
        }

        Timer MakeTimer(int interval, I o, bool isRepeat)
        {
            TimerCallback timecallback = new TimerCallback(this.OnUserTimeout);
            return (isRepeat ? new Timer(timecallback, o, interval, interval) : new Timer(timecallback, o, interval, -1));
        }

        void OnUserTimeout(object o)
        {
            try
            {
                if (!DelegateUtils.IsControlInstance(this.OnTimeout))
                {
                    this.OnTimeout((I)o);
                    return;
                }
                else
                {
                    DelegateUtils.GetControlTarget(this.OnTimeout).BeginInvoke(new DelegateTimeout(this.OnTimeout.Invoke), o);
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }

        }

        public I GetData(object id)
        {
            return keepings[id.ToString()];
        }

        public bool IsLive(object id)
        {
            return runnings.ContainsKey(id.ToString());
        }

        public bool IsLive()
        {
            return runnings.Count != 0;
        }

        public void Start(int interval, I o)
        {
            if (IsLive(o.ToString()) == false) //해당 타이머가 돌고 있다면 다시 스타트 하면 안됨. 
                this.Start(interval, o, o.ToString());
        }

        public void Start(int interval, I o, string id)
        {
            lock (obj)
            {
                CheckPrevSchedule(id);
                keepings.Add(id, o);
                var t = this.MakeTimer(interval, o, true);
                runnings.Add(id, t);
            }
        }

        public void Stop(I o)
        {
            this.Stop(o.ToString());
        }

        public void Stop(string id)
        {
            if (runnings.ContainsKey(id))
            {
                lock (obj)
                {
                    runnings[id].Dispose();
                    runnings.Remove(id);
                    keepings.Remove(id);
                }
            }
        }

        public void StopAll()
        {
            lock (obj)
            {
                foreach (Timer timer in runnings.Values)
                {
                    timer.Dispose();
                }
                runnings.Clear();
                keepings.Clear();
            }
        }
    }

    /// <summary>
    /// ID, Attachment User Timer
    /// </summary>
    /// <typeparam name="I">ID</typeparam>
    /// <typeparam name="A">Attachment</typeparam>
    public class MyTimer_I_A<I, A>
    {
        public delegate void TimeoutDelegate(I id, A attachment);
        public event TimeoutDelegate OnTimeout;

        private object obj = new object();

        private Dictionary<I, TaskOperation> taskOperations = new Dictionary<I, TaskOperation>();

        private void NotifyListeners(object objParam)
        {
            lock (obj)
            {
                TaskOperation taskOperation = objParam as TaskOperation;
                if (!DelegateUtils.IsControlInstance(OnTimeout))
                {
                    OnTimeout(taskOperation.Id, taskOperation.Attachment);
                }
                else
                {
                    object[] id = new object[2];
                    id[0] = taskOperation.Id;
                    id[1] = taskOperation.Attachment;
                    DelegateUtils.GetControlTarget(OnTimeout).BeginInvoke(new TimeoutDelegate(this.OnTimeout.Invoke), id);
                }
            }
        }

        private void NotifyListenersOnce(object objParam)
        {
            lock (obj)
            {
                NotifyListeners(objParam);
                TaskOperation taskOperation = objParam as TaskOperation;
                Stop(taskOperation.Id);
            }
        }

        private void CheckPrevSchedule(I id)
        {
            if(this.OnTimeout == null)
                throw new Exception("OnTimeout delegate is null");

            if (taskOperations.ContainsKey(id))
            {
                Stop(id);
            }
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            StopAll();
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public A GetAttachment(I id)
        {
            if (taskOperations.ContainsKey(id))
            {
                return taskOperations[id].Attachment;
            }
            else
            {
                A a = default(A);
                return a;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HasId(I id)
        {
            return taskOperations.ContainsKey(id);
        }

        /// <summary>
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="id"></param>
        public void StartOnce(int interval, I id)
        {
            A a = default(A);
            this.StartOnce(interval, id, a);
        }

        /// <summary>
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="id"></param>
        /// <param name="attachment"></param>
        public void StartOnce(int interval, I id, A attachment)
        {
            if (interval >= 1)
            {
                lock (obj)
                {
                    CheckPrevSchedule(id);
                    var taskOperation = new TaskOperation();
                    taskOperation.Id = id;
                    taskOperation.Attachment = attachment;
                    TimerCallback timerCallback = new TimerCallback(NotifyListenersOnce);
                    Timer timer = new Timer(timerCallback, taskOperation, interval, interval);
                    taskOperation.Timer = timer;
                    taskOperations.Add(id, taskOperation);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="id"></param>
        public void StartRepeat(int interval, I id)
        {
            A a = default(A);
            StartRepeat(interval, id, a);
        }

        /// <summary>
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="id"></param>
        /// <param name="attachment"></param>
        public void StartRepeat(int interval, I id, A attachment)
        {
            if (interval >= 1)
            {
                lock (obj)
                {
                    var taskOperation = new TaskOperation();
                    taskOperation.Id = id;
                    taskOperation.Attachment = attachment;
                    TimerCallback timerCallback = new TimerCallback(this.NotifyListeners);
                    Timer timer = new Timer(timerCallback, taskOperation, interval, interval);
                    taskOperation.Timer = timer;
                    taskOperations.Add(id, taskOperation);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public A Stop(I id)
        {
            A attachment;
            lock (obj)
            {
                if (taskOperations.ContainsKey(id))
                {
                    var item = taskOperations[id];
                    item.Timer.Dispose();
                    taskOperations.Remove(id);
                    attachment = item.Attachment;
                }
                else
                {
                    A a = default(A);
                    attachment = a;
                }
            }
            return attachment;
        }

        /// <summary>
        /// Stop method가 map을 지우므로 사용하지 않고 dispose, clear처리.
        /// </summary>
        public void StopAll()
        {
            lock (obj)
            {
                foreach (TaskOperation value in taskOperations.Values)
                {
                    value.Timer.Dispose();
                }
                taskOperations.Clear();
            }
        }


        private class TaskOperation
        {
            private A attachment;
            public A Attachment
            {
                get { return attachment; }
                set { attachment = value; }
            }

            private I id;
            public I Id
            {
                get { return id; }
                set { id = value; }
            }

            private Timer timer;
            public Timer Timer
            {
                get { return timer; }
                set { timer = value; }
            }
        }

    }
}
