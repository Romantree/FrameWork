using System;

namespace TS.FW.Wpf.Core
{
    public static class CommandLog
    {
        public static bool IsTracking { get; set; } = true;

        public static void Write(object call, object parameter)
        {
            try
            {
                if (IsTracking == false) return;

                if (parameter == null)
                {
                    Logger.Write(call);
                }
                else
                {
                    Logger.Write(call, $"{parameter}");
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(CommandLog), ex);
            }
        }
    }
}
