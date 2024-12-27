using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using XCOMLib;

namespace TS.FW.XCom.Core
{
    public class XComItem
    {
        public XComMessage Message { get; private set; }

        public eXComData Type { get; private set; }

        public int? Length { get; private set; }

        public string Name { get; private set; }

        public List<XComItem> SubItem { get; private set; } = new List<XComItem>();

        private XComItem(string item, XComMessage msg)
        {
            this.Message = msg;
            this.SetInfo(item);
        }

        private XComItem(IEnumerable<XComItemStep> contents) : this(contents.First().Item, contents.First().Message)
        {
            this.SetSubItem(contents.Skip(1), 1);
        }

        public bool CheckFormat(IXComData data)
        {
            if (this.Type != data.Type) return false;

            if(this.Type == eXComData.LIST)
            {
                if(this.Length.HasValue)
                {
                    if (this.Length.Value != data.Length) return false;

                    for (int i = 0; i < this.Length.Value; i++)
                    {
                        var sItem = this.SubItem[i];
                        var sData = data.List.Values[i];

                        if (sItem.CheckFormat(sData) == false) return false;
                    }

                    return true;
                }
                else
                {
                    foreach (var item in data.List.Values)
                    {
                        if (SubItem[0].CheckFormat(item) == false) return false;
                    }

                    return true;
                }
            }
            else if(this.Type == eXComData.ASCII)
            {
                if(data.Type == eXComData.ASCII)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if(this.Length.HasValue)
                {
                    if(this.Length.Value == data.Length)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        internal void SetSendData(int msgId, XCOM xcom, object data, Type dataType)
        {
            var info = dataType.GetProperty(this.Name);

            if ((this.Type == eXComData.LIST && this.Length.HasValue) == false)
            {
                if (info == null) throw new Exception(string.Format("프로퍼티가 존재하지 않습니다. Property : {0}", this.Name));
            }

            var value = this.ToValue(info, data);

            switch (this.Type)
            {
                case eXComData.LIST:
                    {
                        if (this.Length.HasValue)
                        {
                            xcom.SetList(msgId, this.Length.Value);

                            foreach (var item in this.SubItem)
                            {
                                item.SetSendData(msgId, xcom, data, dataType);
                            }
                        }
                        else
                        {
                            var list = info.GetValue(data) as IList;
                            xcom.SetList(msgId, list.Count);

                            foreach (var item in list)
                            {
                                foreach (var sItem in this.SubItem)
                                {
                                    sItem.SetSendData(msgId, xcom, item, info.PropertyType.GetGenericArguments()[0]);
                                }
                            }
                        }
                    }
                    break;
                case eXComData.ASCII:
                    {
                        this.SetData(value as string, msgId, xcom);
                    }
                    break;
                case eXComData.BINARY:
                    {
                        this.SetData<byte>(value, msgId, xcom.SetBinary);
                    }
                    break;
                case eXComData.BOOL:
                    {
                        this.SetData<bool>(value, msgId, xcom.SetBool);
                    }
                    break;
                case eXComData.INT1:
                    {
                        this.SetData<byte>(value, msgId, xcom.SetINT1);
                    }
                    break;
                case eXComData.INT2:
                    {
                        this.SetData<short>(value, msgId, xcom.SetINT2);
                    }
                    break;
                case eXComData.INT4:
                    {
                        this.SetData<int>(value, msgId, xcom.SetINT4);
                    }
                    break;
                case eXComData.INT8:
                    {
                        this.SetData<int>(value, msgId, xcom.SetINT8);
                    }
                    break;
                case eXComData.UINT1:
                    {
                        this.SetData<byte>(value, msgId, xcom.SetUINT1);
                    }
                    break;
                case eXComData.UINT2:
                    {
                        this.SetData<int>(value, msgId, xcom.SetUINT2);
                    }
                    break;
                case eXComData.UINT4:
                    {
                        this.SetData<long>(value, msgId, xcom.SetUINT4);
                    }
                    break;
                case eXComData.UINT8:
                    {
                        this.SetData<long>(value, msgId, xcom.SetUINT8);
                    }
                    break;
                case eXComData.FLOAT4:
                    {
                        this.SetData<float>(value, msgId, xcom.SetFLOAT4);
                    }
                    break;
                case eXComData.FLOAT8:
                    {
                        this.SetData<double>(value, msgId, xcom.SetFLOAT8);
                    }
                    break;
                default:
                    throw new TypeAccessException(string.Format("정의 되지 않은 유형의 데이터 타입니다. XComDataType = {0}", this.Type));
            }
        }

        public string MakeJson(IXComData data)
        {
            if (data == null) return string.Empty;

            if (this.Type != data.Type) throw new TypeAccessException(string.Format("데이터 유형이 동일 하지 않습니다. Data={0}, Msg={1}", data.Type, this.Type));

            var sb = new StringBuilder();

            switch (this.Type)
            {
                case eXComData.LIST:
                    {
                        var item = data.List;

                        if (this.Length.HasValue)
                        {
                            sb.Append(string.Join(",", this.SubItem.Select(t => t.MakeJson(item.Next))));
                        }
                        else
                        {
                            sb.Append(string.Format("\"{0}\":", this.Name));
                            sb.Append("[");

                            var list = new List<string>();

                            for (int i = 0; i < item.Length; i++)
                            {
                                list.Add("{" + string.Join(",", this.SubItem.Select(t => t.MakeJson(item.Next))) + "}");
                            }

                            sb.Append(string.Join(",", list));

                            sb.Append("]");
                        }
                    }
                    break;
                case eXComData.BINARY:
                case eXComData.BOOL:
                case eXComData.ASCII:
                case eXComData.INT1:
                case eXComData.INT2:
                case eXComData.INT4:
                case eXComData.INT8:
                case eXComData.UINT1:
                case eXComData.UINT2:
                case eXComData.UINT4:
                case eXComData.UINT8:
                case eXComData.FLOAT4:
                case eXComData.FLOAT8:
                    {
                        sb.Append(data.ToJson(this.Name));
                    }
                    break;
                default:
                    throw new TypeAccessException(string.Format("정의 되지 않은 유형의 데이터 타입니다. XComDataType = {0}", this.Type));
            }

            return sb.ToString();
        }

        private int? ToLength(string item)
        {
            if (int.TryParse(item, out int value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        private string ToName(string[] tokens)
        {
            if (tokens.Length >= 3)
            {
                var temp = string.Join(" ", tokens.Skip(2)).Trim();

                if (temp.Contains("="))
                {
                    temp = temp.Substring(0, temp.IndexOf("=")).Trim();
                }

                return temp;
            }

            return string.Empty;
        }

        private void SetInfo(string item)
        {
            var tokens = item.Split(' ');

            this.Type = (eXComData)Enum.Parse(typeof(eXComData), tokens[0]);
            this.Length = this.ToLength(tokens[1]);
            this.Name = this.ToName(tokens);
        }

        private void SetSubItem(IEnumerable<XComItemStep> contents, int step)
        {
            if (contents.Count() <= 0) return;
            if (contents.Any(t => t.Step == step + 1) == false) return;

            var length = contents.Count();

            for (int i = 0; i < length; i++)
            {
                var temp = contents.ElementAt(i);

                var item = new XComItem(temp.Item, temp.Message);
                var subItem = contents.Skip(i + 1).TakeWhile(t => t.Step != step + 1);

                if (subItem.Any(t => t.Step > step + 2))
                {
                    item.SetSubItem(subItem, step + 1);
                }
                else
                {
                    item.SubItem.AddRange(subItem.Select(t => new XComItem(t.Item, t.Message)));
                }

                this.SubItem.Add(item);
                i += subItem.Count();
            }
        }

        private void CheckName()
        {
            var list = this.SelectMany().ToList();
            var index = this.ToIndexList(list);

            foreach (var item in list)
            {
                if (string.IsNullOrWhiteSpace(item.Name) == false) continue;
                if (item.Type == eXComData.LIST && item.Length.HasValue) continue;

                if(string.IsNullOrWhiteSpace(this.Message.SubName))
                {
                    item.Name = string.Format("{0}_{1}", item.Type.ToString().ToUpper(), index[item.Type]++);
                }
                else
                {
                    item.Name = string.Format("{2}_{0}_{1}", item.Type.ToString().ToUpper(), index[item.Type]++, this.Message.SubName);
                }                
            }

            foreach (var nList in list.GroupBy(t => t.Name))
            {
                if (nList.Count() > 1)
                {
                    var nIndex = 1;

                    foreach (var item in nList)
                    {
                        item.Name = string.Format("{0}_{1}", nList.Key, nIndex++);
                    }
                }
            }
        }

        private object ToValue(PropertyInfo info, object data)
        {
            return this.Type != eXComData.LIST ? info.GetValue(data) : new object();
        }

        private void SetData(string value, int msgId, XCOM xcom)
        {
            if (this.Length.HasValue)
            {
                xcom.SetString(msgId, value, this.Length.Value);
            }
            else
            {
                xcom.SetString(msgId, value, value.Length);
            }
        }

        private void SetData<T>(object value, int msgId, Action<int, object, int> setFunc)
        {
            if (this.Length.HasValue)
            {
                setFunc(msgId, value, this.Length.Value);
            }
            else
            {
                var list = (value as IEnumerable<T>).ToArray();
                setFunc(msgId, list, list.Length);
            }
        }

        private Dictionary<eXComData, int> ToIndexList(List<XComItem> list)
        {
            return list.GroupBy(t => t.Type).ToDictionary(t => t.Key, t => 1);
        }

        private IEnumerable<XComItem> SelectMany()
        {
            yield return this;

            if (this.Type == eXComData.LIST)
            {
                foreach (var item in this.SubItem)
                {
                    if (item.Type == eXComData.LIST)
                    {
                        foreach (var item2 in item.SelectMany())
                        {
                            yield return item2;
                        }
                    }
                    else
                    {
                        yield return item;
                    }
                }
            }
        }

        public static XComItem ToXComItem(string[] value, XComMessage msg)
        {
            if (value.Count() <= 1) return null;

            var tokens = value.Select(t => t.Replace("<", "").Replace(">", ""))
                .Where(t => !(string.IsNullOrWhiteSpace(t) || t.Contains("UNDEFINED STRUCTURE")))
                .Select(t => t.TrimEnd());

            if (tokens.Count() <= 0) return null;

            var list = tokens.Select(t => new XComItemStep()
            {
                Step = t.TakeWhile(x => x == ' ').Count() / 2,
                Item = new string(t.SkipWhile(x => x == ' ').ToArray()),
                Message = msg,
            });

            var item = new XComItem(list);
            item.CheckName();

            return item;
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]\t{2}", this.Type, this.Length.HasValue ? this.Length.Value.ToString() : "n", this.Name);
        }

        #region 클래스 파일 만들기

        internal string CreateProperty(XComMessage message)
        {
            var sb = new StringBuilder();

            if (this.Type == eXComData.LIST && this.Length.HasValue)
            {
                foreach (var contents in this.SubItem)
                {
                    if (contents.Type == eXComData.LIST && contents.Length.HasValue)
                    {
                        sb.Append(contents.CreateProperty(message));
                    }
                    else
                    {
                        sb.AppendLine(contents.GetCreateProperty(message));
                        sb.AppendLine();
                    }
                }
            }
            else
            {
                sb.AppendLine(this.GetCreateProperty(message));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private string GetCreateProperty(XComMessage message)
        {
            var sb = new StringBuilder();

            switch (this.Type)
            {
                case eXComData.LIST:
                    {
                        if (this.Length.HasValue == false)
                        {
                            sb.AppendFormat("        public List<{1}_{0}> {0}", this.Name, message.StreamFunction);
                        }
                    }
                    break;
                case eXComData.BINARY:
                case eXComData.INT1:
                case eXComData.UINT1:
                    {
                        if (this.Length.HasValue && this.Length.Value >= 2) sb.AppendLine("        [JsonConverter(typeof(TS.FW.Serialization.JsonByteArraryConvertor))]");
                        sb.AppendFormat("        public {1} {0}", this.Name, this.GetCreateProperty<byte>());
                    }
                    break;
                case eXComData.BOOL:
                    {
                        sb.AppendFormat("        public {1} {0}", this.Name, this.GetCreateProperty<bool>());
                    }
                    break;
                case eXComData.ASCII:
                case eXComData.JIS8:
                    {
                        sb.AppendFormat("        public {1} {0}", this.Name, "string");
                    }
                    break;
                case eXComData.INT2:
                    {
                        sb.AppendFormat("        public {1} {0}", this.Name, this.GetCreateProperty<short>());
                    }
                    break;
                case eXComData.INT4:
                case eXComData.INT8:
                case eXComData.UINT2:
                    {
                        sb.AppendFormat("        public {1} {0}", this.Name, this.GetCreateProperty<int>());
                    }
                    break;
                case eXComData.UINT4:
                case eXComData.UINT8:
                    {
                        sb.AppendFormat("        public {1} {0}", this.Name, this.GetCreateProperty<long>());
                    }
                    break;
                case eXComData.FLOAT4:
                    {
                        sb.AppendFormat("        public {1} {0}", this.Name, this.GetCreateProperty<float>());
                    }
                    break;
                case eXComData.FLOAT8:
                    {
                        sb.AppendFormat("        public {1} {0}", this.Name, this.GetCreateProperty<double>());
                    }
                    break;
                default:
                    throw new TypeAccessException(string.Format("정의 되지 않은 유형의 데이터 타입니다. XComDataType = {0}", this.Type));
            }

            if (sb.Length >= 1)
            {
                sb.Append(" { get; set; }");
            }

            return sb.ToString();
        }

        private string GetCreateProperty<T>()
        {
            var tName = typeof(T).Name;

            if (this.Length.HasValue)
            {
                return this.Length.Value >= 2 ? tName + "[]" : tName;
            }
            else
            {
                return string.Format("List<{0}>", tName);
            }
        }

        internal string InitProperty(XComMessage message)
        {
            var sb = new StringBuilder();

            if (this.Type == eXComData.LIST && this.Length.HasValue)
            {
                foreach (var contents in this.SubItem)
                {
                    if (contents.Type == eXComData.LIST && contents.Length.HasValue)
                    {
                        sb.Append(contents.InitProperty(message));
                    }
                    else
                    {
                        sb.AppendLine(contents.GetInitProperty(message));
                    }
                }
            }
            else
            {
                sb.AppendLine(this.GetInitProperty(message));
            }

            return sb.ToString();
        }

        private string GetInitProperty(XComMessage message)
        {
            var sb = new StringBuilder();

            switch (this.Type)
            {
                case eXComData.LIST:
                    {
                        if (this.Length.HasValue == false)
                        {
                            sb.AppendFormat("            this.{0} = new List<{1}_{0}>();", this.Name, message.StreamFunction);
                        }
                    }
                    break;
                case eXComData.BINARY:
                case eXComData.INT1:
                case eXComData.UINT1:
                    {
                        sb.AppendFormat("            this.{0} = {1};", this.Name, this.GetInitProperty<byte>());
                    }
                    break;
                case eXComData.BOOL:
                    {
                        sb.AppendFormat("            this.{0} = {1};", this.Name, this.GetInitProperty<bool>());
                    }
                    break;
                case eXComData.ASCII:
                case eXComData.JIS8:
                    {
                        sb.AppendFormat("            this.{0} = string.Empty;", this.Name);
                    }
                    break;
                case eXComData.INT2:
                    {
                        sb.AppendFormat("            this.{0} = {1};", this.Name, this.GetInitProperty<short>());
                    }
                    break;
                case eXComData.INT4:
                case eXComData.INT8:
                case eXComData.UINT2:
                    {
                        sb.AppendFormat("            this.{0} = {1};", this.Name, this.GetInitProperty<int>());
                    }
                    break;
                case eXComData.UINT4:
                case eXComData.UINT8:
                    {
                        sb.AppendFormat("            this.{0} = {1};", this.Name, this.GetInitProperty<long>());
                    }
                    break;
                case eXComData.FLOAT4:
                    {
                        sb.AppendFormat("            this.{0} = {1};", this.Name, this.GetInitProperty<float>());
                    }
                    break;
                case eXComData.FLOAT8:
                    {
                        sb.AppendFormat("            this.{0} = {1};", this.Name, this.GetInitProperty<double>());
                    }
                    break;
                default:
                    throw new TypeAccessException(string.Format("정의 되지 않은 유형의 데이터 타입니다. XComDataType = {0}", this.Type));
            }

            return sb.ToString();
        }

        private string GetInitProperty<T>()
        {
            var tName = typeof(T).Name;

            if (this.Length.HasValue)
            {
                return this.Length.Value >= 2 ? string.Format("new {0}[{1}]", tName, this.Length.Value) : default(T).ToString();
            }
            else
            {
                return string.Format("new List<{0}>()", tName);
            }
        }

        internal string CreateSubClass(XComMessage message)
        {
            var sb = new StringBuilder();

            foreach (var contents in this.SubClassList())
            {
                sb.Append(contents.GetCreateSubClass(message));
            }

            return sb.ToString();
        }

        public IEnumerable<XComItem> SubClassList()
        {
            if (this.Type == eXComData.LIST && this.Length.HasValue == false) yield return this;

            foreach (var contents in this.SubItem)
            {
                foreach (var sub in contents.SubClassList())
                {
                    yield return sub;
                }
            }
        }

        private string GetCreateSubClass(XComMessage message)
        {
            var sb = new StringBuilder();
            sb.AppendLine();

            sb.AppendFormat("    public class {0}_{1}\r\n", message.StreamFunction, this.Name);
            sb.AppendLine("    {");

            sb.Append(this.CreatsSubClassProperty(message));

            sb.AppendFormat("        public {0}_{1}()\r\n", message.StreamFunction, this.Name);
            sb.AppendLine("        {");

            sb.Append(this.InitSubClassProperty(message));

            sb.AppendLine("        }");
            sb.AppendLine("    }");

            return sb.ToString();
        }

        private string CreatsSubClassProperty(XComMessage message)
        {
            var sb = new StringBuilder();

            foreach (var contents in this.SubItem)
            {
                if (contents.Type == eXComData.LIST && contents.Length.HasValue)
                {
                    sb.Append(contents.CreatsSubClassProperty(message));
                }
                else
                {
                    sb.AppendLine(contents.GetCreateProperty(message));
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private string InitSubClassProperty(XComMessage message)
        {
            var sb = new StringBuilder();

            foreach (var contents in this.SubItem)
            {
                if (contents.Type == eXComData.LIST && contents.Length.HasValue)
                {
                    sb.Append(contents.InitSubClassProperty(message));
                }
                else
                {
                    sb.AppendLine(contents.GetInitProperty(message));
                }
            }

            return sb.ToString();
        }

        internal IEnumerable<string> GetPropertyName()
        {
            if (this.Type == eXComData.LIST && this.Length.HasValue)
            {
                foreach (var contents in this.SubItem)
                {
                    if (contents.Type == eXComData.LIST && contents.Length.HasValue)
                    {
                        foreach (var item in contents.GetPropertyName())
                        {
                            yield return item;
                        }
                    }
                    else
                    {
                        yield return contents.Name;
                    }
                }
            }
            else
            {
                yield return this.Name;
            }
        }

        internal IEnumerable<KeyValuePair<XComItem, string>> GetSubClassName(XComMessage message)
        {
            foreach (var contents in this.SubClassList())
            {
                yield return new KeyValuePair<XComItem, string>(contents, contents.GetClassName(message));
            }
        }

        internal string GetClassName(XComMessage message)
        {
            return string.Format("{0}_{1}", message.GetClassName(), this.Name);
        }

        internal IEnumerable<string> GetSubClassProperty()
        {
            foreach (var contents in this.SubItem)
            {
                if (contents.Type == eXComData.LIST && contents.Length.HasValue)
                {
                    foreach (var item in contents.GetSubClassProperty())
                    {
                        yield return item;
                    }
                }
                else
                {
                    yield return contents.Name;
                }
            }
        }

        #endregion
    }

    internal class XComItemStep
    {
        public int Step { get; set; }

        public string Item { get; set; }

        public XComMessage Message { get; set; }
    }
}
