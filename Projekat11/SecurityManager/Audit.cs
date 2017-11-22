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

                string message = String.Format("{0} korisnik nema permisiju da kreira fajl!", userName);
                customLog.WriteEntry(message, EventLogEntryType.Error);
                ////////////////////////EventLogEntry a = customLog.Entries[customLog.Entries.Count - 1];
                ////////////////////////int aa = 3;
            }

        }

        public void DeleteFailed(string userName)
        {
            if (customLog != null)
            {
                string message = String.Format("{0} nema permisiju da obrise fajl!", userName);
                customLog.WriteEntry(message, EventLogEntryType.Error);
            }

        }




        public void WriteInFileFailed(string user)
        {

            if (customLog != null)
            {
                string message = String.Format("{0} nema permisiju da upisuje u fajl!",user );
                customLog.WriteEntry(message, EventLogEntryType.Error);
            }

        }


        public void ReadFromFileFailed(string userName)
        {

            if (customLog != null)
            {
                string message = String.Format("{0} nema permisiju da iscitava iz fajla!", userName);
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
