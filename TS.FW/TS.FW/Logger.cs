using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TS.FW.Diagnostics;
using TS.FW.Utils;

namespace TS.FW
{
    public static class Logger
    {
        public enum LogEventLevel
        {
            Error,
            Warning,
            Information,
            Debug,
        }

        public class LogInfo
        {
            public DateTime Time { get; internal set; }

            public int ThreadID { get; internal set; }

            public string ProductName { get; internal set; }

            public string FilePath { get; internal set; }

            public string Category { get; internal set; }

            public string Method { get; internal set; }

            public string Message { get; internal set; }

            public LogEventLevel Level { get; internal set; }

            internal LogInfo()
            {
                this.ThreadID = Thread.CurrentThread.ManagedThreadId;
            }

            public override string ToString()
            {
                // 16:37:45.124642 [LogLevel] category(method) : message
                return string.Format("{0} [{1}] {2}({3})[{4}] : {5}"
                    , this.Time.ToString("HH:mm:ss.fff")
                    , this.Level.ToString()[0]
                    , this.Category
                    , this.Method
                    , this.ThreadID
                    , string.IsNullOrEmpty(this.Message) ? "Empty" : this.Message);
            }
        }

        private class _FileWriteItem
        {
            public string FilePath { get; private set; }

            public LogInfo Contents { get; private set; }

            public _FileWriteItem(string filePath, LogInfo contents)
            {
                this.FilePath = filePath;
                this.Contents = contents;
            }
        }

        private readonly static object deleteLock = new object();

        private readonly static Dictionary<object, DateTime> _WatchList = new Dictionary<object, DateTime>();
        private readonly static List<_FileWriteItem> _fileWriteList = new List<_FileWriteItem>();

        private readonly static BackgroundTimer _trFileWrite = new BackgroundTimer(System.Threading.ApartmentState.MTA);

        private static DateTime _deleteTime = DateTime.MinValue;

        public static event EventHandler<LogInfo> OnLogTraceEvent;

        public static string RootPath { get; set; }

        public static int LogCount => _fileWriteList.Count;

        public static int FileDeleteDay { get; set; } = 30;

        public static LogEventLevel LogLevel { get; set; }

        static Logger()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

            RootPath = Path.Combine(Environment.CurrentDirectory, "Logger");
            LogLevel = LogEventLevel.Warning;

            _trFileWrite.SleepTimeMsc = 5000;
            _trFileWrite.DoWork += _trFileWrite_DoWork;
            _trFileWrite.Start();
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            try
            {
                if (_fileWriteList == null || _fileWriteList.Count <= 0) return;

                List<_FileWriteItem> list;

                lock (_fileWriteList)
                {
                    list = _fileWriteList.ToList();
                    _fileWriteList.Clear();
                }

                foreach (var item in list.GroupBy(t => t.FilePath))
                {
                    File.AppendAllLines(item.Key, item.OrderBy(t => t.Contents.Time).Select(t => t.Contents.ToString()).ToArray(), Encoding.Default);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private static void _trFileWrite_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (_fileWriteList == null || _fileWriteList.Count <= 0) return;

                List<_FileWriteItem> list;

                lock (_fileWriteList)
                {
                    list = _fileWriteList.ToList();
                    _fileWriteList.Clear();
                }

                foreach (var item in list.GroupBy(t => t.FilePath))
                {
                    File.AppendAllLines(item.Key, item.OrderBy(t => t.Contents.Time).Select(t => t.Contents.ToString()).ToArray(), Encoding.Default);
                }

                LogClear();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static void LogClear()
        {
            try
            {
                if (FileDeleteDay <= 0) return;

                if (_deleteTime == DateTime.Now.Date) return;
                _deleteTime = DateTime.Now.Date;

                lock (deleteLock)
                {
                    foreach (var item in GetDirectories())
                    {
                        if (item.Value <= DateTime.Now.AddDays(-FileDeleteDay).Date)
                        {
                            Directory.Delete(item.Key, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private static IEnumerable<KeyValuePair<string, DateTime>> GetDirectories()
        {
            foreach (var item in Directory.EnumerateDirectories(RootPath, "*", SearchOption.AllDirectories))
            {
                var path = item.Replace(RootPath, "");

                var time = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                if (time.Length != 3) continue;

                var str = string.Format("{0}-{1}-{2}", time[0], time[1], time[2]);

                if (DateTime.TryParse(str, out DateTime dt) == false) continue;

                yield return new KeyValuePair<string, DateTime>(item, dt);
            }
        }

        public static void StartWatch(out object obj)
        {
            obj = new object();

            if (_WatchList.ContainsKey(obj))
            {
                _WatchList[obj] = DateTime.Now;
            }
            else
            {
                _WatchList.Add(obj, DateTime.Now);
            }
        }

        public static void StopWatch(object obj, string message, bool isDebug = false)
        {
            try
            {
                if (_WatchList.ContainsKey(obj) == false) return;
                var tactTime = DateTime.Now - _WatchList[obj];

                if (isDebug)
                {
                    Trace.WriteLine(string.Format("{0} [{1:N0}ms]", message, tactTime.TotalMilliseconds));
                }
                else
                {
                    Logger.Write(typeof(Logger), string.Format("{0} [{1:N0}ms]", message, tactTime.TotalMilliseconds));
                }
            }
            finally
            {
                _WatchList[obj] = DateTime.Now;
            }
        }

        public static void DebugWrite(object caller)
        {
            var now = DateTime.Now;
            var productName = Assembly.GetCallingAssembly().GetName().Name;
            var filePath = GetFilePath(Assembly.GetCallingAssembly(), now);
            var category = ResolveCaller(caller);
            var method = ResolveMethod();
            var message = string.Empty;
            var level = LogEventLevel.Debug;

            Logger.Write(now, productName, filePath, category, method, message, level);
        }

        public static void DebugWrite(object caller, string message)
        {
            var now = DateTime.Now;
            var productName = Assembly.GetCallingAssembly().GetName().Name;
            var filePath = GetFilePath(Assembly.GetCallingAssembly(), now);
            var category = ResolveCaller(caller);
            var method = ResolveMethod();
            var level = LogEventLevel.Debug;

            Logger.Write(now, productName, filePath, category, method, message, level);
        }

        public static void DebugWrite(object caller, Exception ex)
        {
            var now = DateTime.Now;
            var productName = Assembly.GetCallingAssembly().GetName().Name;
            var filePath = GetFilePath(Assembly.GetCallingAssembly(), now);
            var category = ResolveCaller(caller);
            var method = ResolveMethod();
            var message = ex.MakeExceptionMessage();
            var level = LogEventLevel.Debug;

            Logger.Write(now, productName, filePath, category, method, method, level);
        }

        public static void Write(object caller, LogEventLevel level = LogEventLevel.Information)
        {
            var now = DateTime.Now;
            var productName = Assembly.GetCallingAssembly().GetName().Name;
            var filePath = GetFilePath(Assembly.GetCallingAssembly(), now);
            var category = ResolveCaller(caller);
            var method = ResolveMethod();
            var message = "Call";

            Logger.Write(now, productName, filePath, category, method, message, level);
        }

        public static void Write(object caller, string message, LogEventLevel level = LogEventLevel.Information)
        {
            var now = DateTime.Now;
            var productName = Assembly.GetCallingAssembly().GetName().Name;
            var filePath = GetFilePath(Assembly.GetCallingAssembly(), now);
            var category = ResolveCaller(caller);
            var method = ResolveMethod();

            Logger.Write(now, productName, filePath, category, method, message, level);
        }

        public static void Write(object caller, Exception ex, LogEventLevel level = LogEventLevel.Error)
        {
            var now = DateTime.Now;
            var productName = Assembly.GetCallingAssembly().GetName().Name;
            var filePath = GetFilePath(Assembly.GetCallingAssembly(), now);
            var category = ResolveCaller(caller);
            var method = ResolveMethod();
            var message = ex.MakeExceptionMessage();

            Logger.Write(now, productName, filePath, category, method, message, level);
        }

        public static void CustomWrite(string name, object caller, string message, LogEventLevel level = LogEventLevel.Information)
        {
            var now = DateTime.Now;
            var productName = Assembly.GetCallingAssembly().GetName().Name;
            var filePath = GetFilePath(name, now);
            var category = ResolveCaller(caller);
            var method = ResolveMethod();

            Logger.Write(now, productName, filePath, category, method, message, level);
        }

        private static void Write(DateTime now, string productName, string filePath, string category, string method, string message, LogEventLevel level)
        {
            var info = new LogInfo()
            {
                Time = now,
                ProductName = productName,
                FilePath = filePath,
                Category = category,
                Method = method,
                Message = message,
                Level = level,
            };

            if (level == LogEventLevel.Debug)
            {
#if DEBUG
                Debug.WriteLine(info.ToString());
#else
                Trace.WriteLine(info.ToString());
#endif
            }

            FileWrite(info);
        }

        private static void FileWrite(LogInfo info)
        {
            if (info.Level == LogEventLevel.Debug || (info.Level <= LogLevel) == false) return;

            OnLogTraceEvent?.Invoke(typeof(Logger), info);

            var dir = Path.GetDirectoryName(info.FilePath);
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);

            var filePath = GetFilePath(info.FilePath);

            lock (_fileWriteList)
            {
                _fileWriteList.Add(new _FileWriteItem(filePath, info));
            }
        }

        private static string ResolveCaller(object caller)
        {
            if (caller == null) return string.Empty;
            if (caller is string) return caller.ToString();
            if (caller is Enum) return caller.ToString();
            if (caller is Type) return ((Type)caller).Name;

            return caller.GetType().Name;
        }

        private static string ResolveMethod()
        {
            var info = new StackTrace().GetFrame(2).GetMethod();

            return info != null ? info.Name : string.Empty;
        }

        private static string GetFilePath(Assembly assembly, DateTime now)
        {
            return GetFilePath(assembly.GetName().Name, now);
        }

        private static string GetFilePath(string name, DateTime now)
        {
            return Path.Combine(RootPath, now.ToString("yyyy"), now.ToString("MM"), now.ToString("dd"), string.Format("{0}_{1}.log", name, now.ToString("MMddHH")));
        }

        private static string GetFilePath(string filePath)
        {
            if (File.Exists(filePath) && new FileInfo(filePath).Length >= (10 * 1024 * 1024))
            {
                var dir = Path.GetDirectoryName(filePath);
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                var extension = Path.GetExtension(filePath);

                fileName = fileName.Contains("[")
                    ? string.Format("{0}[{1}]", fileName.Substring(0, fileName.IndexOf("[")), int.Parse(fileName.Substring(fileName.IndexOf("[")
                    + 1, fileName.Length - fileName.IndexOf("[") - 2)) + 1)
                    : string.Format("{0}[1]", fileName);

                return GetFilePath(Path.Combine(dir, string.Format("{0}{1}", fileName, extension)));
            }

            return filePath;
        }
    }
}
