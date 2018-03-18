using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ConfigAutomation
{
    public static class WindowsEventLogger
    {
        public static void LogError(string function
            , string message
            , string serviceName = ""
            , string source = ""
            , bool iserror = true)
        {
            //this.ServiceName = "SftpFileTransferService";
            //this.EventLog.Source = "SftpFileTransferService";
            //this.EventLog.Log = "Application";
            if (string.IsNullOrEmpty(serviceName))
            {
                serviceName = "Config Automation";
            }
            if (string.IsNullOrEmpty(source))
            {
                source = "Config Automation";
            }
            string log = "Application";
            string logEvent = String.Format("{0} - {1}", function, message);
            if (!EventLog.SourceExists(serviceName))
                EventLog.CreateEventSource(serviceName, log);
            if (iserror)
                EventLog.WriteEntry(serviceName, logEvent, EventLogEntryType.Error);
            else
                EventLog.WriteEntry(serviceName, logEvent, EventLogEntryType.Information);
        }
        public static void LogInformation(string function
            , string message
            , string serviceName = ""
            , string source = "")
        {
            LogError(function, message, serviceName, source, false);
        }
    }
}
