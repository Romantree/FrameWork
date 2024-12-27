using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TS.FW.Utils
{
    public class MutexQueue_T<T>
    {
        private const int MAX_COUNT = 1024;
        private object _obj;
        private Queue<T> _queue;

        /// <summary>
        /// Queue 의 총 개수
        /// </summary>
        public int Count
        {
            get
            {
                return this._queue.Count;
            }
        }

        public MutexQueue_T()
        {
            this._obj = new object();
            this._queue = new Queue<T>(1024);
        }

        /// <summary>
        /// </summary>
        public void Clear()
        {            
            //
            lock (this._obj)
            {
                this._queue.Clear();
            }
            if (Monitor.IsEntered(this._obj) == true)
                Monitor.PulseAll(this._obj);
        }

        /// <summary>
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public T Dequeue(int milliseconds)
        {
            T t;
            lock (this._obj)
            {
                if (this._queue.Count == 0)
                {
                    Monitor.Wait(this._obj, milliseconds);
                }
                if (this._queue.Count <= 0)
                {
                    T t1 = default(T);
                    return t1;
                }
                else
                {
                    t = (T)this._queue.Dequeue();
                }
            }
            return t;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            T t;
            lock (this._obj)
            {
                if (this._queue.Count == 0)
                {
                    Monitor.Wait(this._obj);
                }
                t = (T)this._queue.Dequeue();
            }
            return t;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public T DequeueNoWait()
        {
            T t;
            lock (this._obj)
            {
                if (this._queue.Count <= 0)
                {
                    T t1 = default(T);
                    return t1;
                }
                else
                {
                    t = (T)this._queue.Dequeue();
                }
            }
            return t;
        }

        /// <summary>
        /// </summary>
        /// <param name="o"></param>
        public void Enqueue(T o)
        {
            lock (this._obj)
            {
                if (this._queue.Count > MAX_COUNT)
                {
                    Logger.Write(this, "MutexQueue_T cleared");
                    this._queue.Clear();
                }
                this._queue.Enqueue(o);
                Monitor.PulseAll(this._obj);
            }
        }
    }
}
