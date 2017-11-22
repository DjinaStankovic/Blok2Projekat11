using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SIEM
{
    public class Program
    {
      
        static void Main(string[] args)
        {
            EventLog ev1 = null;
            EventLog ev2 = null;
            EventLog ev3 = null;

            EventLog customLog1 = null;
            EventLog customLog2 = null;
            EventLog customLog3 = null;

            Console.WriteLine("Enter machine1 name( in format P04-X): ");
            string machineName1 = Console.ReadLine();
            machineName1 = machineName1.ToUpper();
            string[] a = machineName1.Split('-');
            string machine1 = String.Format("{0}{1}", a[0], a[1]);
            Console.WriteLine("Enter machine2 name( in format P04-X):");
            string machineName2 = Console.ReadLine();
            machineName2 = machineName2.ToUpper();
            string[] b = machineName2.Split('-');
            string machine2 = String.Format("{0}{1}", b[0], b[1]);
            Console.WriteLine("Enter machine3 name( in format P04-X):");
            string machineName3 = Console.ReadLine();
            machineName3 = machineName3.ToUpper();
            string[] c = machineName3.Split('-');
            string machine3 = String.Format("{0}{1}", c[0], c[1]);

            customLog1 = CreateLog("FirstLog", String.Format("FirstLog{0}",machine1));
            customLog2= CreateLog("SecondLog", String.Format("SecondLog{0}", machine2));
            customLog3= CreateLog("ThirdLog", String.Format("ThirdLog{0}", machine3));


          
               ev1 = new EventLog(String.Format("{0}LogFile", machine1),machineName1);
               ev2 = new EventLog(String.Format("{0}LogFile", machine2),machineName2);
               ev3 = new EventLog(String.Format("{0}LogFile", machine3),machineName3);
              
            while (true)
            {
                ReadMessage(ev1.Entries.Count, customLog1.Entries.Count, ev1, customLog1);
                ReadMessage(ev2.Entries.Count, customLog2.Entries.Count, ev2, customLog2);
                ReadMessage(ev3.Entries.Count, customLog3.Entries.Count, ev3, customLog3);
                Thread.Sleep(2000);
            
            }

         
        }
        static void ReadEventLog(string eventLogName,string machineName)
        {
            EventLog eventLog = new EventLog();
            eventLog.Log = eventLogName;
           
            eventLog.MachineName = machineName;      

            foreach (EventLogEntry log in eventLog.Entries)
            {
                Console.WriteLine("Machine name: {0}", log.MachineName);
                Console.WriteLine("Entry type: {0}", log.EntryType.ToString());
                Console.WriteLine("Time : {0}",log.TimeWritten);
                Console.WriteLine("Message : " + log.Message +"\n");
               
            }
        }


        static void ReadMessage(int totalEv, int writtenCustom, EventLog ev, EventLog custom)
        {
          
                if (totalEv== writtenCustom)
                {
                    return;
                }
                else
                {
                    for (int i = writtenCustom; i < totalEv; i++)
                    {
                        EventLogEntry CurrentEntry = ev.Entries[i];
                        custom.WriteEntry(CurrentEntry.Message, EventLogEntryType.Error);
                        Console.WriteLine("Machine: {0} \nNew message:{1} ",ev.MachineName,CurrentEntry.Message);
                    }
                }
               
          }


        static EventLog CreateLog(string logName,string sourceName)
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
                ev= null;
                Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
            }
            return ev;
        }
    


    }
}
