using System;

namespace TS.FW.Melsec
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MelsecAttribute : Attribute
    {
        public int No { get; private set; }

        public MDataFormat Format { get; set; }

        public MDevType DevType { get; set; } = MDevType.LW;

        public int Lenght { get; set; } = 1;

        public MelsecAttribute(int no, MDataFormat format = MDataFormat.Shot)
        {
            this.No = no;
            this.Format = format;
            this.Lenght = this.ToLenght(format);
        }

        private int ToLenght(MDataFormat format)
        {
            switch (format)
            {
                case MDataFormat.Bool:
                case MDataFormat.Shot: return 1;
                case MDataFormat.Int:
                case MDataFormat.Flat: return 2;
                case MDataFormat.Long:
                case MDataFormat.Double: return 4;
            }

            return 0;
        }
    }
}
