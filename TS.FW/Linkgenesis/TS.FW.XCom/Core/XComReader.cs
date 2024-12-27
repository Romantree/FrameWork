using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TS.FW.XCom.Core
{
    public class XComReader
    {
        public XComConfig Config { get; private set; }

        public List<XComMessage> List { get; private set; } = new List<XComMessage>();

        public XComReader(string cfgFilePath)
        {
            this.Config = new XComConfig(cfgFilePath);
        }

        public void LoadData()
        {
            var sml = this.Config.SML;

            var dir = Path.GetDirectoryName(sml);

            if (string.IsNullOrWhiteSpace(dir)) sml = Path.Combine(Path.GetDirectoryName(this.Config.ConfigFilePath), sml);

            if (string.IsNullOrWhiteSpace(sml)) throw new NullReferenceException("SML FILE");
            if (File.Exists(sml) == false) throw new FileNotFoundException(sml);

            this.List.Clear();

            foreach (var item in this.ToSmlSplit(sml))
            {
                this.List.Add(new XComMessage(item.ToArray()));
            }
        }

        public XComMessage FindMessage(int stream, int function, IXComData data)
        {
            var list = this.List.Where(t => t.Stream == stream && t.Function == function).ToList();

            if (list.Count <= 0) throw new KeyNotFoundException(string.Format("S{0}F{1}", stream, function));
            else if (list.Count >= 2)
            {
                foreach (var msg in list)
                {
                    if (msg.CheckFormat(data)) return msg;
                }

                throw new NotSupportedException(string.Format("S{0}F{1}", stream, function));
            }

            return list.First();
        }

        public XComMessage FindMessage(IXComModel data)
        {
            return this.List.FirstOrDefault(t => t.Stream == data.Stream && t.Function == data.Function && t.FullName == data.FullName);
        }

        private IEnumerable<IEnumerable<string>> ToSmlSplit(string sml)
        {
            var lines = File.ReadAllLines(sml);
            var list = lines.Where(t => t.Contains("<XCom ") == false).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                var line = list.Skip(i).TakeWhile(t => string.IsNullOrEmpty(t) == false);

                yield return line;

                i += line.Count();
            }
        }

        #region 클래스 파일 만들기

        public IEnumerable<string> ValidationClassFile()
        {
            foreach (var item in this.List)
            {
                var value = item.ValidationClassFile();

                if (string.IsNullOrWhiteSpace(value) == false) yield return string.Format("{0}\r\n{1}", item, value);
            }
        }

        public IEnumerable<KeyValuePair<string, string>> MakeClassFile(string path, string projectName)
        {
            foreach (var item in this.List)
            {
                yield return new KeyValuePair<string, string>(item.GetDirectory(path, projectName), item.MakeClassFile(projectName));
            }
        }

        #endregion
    }
}
