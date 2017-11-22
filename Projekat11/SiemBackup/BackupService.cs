using SiemBackup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Diagnostics;

namespace SiemBackup
{
    public class BackupService : iBackupService
    {
        public void LogChanged(string LogName, EventLogEntry entry)
        {
            switch (LogName)
            {
                case "FirstLog":
                    Program.backupLog1.WriteEntry(entry.Message,EventLogEntryType.Error);
                    break;
                case "SecondLog":
                    Program.backupLog2.WriteEntry(entry.Message,EventLogEntryType.Error);
                    break;
                case "ThirdLog":
                    Program.backupLog3.WriteEntry(entry.Message,EventLogEntryType.Error);
                    break;
            }  
        }

        public  EventLog CreateLog(string logName, string sourceName)
        {
            EventLog ev = null;

            try
            {
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, logName);
                }

                ev = new EventLog(logName, Environment.MachineName, sourceName);
            }
            catch (Exception e)
            {
                ev = null;
                Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
            }
            return ev;
        }

    }
}
