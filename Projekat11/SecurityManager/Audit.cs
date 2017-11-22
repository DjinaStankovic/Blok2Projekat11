using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class Audit : IDisposable
    {
        private  EventLog customLog = null;
        private string logName = String.Empty;
 
        public Audit(string LogName,string SourceName)
        {
            try
            {
                logName = LogName;

                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, LogName);
                }
              
                customLog = new EventLog(LogName, Environment.MachineName, SourceName);
            }
            catch (Exception e)
            {
                customLog = null;
                Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
            }
        }

        public  void CreateFailed(string userName)
        {

            if (customLog != null)
            {
                string message = String.Format("{0} doesn't have a permissions to create a file!", userName);
                customLog.WriteEntry(message, EventLogEntryType.Error);
            }
        }

        public void DeleteFailed(string userName)
        {
            if (customLog != null)
            {
                string message = String.Format("{0} doesn't have a permissions to delete a file!", userName);
                customLog.WriteEntry(message, EventLogEntryType.Error);
            }
        }

        public void WriteInFileFailed(string userName)
        {
            if (customLog != null)
            {
                string message = String.Format("{0} doesn't have the permissions to write to the file!", userName );
                customLog.WriteEntry(message, EventLogEntryType.Error);
            }
        }

        public void ReadFromFileFailed(string userName)
        {
            if (customLog != null)
            {
                string message = String.Format("{0} doesn't have the permissions to read from the file!", userName);
                customLog.WriteEntry(message, EventLogEntryType.Error);      
            }
        }

        public void Dispose()
        {
            if (customLog != null)
            {
                customLog.Dispose();
                customLog = null;
            }
        }

    }
}
