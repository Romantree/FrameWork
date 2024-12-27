using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TS.FW.Serialization
{
    public static class Serialization
    {
        #region XML Serializer

        /// <summary>
        /// XML Serializer 함수
        /// </summary>
        /// <param name="item"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Response<string> Serializer(this object item, Encoding encoding = null)
        {
            try
            {
                var encoder = (encoding == null) ? Encoding.UTF8 : encoding;

                using (var stream = new MemoryStream())
                {
                    var settting = new XmlWriterSettings() { Indent = true, Encoding = encoder };

                    using (var writer = XmlWriter.Create(stream, settting))
                    {
                        writer.WriteStartDocument(true);

                        var namespaces = new XmlSerializerNamespaces();
                        namespaces.Add(string.Empty, string.Empty);

                        var serializer = new XmlSerializer(item.GetType());
                        serializer.Serialize(writer, item, namespaces);
                    }

                    var xmlString = encoder.GetString(stream.ToArray()).Substring(1);

                    if (item is IXmlSerializationBase)
                    {
                        return new Response<string>((item as IXmlSerializationBase).SerializerEx(xmlString));
                    }
                    else
                    {
                        return new Response<string>(xmlString);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        /// <summary>
        /// Xml Deserializer 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static Response<T> Deserializer<T>(this string xmlString, Encoding encoding = null)
        {
            try
            {
                var encoder = (encoding == null) ? Encoding.UTF8 : encoding;

                using (var stream = new MemoryStream(encoder.GetBytes(xmlString)))
                {
                    var serializer = new XmlSerializer(typeof(T));

                    if (typeof(T).BaseType == typeof(IXmlSerializationBase))
                    {
                        serializer.UnknownElement += OnXmlElementEvent;
                        serializer.UnknownAttribute += OnXmlAttributeEvent;
                    }

                    return new Response<T>((T)serializer.Deserialize(stream));
                }
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        /// <summary>
        /// XML Serializer 함수 파일로 저장
        /// </summary>
        /// <param name="item"></param>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Response SerializerFile(this object item, string filePath, Encoding encoding = null)
        {
            try
            {
                var res = item.Serializer(encoding);

                if (res == false) return res;

                var xmlString = res.Result;

                var folderPath = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                if (File.Exists(filePath)) File.Delete(filePath);

                using (var stream = new StreamWriter(filePath))
                {
                    stream.Write(xmlString);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        /// <summary>
        /// Xml Deserializer 함수 파일에서 읽기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Response<T> DeserializerFile<T>(this string filePath, Encoding encoding = null)
        {
            try
            {
                using (var stream = new StreamReader(filePath, encoding == null ? Encoding.UTF8 : encoding))
                {
                    var xmlString = stream.ReadToEnd();

                    return xmlString.Deserializer<T>(encoding);
                }
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        private static void OnXmlElementEvent(object sender, XmlElementEventArgs e)
        {
            try
            {
                var obj = e.ObjectBeingDeserialized as IXmlSerializationBase;

                if (obj == null)
                {
                    Logger.Write(typeof(Serialization)
                        , string.Format("IXmlSerializationBase 클래스가 상속 되어 있지 않습니다. 클래스 명 : {0}", e.ObjectBeingDeserialized), Logger.LogEventLevel.Warning);

                    return;
                }

                obj.Add(e.Element);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(Serialization), ex);
            }
        }

        private static void OnXmlAttributeEvent(object sender, XmlAttributeEventArgs e)
        {
            try
            {
                var obj = e.ObjectBeingDeserialized as IXmlSerializationBase;

                if (obj == null)
                {
                    Logger.Write(typeof(Serialization)
                        , string.Format("IXmlSerializationBase 클래스가 상속 되어 있지 않습니다. 클래스 명 : {0}", e.ObjectBeingDeserialized), Logger.LogEventLevel.Warning);

                    return;
                }

                obj.Add(e.Attr);
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(Serialization), ex);
            }
        }

        #endregion

        #region DataContract Serializer

        /// <summary>
        /// DataContract Serializer 함수
        /// </summary>
        /// <param name="item"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Response<string> DataContractSerializer(this object item, Encoding encoding = null)
        {
            try
            {
                var encoder = (encoding == null) ? Encoding.UTF8 : encoding;

                using (var stream = new MemoryStream())
                {
                    var settting = new XmlWriterSettings() { Indent = true, Encoding = encoder };

                    using (var writer = XmlWriter.Create(stream, settting))
                    {
                        writer.WriteStartDocument(true);

                        var serializer = new DataContractSerializer(item.GetType());
                        serializer.WriteObject(writer, item);
                    }

                    return new Response<string>(encoder.GetString(stream.ToArray()));
                }
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        /// <summary>
        /// DataContract Deserializer 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static Response<T> DataContractDeserializer<T>(this string xmlString, Encoding encoding = null)
        {
            try
            {
                var encoder = (encoding == null) ? Encoding.UTF8 : encoding;

                using (var stream = new MemoryStream(encoder.GetBytes(xmlString)))
                {
                    //var serializer = new XmlSerializer(typeof(T));
                    var serializer = new DataContractSerializer(typeof(T));

                    return new Response<T>((T)serializer.ReadObject(stream));
                }
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        /// <summary>
        /// DataContract Serializer 함수 파일로 저장
        /// </summary>
        /// <param name="item"></param>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Response DataContractSerializerFile(this object item, string filePath, Encoding encoding = null)
        {
            try
            {
                var res = item.DataContractSerializer(encoding);

                if (res == false) throw new Exception(res.Comment);

                var xmlString = res.Result;

                var folderPath = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                if (File.Exists(filePath)) File.Delete(filePath);

                using (var stream = new StreamWriter(filePath))
                {
                    stream.Write(xmlString);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        /// <summary>
        /// Xml Deserializer 함수 파일에서 읽기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Response<T> DataContractDeserializerFile<T>(this string filePath, Encoding encoding = null)
        {
            try
            {
                using (var stream = new StreamReader(filePath, encoding == null ? Encoding.UTF8 : encoding))
                {
                    var xmlString = stream.ReadToEnd();

                    return xmlString.DataContractDeserializer<T>(encoding);
                }
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        #endregion

        #region Json Serializer

        /// <summary>
        /// Json Serializer 함수
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Response<string> JsonSerializer(this object item)
        {
            try
            {
                var setting = new JsonSerializerSettings()
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                    TypeNameHandling = TypeNameHandling.Objects,
                };

                return new Response<string>(JsonConvert.SerializeObject(item, item.GetType(), setting));
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        /// <summary>
        /// Json Deserializer 함수
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static Response<T> JsonDeserializer<T>(this string jsonString)
        {
            try
            {
                var setting = new JsonSerializerSettings()
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                    TypeNameHandling = TypeNameHandling.Objects,
                };

                return new Response<T>(JsonConvert.DeserializeObject<T>(jsonString, setting));
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        /// <summary>
        /// Json Deserializer 함수
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Response<object> JsonDeserializer(this string jsonString, Type type)
        {
            var setting = new JsonSerializerSettings()
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                TypeNameHandling = TypeNameHandling.Objects,
            };

            return new Response<object>(JsonConvert.DeserializeObject(jsonString, type, setting));
        }

        /// <summary>
        /// Json Serializer 함수 파일로 저장
        /// </summary>
        /// <param name="item"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Response JsonSerializerFile(this object item, string filePath)
        {
            try
            {
                var res = item.JsonSerializer();

                if (res == false) throw new Exception(res.Comment);

                var xmlString = res.Result;

                var folderPath = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                if (File.Exists(filePath)) File.Delete(filePath);

                using (var stream = new StreamWriter(filePath))
                {
                    stream.Write(xmlString);
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        /// <summary>
        /// Json Deserializer 함수 파일에서 읽기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Response<T> JsonDeserializerFile<T>(this string filePath)
        {
            try
            {
                using (var stream = new StreamReader(filePath))
                {
                    var xmlString = stream.ReadToEnd();

                    return xmlString.JsonDeserializer<T>();
                }
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        /// <summary>
        /// Json Deserializer 함수 파일에서 읽기
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Response<object> JsonDeserializerFile(this string filePath, Type type)
        {
            try
            {
                using (var stream = new StreamReader(filePath))
                {
                    var xmlString = stream.ReadToEnd();

                    return xmlString.JsonDeserializer(type);
                }
            }
            catch (Exception ex)
            {
                Logger.DebugWrite(typeof(Serialization), ex);
                return ex;
            }
        }

        #endregion
    }
}
