using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Serialization;
using XCOMLib;

namespace TS.FW.XCom.Core
{
    public class XComMessage
    {
        private readonly string[] _format = null;

        public string FullName { get; private set; }

        public int Stream { get; private set; }

        public int Function { get; private set; }

        public string Action { get; private set; }

        public bool IsUnDefined { get; private set; }

        public XComItem Item { get; private set; }

        public string Name => this.ToNameSplit("-", 0, this.FullName);

        public string SubName => this.ToNameSplit("-", 1, string.Empty);

        public string StreamFunction => string.Format("S{0}F{1}", this.Stream, this.Function);

        public string Format => (this._format == null || this._format.Length <= 0) ? string.Empty : string.Join(Environment.NewLine, this._format);

        private string ToNameSplit(string separator, int index, string defaultValue = "")
        {
            if (string.IsNullOrEmpty(this.FullName)) return defaultValue;
            if (this.FullName.Contains(separator)) return this.FullName.Split(new string[] { separator }, StringSplitOptions.None)[index].Trim();

            return defaultValue;
        }

        public XComMessage(string[] list)
        {
            try
            {
                this._format = list;

                var value = list.First();
                var tokens = value.Substring(1).Split(' ');
                var first = tokens.First();
                var Item = list.Skip(1).ToArray();

                this.Stream = int.Parse(first.Substring(1, first.IndexOf('F') - 1));
                this.Function = int.Parse(first.Substring(first.IndexOf('F') + 1));
                this.Action = tokens.Skip(1).First();
                this.FullName = string.Join(" ", tokens.Skip(2));
                this.IsUnDefined = Item.Select(t => t.Replace("<", "").Replace(">", "").Trim()).Any(t => t.Contains("UNDEFINED STRUCTURE"));

                this.Item = XComItem.ToXComItem(Item, this);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool CheckFormat(IXComData data)
        {
            if (this.IsUnDefined == true) return false;
            if (this.Item == null && data == null) return true;

            return this.Item.CheckFormat(data);
        }

        public void SetSendData(int msgId, XCOM xcom, IXComModel data)
        {
            data.SysByte = xcom.GetSysBytes(msgId);

            if (this.IsUnDefined || this.Item == null)
            {
                if (this.IsUnDefined) data.SendData(xcom, msgId);
                return;
            }

            this.Item.SetSendData(msgId, xcom, data, data.GetType());
        }

        public IXComModel CreateMsgData(XComMsgInfo info, IXComData data, string projectName, Assembly assembly)
        {
            var msgData = this.CreateInstance(assembly, projectName, out string className);

            if (this.IsUnDefined || this.Item == null)
            {
                msgData.SetXComMsgInfo(info);

                if (this.IsUnDefined) msgData.RecvData(data);

                return msgData;
            }

            var json = this.MakeJson(data);
            if (string.IsNullOrWhiteSpace(json)) throw new FormatException(string.Format("데이터 포멧이 맞지 않습니다. Class = {0}", className));

            var item = this.ToMsgData(json, msgData.GetType());

            item.SetXComMsgInfo(info);

            return item;
        }

        private IXComModel CreateInstance(Assembly assembly, string projectName, out string className)
        {
            if (assembly == null) throw new NullReferenceException("Assembly가 초기화 되어 있지 않습니다.");

            className = this.GetClassName(assembly, projectName);

            var item = assembly.CreateInstance(className);
            if (item == null) throw new NotImplementedException(string.Format("클래스가 존재하지 않습니다. Class = {0}", className));

            var model = item as IXComModel;
            if (model == null) throw new NotImplementedException(string.Format("해당 클래스는 IXComData 클래스를 상속하지 않았습니다. Class = {0}", className));

            return model;
        }

        private string GetClassName(Assembly assembly, string projectName)
        {
            var className = string.Format("{0}.{1}_Model", projectName, this.GetClassName());

            var info = assembly.DefinedTypes.Where(t => t.BaseType == typeof(IXComModel)).FirstOrDefault(t => t.FullName == className);
            if (info == null) throw new NotImplementedException(string.Format("클래스가 존재하지 않습니다. Class = {0}", className));

            return info.FullName;
        }

        public string GetDirectory(string path, string projectName)
        {
            return Path.Combine(path, string.Join(@"\", projectName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries))
                        , string.Format("Stream{0:000}", this.Stream), string.Format("{0}_Model.cs", this.GetClassFileName()));
        }

        public string GetClassFileName()
        {
            return string.Format("S{0}F{1:000}_{2}{3}", this.Stream, this.Function, this.Name, string.IsNullOrWhiteSpace(this.SubName) ? "" : string.Format("_{0}", this.SubName))
                        .Replace(" ", "_")
                        .Replace(",", "")
                        .Replace(".", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("/", "")
                        .Replace(@"\", "")
                        .Replace("�", "");
        }

        public string GetClassName()
        {
            return string.Format("{0}_{1}{2}", this.StreamFunction, this.Name, string.IsNullOrWhiteSpace(this.SubName) ? "" : string.Format("_{0}", this.SubName))
                        .Replace(" ", "_")
                        .Replace(",", "")
                        .Replace(".", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("/", "")
                        .Replace(@"\", "")
                        .Replace("�", "");
        }

        public string MakeJson(IXComData data)
        {
            if (this.Item == null || data == null) return string.Empty;

            var sb = new StringBuilder();
            sb.Append("{");

            sb.Append(this.Item.MakeJson(data));

            sb.Append("}");
            return sb.ToString();
        }

        private IXComModel ToMsgData(string json, Type type)
        {
            var res = json.JsonDeserializer(type);
            if (res == false) Logger.Write(this, res.Comment, Logger.LogEventLevel.Error);

            return res.Result as IXComModel;
        }

        #region 클래스 파일 만들기

        public string MakeClassFile(string projectName)
        {
            var sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine("using TS.FW.XCom;");
            sb.AppendLine("using Newtonsoft.Json;");
            sb.AppendLine("using XCOMLib;");

            sb.AppendLine();

            sb.AppendLine(string.Format("namespace {0}", projectName));
            sb.AppendLine("{");
            sb.AppendLine(string.Format("    public class {0}_Model : IXComModel", this.GetClassName()));
            sb.AppendLine("    {");

            // 프로퍼티 선언
            if (this.Item != null)
            {
                sb.Append(this.Item.CreateProperty(this));
            }

            sb.AppendLine(string.Format("        public {0}_Model()", this.GetClassName()));
            sb.AppendLine("        {");
            sb.AppendLine(string.Format("            this.Stream = {0};", this.Stream));
            sb.AppendLine(string.Format("            this.Function = {0};", this.Function));
            sb.AppendLine(string.Format("            this.FullName = \"{0}\";", this.FullName));
            sb.AppendLine(string.Format("            this.Name = \"{0}\";", this.Name));
            sb.AppendLine(string.Format("            this.SubName = \"{0}\";", this.SubName));
            sb.AppendLine(string.Format("            this.IsUnDefined = {0};", this.IsUnDefined.ToString().ToLower()));

            // 프로퍼티 초기화
            if (this.Item != null)
            {
                sb.AppendLine();
                sb.Append(this.Item.InitProperty(this));
            }

            sb.AppendLine("        }");

            if (this.IsUnDefined)
            {
                sb.AppendLine();
                sb.AppendLine("        public override void RecvData(IXComData data)");
                sb.AppendLine("        {");
                sb.AppendLine("            ");
                sb.AppendLine("        }");
                sb.AppendLine();
                sb.AppendLine("        public override void SendData(XCOM xcom, int msgId)");
                sb.AppendLine("        {");
                sb.AppendLine("            ");
                sb.AppendLine("        }");
            }

            sb.AppendLine("    }");

            // 서브 클래스
            if (this.Item != null)
            {
                sb.Append(this.Item.CreateSubClass(this));
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        public string ValidationClassFile()
        {
            var sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(this.FullName))
            {
                sb.AppendLine("[클래스 명] : 공백 입니다.");
            }
            else if (this.IsClassName(this.FullName) == false)
            {
                sb.AppendFormat("[클래스 명] : 특수문자가 존재합니다. {0}\r\n", this.FullName);
            }

            if (this.Item == null) return sb.ToString();

            var proeptyList = this.Item.GetPropertyName();
            sb.Append(this.ValidationProperty(proeptyList, "클래스 프로퍼티"));

            var subClassList = this.Item.GetSubClassName(this);

            foreach (var item in subClassList.Select(t => t.Value)
                .Where(t => string.IsNullOrWhiteSpace(t) == false).GroupBy(t => t).Where(t => t.Count() >= 2))
            {
                sb.AppendFormat("[서브 클래스 명] : 동일한 이름이 존재합니다. {0} [{1}]\r\n", item.Key, item.Count());
            }

            foreach (var item in subClassList)
            {
                var subSb = new StringBuilder();
                var subClassPropertyList = item.Key.GetSubClassProperty();

                if (string.IsNullOrWhiteSpace(item.Value))
                {
                    subSb.AppendLine("[서브클래스 명] : 공백 입니다.");
                }
                else if (this.IsPropertyName(item.Value) == false)
                {
                    subSb.AppendFormat("[서브클래스 명] : 특수문자 또는 공백이 존재합니다. {0}\r\n", item.Value);
                }

                subSb.Append(this.ValidationProperty(subClassPropertyList, "서브클래스 프로퍼티"));

                if (string.IsNullOrEmpty(subSb.ToString()) == false)
                {
                    sb.Append(string.Format("{0}\r\n{1}", item.Key.GetClassName(this), subSb.ToString()));
                }
            }

            return sb.ToString();
        }

        private string ValidationProperty(IEnumerable<string> list, string token)
        {
            var sb = new StringBuilder();

            if (list.Any(t => string.IsNullOrWhiteSpace(t)))
            {
                sb.AppendFormat("[{0}] : 공백이 존재합니다.\r\n", token);
            }

            foreach (var item in list.Where(t => string.IsNullOrWhiteSpace(t) == false)
                .Where(t => this.IsPropertyName(t) == false))
            {
                sb.AppendFormat("[{0}] : 특수문자 또는 공백이 존재합니다. {1}\r\n", token, item);
            }

            foreach (var item in list.Where(t => string.IsNullOrWhiteSpace(t) == false)
                .GroupBy(t => t).Where(t => t.Count() >= 2))
            {
                sb.AppendFormat("[{0}] : 동일한 이름이 존재합니다. {1} [{2}]\r\n", token, item.Key, item.Count());
            }

            return sb.ToString();
        }

        private bool IsClassName(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            return this.IsRegexMatch(input, "^[a-zA-Z0-9_ -]*$");
        }

        private bool IsPropertyName(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            return this.IsRegexMatch(input, "^[a-zA-Z0-9_]*$");
        }

        private bool IsRegexMatch(string input, string pattern)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, pattern);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} {1}\t{2}", this.StreamFunction, this.Action, this.FullName);
        }
    }
}
