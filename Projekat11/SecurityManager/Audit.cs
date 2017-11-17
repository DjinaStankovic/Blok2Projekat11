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
        private static EventLog customLog = null;
        const string SourceName = "SecurtiyManager.Audit";
        const string LogName = "NasLog";

        static Audit()
        {
            try
            {

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


        public static void CreateFailed(string userName)
        {

            if (customLog != null)
            {

                string message = String.Format("{0} korisnik nema permisiju da kreira fajl!", userName);
                customLog.WriteEntry(message, EventLogEntryType.Information);
            }

        }

        public static void DeleteFailed(string userName)
        {
            if (customLog != null)
            {
                string message = String.Format("{0} nema permisiju da obrise fajl!", userName);
                customLog.WriteEntry(message, EventLogEntryType.Information);
            }

        }




        public static void WriteInFileFailed(string userName)
        {

            if (customLog != null)
            {
                string message = String.Format("{0} nema permisiju da upisuje u fajl!", userName);
                customLog.WriteEntry(message, EventLogEntryType.Error);
            }

        }


        public static void ReadFromFileFailed(string userName)
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
