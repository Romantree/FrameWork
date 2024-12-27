using System;
using System.Collections.Generic;
using System.Linq;
using TS.FW.Device.Dto.DInOut;

namespace TS.FW.Device.Simulation
{
    public class SimulationDInOut : IDInOut
    {
        private readonly List<DInOutBit> _InOutList = new List<DInOutBit>();

        public Response<DInOutBit> ReadBitIn(int moduleNo, int bitNo)
        {
            try
            {
                var item = this.GetInOut(moduleNo, bitNo);

                return new Response<DInOutBit>(item);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<DInOutBit> ReadBitOut(int moduleNo, int bitNo)
        {
            try
            {
                var item = this.GetInOut(moduleNo, bitNo);

                return new Response<DInOutBit>(item);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<DInOutByte> ReadByteIn(int moduleNo, int offset)
        {
            try
            {
                var item = this.GetInOutByte(moduleNo, offset);

                return new Response<DInOutByte>(item);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<DInOutByte> ReadByteOut(int moduleNo, int offset)
        {
            try
            {
                var item = this.GetInOutByte(moduleNo, offset);

                return new Response<DInOutByte>(item);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<DInOutWord> ReadWordIn(int moduleNo, int offset)
        {
            try
            {
                var item = this.GetInOutWord(moduleNo, offset);

                return new Response<DInOutWord>(item);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<DInOutWord> ReadWordOut(int moduleNo, int offset)
        {
            try
            {
                var item = this.GetInOutWord(moduleNo, offset);

                return new Response<DInOutWord>(item);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<DInOutDWord> ReadDWordIn(int moduleNo)
        {
            try
            {
                var item = this.GetInOutDWord(moduleNo, 0);

                return new Response<DInOutDWord>(item);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response<DInOutDWord> ReadDWordOut(int moduleNo)
        {
            try
            {
                var item = this.GetInOutDWord(moduleNo, 0);

                return new Response<DInOutDWord>(item);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteBit(int moduleNo, int bitNo, eSignal signal)
        {
            try
            {
                var item = this.GetInOut(moduleNo, bitNo);
                item.Signal = signal;

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteByte(int moduleNo, int offset, eSignal signal)
        {
            try
            {
                var start = offset * 8;
                var end = start + 8;

                foreach (var item in this.GetInOut(moduleNo, start, end))
                {
                    item.Signal = signal;
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteWord(int moduleNo, int offset, eSignal signal)
        {
            try
            {
                var start = offset * 16;
                var end = start + 16;

                foreach (var item in this.GetInOut(moduleNo, start, end))
                {
                    item.Signal = signal;
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public Response WriteDWord(int moduleNo, eSignal signal)
        {
            try
            {
                var start = 0;
                var end = 32;

                foreach (var item in this.GetInOut(moduleNo, start, end))
                {
                    item.Signal = signal;
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private DInOutBit GetInOut(int moduleNo, int bitNo)
        {
            var item = this._InOutList.FirstOrDefault(t => t.ModuleNo == moduleNo && t.BitNo == bitNo);
            if (item == null)
            {
                item = new DInOutBit(moduleNo, bitNo, false);
                this._InOutList.Add(item);
            }

            return item;
        }

        private IEnumerable<DInOutBit> GetInOut(int moduleNo, int startBit, int endBit)
        {
            for (int i = startBit; i < endBit; i++)
            {
                yield return this.GetInOut(moduleNo, i);
            }
        }

        private DInOutByte GetInOutByte(int moduleNo, int offset)
        {
            var start = offset * 8;
            var end = start + 8;

            return new DInOutByte(moduleNo, start, this.ToByte(this.GetInOut(moduleNo, start, end)));
        }

        private DInOutWord GetInOutWord(int moduleNo, int offset)
        {
            var start = offset * 16;
            var end = start + 16;

            return new DInOutWord(moduleNo, start, this.ToUint16(this.GetInOut(moduleNo, start, end)));
        }

        private DInOutDWord GetInOutDWord(int moduleNo, int offset)
        {
            var start = offset * 32;
            var end = start + 32;

            return new DInOutDWord(moduleNo, start, this.ToUInt32(this.GetInOut(moduleNo, start, end)));
        }

        private byte ToByte(IEnumerable<DInOutBit> list)
        {
            return Convert.ToByte(string.Join("", list.OrderBy(t => t.BitNo).Select(t => t ? "1" : "0").Reverse()), 2);
        }

        private ushort ToUint16(IEnumerable<DInOutBit> list)
        {
            return Convert.ToUInt16(string.Join("", list.OrderBy(t => t.BitNo).Select(t => t ? "1" : "0").Reverse()), 2);
        }

        private uint ToUInt32(IEnumerable<DInOutBit> list)
        {
            return Convert.ToUInt32(string.Join("", list.OrderBy(t => t.BitNo).Select(t => t ? "1" : "0").Reverse()), 2);
        }
    }
}
