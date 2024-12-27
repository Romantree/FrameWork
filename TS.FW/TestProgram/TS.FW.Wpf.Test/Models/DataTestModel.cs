using System.Runtime.Serialization;
using TS.FW.Wpf.Core;

namespace TS.FW.Wpf.Test.Models
{
    [DataContract]
    public class DataTestModel : DataModelBase
    {
        [DataMember(Order = 1, Name = "Data1")]
        public sbyte SByte { get => this.GetValue<sbyte>(); set => this.SetValue(value); }

        [DataMember(Order = 2, Name = "Data2")]
        public short Shot { get => this.GetValue<short>(); set => this.SetValue(value); }

        [DataMember(Order = 3, Name = "Data3")]
        public int Int { get => this.GetValue<int>(); set => this.SetValue(value); }

        [DataMember(Order = 4, Name = "Data4")]
        public long Long { get => this.GetValue<long>(); set => this.SetValue(value); }

        [DataMember(Order = 5, Name = "Data5")]
        public byte Byte { get => this.GetValue<byte>(); set => this.SetValue(value); }

        [DataMember(Order = 6, Name = "Data6")]
        public ushort UShot { get => this.GetValue<ushort>(); set => this.SetValue(value); }

        [DataMember(Order = 7, Name = "Data7")]
        public uint UInt { get => this.GetValue<uint>(); set => this.SetValue(value); }

        [DataMember(Order = 8, Name = "Data8")]
        public ulong ULong { get => this.GetValue<ulong>(); set => this.SetValue(value); }

        [DataMember(Order = 9, Name = "Data9")]
        public float Float { get => this.GetValue<float>(); set => this.SetValue(value); }

        [DataMember(Order = 10, Name = "Data10")]
        public double Double { get => this.GetValue<double>(); set => this.SetValue(value); }

        [DataMember(Order = 11)]
        public string String { get => this.GetValue<string>(); set => this.SetValue(value); }

        public DataTestModel()
        {
            this.String = string.Empty;
        }
    }
}
