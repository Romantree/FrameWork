using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TS.FW.Utils;

namespace TS.FW.Plc.Dto.Packet.LS.Command
{
    public abstract class ILSCommandBase
    {
        public PlcCommandType CommandType { get; set; }

        public PlcDeviceCode DataType { get; set; }

        public const ushort RESERVED = 0X0000;
        
        public ushort reserved { get; private set; }

        public ushort VariablesCount { get; private set; }

        internal ILSCommandBase(PlcCommandType commandType, PlcDeviceCode dataType)
        {
            this.CommandType = commandType;
            this.DataType = dataType;
            this.reserved = ILSCommandBase.RESERVED;
        }

        public virtual IEnumerable<byte> ToByte()
        {
            foreach (var item in ((ushort)this.CommandType).ToByte(BitHandler.ByteOrder.LittleEndian))
            {
                yield return item;
            }

            foreach (var item in ((ushort)this.DataType).ToByte(BitHandler.ByteOrder.LittleEndian))
            {
                yield return item;
            }

            foreach (var item in this.reserved.ToByte(BitHandler.ByteOrder.LittleEndian))
            {
                yield return item;
            }
        }
    }

    public abstract class ILSReadCommandBase : ILSCommandBase
    {
        public int StartAddress { get; protected set; }

        public ILSReadCommandBase(PlcDeviceCode code) : base(PlcCommandType.LS_READ_REQUEST, code)
        {

        }
        
        public override IEnumerable<byte> ToByte()
        {
            foreach (var item in base.ToByte())
            {
                yield return item;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            return string.Format("COMMAND :\r\n- CommandType[{0}] : {1}", ((uint)this.CommandType).ToHex(), this.CommandType);
        }
        
    }

    public abstract class ILSWriteCommandBase : ILSCommandBase
    {
        public int StartAddress { get; protected set; }

        public ILSWriteCommandBase(PlcDeviceCode code) : base(PlcCommandType.LS_WRITE_REQUEST, code)
        {

        }

        public override IEnumerable<byte> ToByte()
        {
            foreach (var item in base.ToByte())
            {
                yield return item;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            return string.Format("COMMAND :\r\n- CommandType[{0}] : {1}", ((uint)this.CommandType).ToHex(), this.CommandType);
        }
    }


    public class ILSReadEachCommand : ILSReadCommandBase
    {
        public ushort VariableCount { get; private set; }

        public List<VariableSet> VarSet { get; private set; } = new List<VariableSet>();

        public ILSReadEachCommand(PlcBit bit) : base(PlcDeviceCode.LS_BIT_DATA_CODE)
        {
            this.StartAddress = bit.Address;
            this.VariableCount = 1;
            this.VarSet.Add(new VariableSet(bit, DataType));
        }

        public ILSReadEachCommand(IEnumerable<PlcBit> list) : base(PlcDeviceCode.LS_BIT_DATA_CODE)
        {
            this.StartAddress = list.OrderBy(t => t.Address).First().Address;
            this.VariableCount = (ushort)list.Count();

            foreach (var item in list)
            {
                this.VarSet.Add(new VariableSet(item, DataType));
            }
        }

        public ILSReadEachCommand(PlcWord word) : base(PlcDeviceCode.LS_WORD_DATA_CODE)
        {
            this.StartAddress = word.Address;
            this.VariableCount = 1;
            this.VarSet.Add(new VariableSet(word, DataType));
        }

        public ILSReadEachCommand(IEnumerable<PlcWord> list) : base(PlcDeviceCode.LS_WORD_DATA_CODE)
        {
            this.StartAddress = list.OrderBy(t => t.Address).First().Address;
            this.VariableCount = (ushort)list.Count();

            foreach (var item in list)
            {
                this.VarSet.Add(new VariableSet(item, DataType));
            }
        }
        
        public override IEnumerable<byte> ToByte()
        {
            foreach (var item in base.ToByte())
            {
                yield return item;
            }
            
            foreach (var item in this.VariableCount.ToByte())
            {
                yield return item;
            }

            foreach (var item in this.VarSet.SelectMany(t => t.ToByte()))
            {
                yield return item;
            }
        }
    }
    
    public class ILSReadContinueCommand : ILSReadCommandBase
    {
        public const ushort VARIABLE_COUNT = 0x0001;

        public ushort VariableCount { get; private set; }

        public List<VariableSet> VarSet { get; private set; } = new List<VariableSet>();

        public ushort DataByteSize { get; private set; }


        public ILSReadContinueCommand(IEnumerable<PlcBit> list) : base(PlcDeviceCode.LS_CONTINUE_DATA_CODE)
        {
            this.StartAddress = list.OrderBy(t => t.Address).First().Address;

            this.VariableCount = ILSReadContinueCommand.VARIABLE_COUNT;

            foreach (var item in list)
            {
                this.VarSet.Add(new VariableSet(item, DataType));
            }

            DataByteSize = (ushort)(list.Count());
        }

        public ILSReadContinueCommand(IEnumerable<PlcWord> list) : base(PlcDeviceCode.LS_CONTINUE_DATA_CODE)
        {
            this.StartAddress = list.OrderBy(t => t.Address).First().Address;

            this.VariableCount = ILSReadContinueCommand.VARIABLE_COUNT;

            foreach (var item in list)
            {
                this.VarSet.Add(new VariableSet(item, DataType));
                break;
            }

            var orderByList = list.OrderBy(t => t.Address);

            DataByteSize = (ushort)((orderByList.Sum(t => t.Length))*2);
        }

        public override IEnumerable<byte> ToByte()
        {
            foreach (var item in base.ToByte())
            {
                yield return item;
            }

            foreach (var item in this.VariableCount.ToByte(BitHandler.ByteOrder.LittleEndian))
            {
                yield return item;
            }

            foreach (var item in this.VarSet.SelectMany(t => t.ToByte()))
            {
                yield return item;
            }

            foreach (var item in this.DataByteSize.ToByte())
            {
                yield return item;
            }
        }
    }

    public class ILSWriteEachCommandBase : ILSWriteCommandBase
    {
        public ushort VariableCount { get; private set; }

        public List<VariableSet> VarSet { get; private set; } = new List<VariableSet>();

        public List<DatasSet> DataSet { get; private set; } = new List<DatasSet>();

        public ILSWriteEachCommandBase(PlcBit bit, PlcBit.Signal signal) : base(PlcDeviceCode.LS_BIT_DATA_CODE)
        {
            this.VariableCount = 1;
            this.VarSet.Add(new VariableSet(bit, DataType));
            this.DataSet.Add(new DatasSet(signal));
        }

        public ILSWriteEachCommandBase(Dictionary<PlcBit, PlcBit.Signal> list) : base(PlcDeviceCode.LS_BIT_DATA_CODE)
        {
            this.VariableCount = (ushort)list.Count();

            foreach (var item in list)
            {
                this.VarSet.Add(new VariableSet(item.Key, DataType));

                this.DataSet.Add(new DatasSet(item.Value));
            }
        }

        public ILSWriteEachCommandBase(PlcWord word, object value) : base(PlcDeviceCode.LS_WORD_DATA_CODE)
        {
            this.VariableCount = 1;
            this.VarSet.Add(new VariableSet(word, DataType));
            this.DataSet.Add(new DatasSet(value));
        }

        public ILSWriteEachCommandBase(Dictionary<PlcWord, object> list) : base(PlcDeviceCode.LS_WORD_DATA_CODE)
        {
            this.VariableCount = (ushort)list.Count();

            foreach (var item in list)
            {
                this.VarSet.Add(new VariableSet(item.Key, DataType));

                this.DataSet.Add(new DatasSet(item.Value));
            }
        }

        public override IEnumerable<byte> ToByte()
        {
            foreach (var item in base.ToByte())
            {
                yield return item;
            }

            foreach (var item in this.VariableCount.ToByte())
            {
                yield return item;
            }

            foreach (var item in this.VarSet.SelectMany(t => t.ToByte()))
            {
                yield return item;
            }

            foreach (var item in this.DataSet.SelectMany(t => t.ToByte()))
            {
                yield return item;
            }
        }
    }

    public class ILSWriteContinueCommandBase : ILSWriteCommandBase
    {
        public const ushort VARIABLE_COUNT = 0X0001;

        public ushort VariableCount { get; private set; }

        public List<VariableSet> VarSet { get; private set; } = new List<VariableSet>();

        public List<DatasSet> DataSet { get; private set; } = new List<DatasSet>();

        public ILSWriteContinueCommandBase(Dictionary<PlcBit, PlcBit.Signal> list) : base(PlcDeviceCode.LS_BIT_DATA_CODE)
        {
            this.StartAddress = list.OrderBy(t => t.Key.Address).First().Key.Address;

            this.VariableCount = ILSWriteContinueCommandBase.VARIABLE_COUNT;
            
            foreach (var item in list)
            {
                this.VarSet.Add(new VariableSet(item.Key, DataType));

                this.DataSet.Add(new DatasSet(item.Value));
            }
        }

        public ILSWriteContinueCommandBase(Dictionary<PlcWord, object> list) : base(PlcDeviceCode.LS_WORD_DATA_CODE)
        {
            this.StartAddress = list.OrderBy(t => t.Key.Address).First().Key.Address;

            this.VariableCount = ILSWriteContinueCommandBase.VARIABLE_COUNT;

            foreach (var item in list)
            {
                this.VarSet.Add(new VariableSet(item.Key, DataType));

                this.DataSet.Add(new DatasSet(item.Value));
            }
        }
        
        public override IEnumerable<byte> ToByte()
        {
            foreach (var item in base.ToByte())
            {
                yield return item;
            }

            foreach (var item in this.VariableCount.ToByte())
            {
                yield return item;
            }

            foreach (var item in this.VarSet.SelectMany(t => t.ToByte()))
            {
                yield return item;
            }

            foreach (var item in this.DataSet.SelectMany(t => t.ToByte()))
            {
                yield return item;
            }
        }
    }

    public class VariableSet
    {
        public ushort Length { get; private set; }

        public byte[] Name { get; private set; }

        public string NameString
        {
            get
            {
                return Encoding.ASCII.GetString(this.Name);
            }
        }

        public VariableSet(PlcBit bit, PlcDeviceCode code)
        {
            var value = "";
            int Add = bit.Address;

            if (code == PlcDeviceCode.LS_CONTINUE_DATA_CODE)
            {
                value = string.Format("%MB{0}", ((Add / 8) + (Add % 8) * (0.1D)));
            }
            else
            {
                value = string.Format("%MX{0}", Convert.ToString(Add));
            }

            this.Name = Encoding.ASCII.GetBytes(value);
            this.Length = (ushort)value.Length;
        }

        public VariableSet(PlcWord word, PlcDeviceCode code)
        {
            var value = "";
            int Add = word.Address;

            if (code == PlcDeviceCode.LS_CONTINUE_DATA_CODE)
            {
                if (word.Code == PlcDeviceCode.R_FILE_ACCESS_REGISTER)
                {
                    value = string.Format("%RB{0}", Add * 2);
                }
                else
                {
                    value = string.Format("%MB{0}", Add * 2);
                }
            }
            else
            {
                if (word.Code == PlcDeviceCode.R_FILE_ACCESS_REGISTER)
                {
                    value = string.Format("%RW{0}", Convert.ToString(Add));
                }
                else
                {
                    value = string.Format("%MW{0}", Convert.ToString(Add));
                }
            }

            this.Name = Encoding.ASCII.GetBytes(value);
            this.Length = (ushort)value.Length;
        }

        public IEnumerable<byte> ToByte()
        {
            foreach (var len in Length.ToByte())
            {
                yield return len;
            }

            foreach (var item in Name)
            {
                yield return item;
            }
        }

        public static implicit operator byte[] (VariableSet value)
        {
            return value.ToByte().ToArray();
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", this.NameString, this.Length);
        }
    }

    public class DatasSet
    {
        public ushort Value;
        public ushort Size;

        public DatasSet(PlcBit.Signal signal)
        {
            this.Value = (ushort)signal;
            this.Size = 0x0200;
        }

        public DatasSet(object data)
        {
            this.Value = (ushort)data;
            this.Size = (ushort)(data.ToString().Length);
        }

        public IEnumerable<byte> ToByte()
        {
            foreach (var len in Size.ToByte())
            {
                yield return len;
            }

            foreach (var name in Value.ToByte())
            {
                yield return name;
            }
        }

        public static implicit operator byte[] (DatasSet value)
        {
            return value.ToByte().ToArray();
        }
    }
}
