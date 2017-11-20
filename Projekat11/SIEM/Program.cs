using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIEM
{
    public class Program
    {
        static void Main(string[] args)
        {
            //logType can be Application, Security, System or any other Custom Log.
           string logType = "NoviLog";

           EventLog ev = new EventLog(logType, System.Environment.MachineName);
           // EventLog ev = new EventLog(logType, "P04-19");
            int LastLogToShow = ev.Entries.Count;
            if (LastLogToShow <= 0)
                Console.WriteLine("No Event Logs in the Log :" + logType);

            // Read the last 2 records in the specified log. 
            int i;
            for (i = ev.Entries.Count - 1; i >= LastLogToShow - 2; i--)
            {
                EventLogEntry CurrentEntry = ev.Entries[i];
                Console.WriteLine("Event ID : " + CurrentEntry.EventID);
                Console.WriteLine("Entry Type : " + CurrentEntry.EntryType.ToString());
                Console.WriteLine("Message :  " + CurrentEntry.Message + "\n");
            }
            ev.Close();
            Console.ReadLine();
        }
    }
}
