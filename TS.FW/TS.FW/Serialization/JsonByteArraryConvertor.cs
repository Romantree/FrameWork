using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Serialization
{
    /// <summary>
    /// Json Byte[] 변환 클래스
    /// </summary>
    public class JsonByteArraryConvertor : JsonConverter
    {
        /// <summary>
        /// 변환 가능 여부
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(byte[]);
        }

        /// <summary>
        /// Json Deserializer
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                if (reader.TokenType == Newtonsoft.Json.JsonToken.StartArray)
                {
                    var byteList = new List<byte>();

                    while (reader.Read())
                    {
                        switch (reader.TokenType)
                        {
                            case JsonToken.Integer:
                                byteList.Add(Convert.ToByte(reader.Value));
                                break;
                            case JsonToken.EndArray:
                                return byteList.ToArray();
                            case JsonToken.Comment:
                                break;
                            default:
                                throw new Exception(string.Format("Unexpected token when reading bytes: {0}", reader.TokenType));

                        }
                    }

                    throw new Exception("Unexpected end when reading bytes.");
                }
                else
                {
                    throw new Exception(string.Format("Unexpected token parsing binary. Expected StartArray, got {0}.", reader.TokenType));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                throw ex;
            }
        }

        /// <summary>
        /// Json Serializer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                if (value == null)
                {
                    writer.WriteNull();
                    return;
                }

                var data = (byte[])value;

                writer.WriteStartArray();

                for (var i = 0; i < data.Length; i++)
                {
                    writer.WriteValue(data[i]);
                }

                writer.WriteEndArray();
            }
            catch (Exception ex)
            {
                Logger.Write(this, ex);
                throw ex;
            }
        }
    }
}
