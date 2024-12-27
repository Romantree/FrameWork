using TS.FW.Utils;

namespace TS.FW.Plc.Dto
{
    public abstract class IPlcDataBase
    {
        public int Address { get; private set; }

        public string Name { get; private set; }

        public IPlcDataBase(int address, string name)
        {
            this.Address = address;
            this.Name = name;
        }

        public virtual string ToAddress()
        {
            return string.Format("{0}[{1}]", this.Name, this.Address.ToHex());
        }

        public virtual string AddressString()
        {
            return this.Address.ToHex();
        }

        public abstract string ToValueString();

        public override string ToString()
        {
            return this.ToAddress();
        }

        public override int GetHashCode()
        {
            return this.Address;
        }

        public override bool Equals(object obj)
        {
            if (object.Equals(this, null) && object.Equals(obj, null)) return true;
            if (object.Equals(this, null)) return false;
            if (object.Equals(obj, null)) return false;

            return this.GetHashCode() == obj.GetHashCode();
        }
    }
}

