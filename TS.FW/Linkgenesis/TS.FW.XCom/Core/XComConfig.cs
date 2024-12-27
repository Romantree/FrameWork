using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Serialization;
using TS.FW.Utils;
using XCOMLib;

namespace TS.FW.XCom.Core
{
    public class XComConfig
    {
        public string ConfigFilePath { get; private set; }

        public int DeviceID { get => this.ConfigFilePath.GetIniFile_Value("General", "Device ID", 1).Result; set => this.ConfigFilePath.SetIniFile_Value("General", "Device ID", value); }

        public string SML { get => this.ConfigFilePath.GetIniFile_Value("General", "SML", "").Result; set => this.ConfigFilePath.SetIniFile_Value("General", "SML", value); }

        public string IP { get => this.ConfigFilePath.GetIniFile_Value("HSMS", "IP", "127.0.0.1").Result; set => this.ConfigFilePath.SetIniFile_Value("HSMS", "IP", value); }

        public int Port { get => this.ConfigFilePath.GetIniFile_Value("HSMS", "Port", 2001).Result; set => this.ConfigFilePath.SetIniFile_Value("HSMS", "Port", value); }

        public bool Active { get => Convert.ToBoolean(this.ConfigFilePath.GetIniFile_Value("HSMS", "Active", "false").Result); set => this.ConfigFilePath.SetIniFile_Value("HSMS", "Active", value.ToString()); }

        public int T1 { get => this.ConfigFilePath.GetIniFile_Value("Timeout", "T1", 1).Result; set => this.ConfigFilePath.SetIniFile_Value("Timeout", "T1", value); }

        public int T2 { get => this.ConfigFilePath.GetIniFile_Value("Timeout", "T2", 10).Result; set => this.ConfigFilePath.SetIniFile_Value("Timeout", "T2", value); }

        public int T3 { get => this.ConfigFilePath.GetIniFile_Value("Timeout", "T3", 45).Result; set => this.ConfigFilePath.SetIniFile_Value("Timeout", "T3", value); }

        public int T4 { get => this.ConfigFilePath.GetIniFile_Value("Timeout", "T4", 30).Result; set => this.ConfigFilePath.SetIniFile_Value("Timeout", "T4", value); }

        public int T5 { get => this.ConfigFilePath.GetIniFile_Value("Timeout", "T5", 10).Result; set => this.ConfigFilePath.SetIniFile_Value("Timeout", "T5", value); }

        public int T6 { get => this.ConfigFilePath.GetIniFile_Value("Timeout", "T6", 5).Result; set => this.ConfigFilePath.SetIniFile_Value("Timeout", "T6", value); }

        public int T7 { get => this.ConfigFilePath.GetIniFile_Value("Timeout", "T7", 10).Result; set => this.ConfigFilePath.SetIniFile_Value("Timeout", "T7", value); }

        public int T8 { get => this.ConfigFilePath.GetIniFile_Value("Timeout", "T8", 5).Result; set => this.ConfigFilePath.SetIniFile_Value("Timeout", "T8", value); }

        public int LinkTestInterval { get => this.ConfigFilePath.GetIniFile_Value("Timeout", "Link Test Interval", 30).Result; set => this.ConfigFilePath.SetIniFile_Value("Timeout", "Link Test Interval", value); }

        public int RetryLimit { get => this.ConfigFilePath.GetIniFile_Value("Timeout", "Retry Limit", 0).Result; set => this.ConfigFilePath.SetIniFile_Value("Timeout", "Retry Limit", value); }

        public XComConfig(string filePath)
        {
            this.ConfigFilePath = filePath;
        }
    }
}
