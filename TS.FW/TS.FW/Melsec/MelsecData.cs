namespace TS.FW.Melsec
{
    public class MelsecData
    {
        /// <summary>
        /// Melsec Device Type : LB - Bit / LW - Word
        /// </summary>
        public MDevType DevType { get; set; }

        /// <summary>
        /// Melsec Address
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        /// Melsec Data
        /// </summary>
        public short Data { get; set; }

        /// <summary>
        /// Melsec DevType, Address, Data를 문자열로 취합
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} 0x{1:X} : [{2:X} / {2}]", this.DevType, this.Address, this.Data);
        }
    }
}
