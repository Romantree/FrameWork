using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Dac.Core
{
    public class TsSqlParameter
    {
        public string ParameterName { get; private set; }

        public string DatabaseType { get; private set; }

        public object Value { get; set; }

        internal MySqlDbType MySqlDbType { get => (MySqlDbType)Enum.Parse(typeof(MySqlDbType), this.DatabaseType); }

        public TsSqlParameter(string parameterName, string databaseType, object value)
        {
            this.ParameterName = parameterName;
            this.DatabaseType = databaseType;
            this.Value = value;
        }

        public TsSqlParameter(string parameterName, object value)
        {
            this.ParameterName = parameterName;
            this.Value = value;
        }

        public TsSqlParameter(string parameterName, TsSqlDbType databaseType, object value)
        {
            this.ParameterName = parameterName;
            this.DatabaseType = databaseType.ToString();
            this.Value = value;
        }

        public override string ToString()
        {
            return $"{this.ParameterName} = {this.Value}";
        }
    }

    public class TsSqlParameterCollection : List<TsSqlParameter>
    {
        public TsSqlParameter this[string name]
        {
            get
            {
                if(this.Any(t=>t.ParameterName == name) == false)
                {
                    this.Add(new TsSqlParameter(name, null));
                }

                return this.FirstOrDefault(t => t.ParameterName == name);
            }
        }
    }

    public enum TsSqlDbType
    {
        Decimal = 0,
        Byte = 1,
        Int16 = 2,
        Int32 = 3,
        Float = 4,
        Double = 5,
        Timestamp = 7,
        Int64 = 8,
        Int24 = 9,
        Date = 10,
        Time = 11,
        DateTime = 12,
        Datetime = 12,
        Year = 13,
        Newdate = 14,
        VarString = 15,
        Bit = 16,
        JSON = 245,
        NewDecimal = 246,
        Enum = 247,
        Set = 248,
        TinyBlob = 249,
        MediumBlob = 250,
        LongBlob = 251,
        Blob = 252,
        VarChar = 253,
        String = 254,
        Geometry = 255,
        UByte = 501,
        UInt16 = 502,
        UInt32 = 503,
        UInt64 = 508,
        UInt24 = 509,
        Binary = 600,
        VarBinary = 601,
        TinyText = 749,
        MediumText = 750,
        LongText = 751,
        Text = 752,
        Guid = 800
    }
}
