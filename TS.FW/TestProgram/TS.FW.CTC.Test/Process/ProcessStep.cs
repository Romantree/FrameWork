using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.CTC.Test.Process
{
    public class ProcessStep<T> where T : struct, IConvertible
    {
        private Type _Type => typeof(T);

        public T CurrentStep { get; private set; }

        public ProcessStep()
        {
            if (this._Type.IsEnum == false) throw new TypeAccessException(string.Format("Process Step이 Enum Type이 아닙니다. {0}", this._Type.Name));
        }

        public void Init()
        {
            this.CurrentStep = default(T);
        }

        public void Prev()
        {
            var next = (T)Enum.ToObject(this._Type, this.CurrentStep.ToInt32(CultureInfo.InvariantCulture) - 1);

            if (Enum.IsDefined(this._Type, next))
            {
                this.CurrentStep = next;
            }
            else
            {
                this.Init();
            }
        }

        public void Next(int stepNo = 1)
        {
            var next = (T)Enum.ToObject(this._Type, this.CurrentStep.ToInt32(CultureInfo.InvariantCulture) + stepNo);

            if (Enum.IsDefined(this._Type, next))
            {
                this.CurrentStep = next;
            }
            else
            {
                this.Init();
            }
        }

        public void Change(T step)
        {
            this.CurrentStep = step;
        }

        public StepResult ExcuteProcess(object obj, params object[] arg)
        {
            try
            {
                var info = obj.GetType().GetMethod(this.CurrentStep.ToString());
                if (info == null)
                {
                    Logger.Write("PROCESS", string.Format("해당 Step이 존재하지 않습니다. [{0}]", this.CurrentStep), Logger.LogEventLevel.Error);
                    return StepResult.Not_Found_Step;
                }

                return (StepResult)info.Invoke(obj, arg);
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                return StepResult.Error;
            }
        }

        public bool Contains(string value)
        {
            return this.CurrentStep.ToString().ToUpper().Contains(value);
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", this._Type.Name, this.CurrentStep);
        }

        public static implicit operator T(ProcessStep<T> item)
        {
            return item.CurrentStep;
        }

        public static implicit operator ProcessStep<T>(T step)
        {
            return new ProcessStep<T>() { CurrentStep = step };
        }
    }
}
