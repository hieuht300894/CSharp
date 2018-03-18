using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ConfigAutomation
{
    public static class FileErrorLogger
    {
        #region Constant
        const string _DateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        #endregion
        public static void LogError(string function
            , string message
            , string logFolder = ""
            , bool iserror = true)
        {
            try
            {
                if (string.IsNullOrEmpty(logFolder))
                {
                    string folder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                    if (iserror)
                        logFolder = String.Format(@"{0}\Err", folder);
                    else
                        logFolder = String.Format(@"{0}\Logs", folder);
                }
                if (!Directory.Exists(logFolder))
                {
                    Directory.CreateDirectory(logFolder);
                }
                string logFile = String.Format(@"{0}\{1}.txt", logFolder, DateTime.Now.ToString("yyyyMMdd"));
                using (StreamWriter w = File.AppendText(logFile))
                {
                    w.WriteLine(String.Format("{0} - {1} - {2}", function, DateTime.Now.ToString(_DateTimeFormat), message));
                }
            }
            catch ///(Exception ex)
            {
                //WindowsEventLogger.LogError("FileErrorLogger", ex.Message);
            }
        }
        public static void LogInformation(string function
            , string message
            , string logFolder = "")
        {
            LogError(function, message, logFolder, false);
        }
    }
}
