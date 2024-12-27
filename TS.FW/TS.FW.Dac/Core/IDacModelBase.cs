using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Dac.Core
{
    public abstract class IDacModelBase<T>
    {
        public abstract void UpdateModel(T model);

        public virtual new bool Equals(object obj)
        {
            if (object.Equals(this, null) && object.Equals(obj, null)) return true;
            if (object.Equals(this, null) || object.Equals(obj, null)) return false;

            return this.GetHashCode() == obj.GetHashCode();
        }
    }
}
