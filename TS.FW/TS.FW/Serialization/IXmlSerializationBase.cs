using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TS.FW.Serialization
{
    public abstract class IXmlSerializationBase
    {
        internal List<XmlElement> _ElementList = new List<XmlElement>();
        internal List<XmlAttribute> _AttributeList = new List<XmlAttribute>();

        internal void Add(XmlElement item)
        {
            this._ElementList.Add(item);
        }

        internal void Add(XmlAttribute item)
        {
            this._AttributeList.Add(item);
        }

        internal string SerializerEx(string xmlString)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlString);

            var type = this.GetType();
            var list = doc.GetElementsByTagName(type.Name);

            if (list != null && list.Count > 0)
            {
                this.SerializerEx(doc, list[0]);
            }

            using (var writer = new StringWriter())
            {
                using (var xmlWriter = new XmlTextWriter(writer) { Formatting = Formatting.Indented })
                {
                    doc.WriteTo(xmlWriter);
                }

                return writer.ToString();
            }
        }

        internal void SerializerEx(XmlDocument doc, XmlNode node)
        {
            var type = this.GetType();

            foreach (var pro in type.GetProperties())
            {
                if (pro.PropertyType.BaseType != typeof(IXmlSerializationBase)) continue;

                var list = doc.GetElementsByTagName(pro.Name);

                if (list == null || list.Count <= 0) continue;

                var item = pro.GetValue(this) as IXmlSerializationBase;
                if (item == null) continue;

                item.SerializerEx(doc, list[0]);
            }

            foreach (var element in _ElementList)
            {
                node.AppendChild(doc.ImportNode(element, true));
            }

            foreach (var attribute in _AttributeList)
            {
                node.Attributes.Append(doc.ImportNode(attribute, true) as XmlAttribute);
            }
        }
    }
}
