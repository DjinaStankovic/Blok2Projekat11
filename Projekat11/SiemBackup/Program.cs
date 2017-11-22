using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SiemBackup
{
    class Program
    {
        public static EventLog backupLog1=null;
        public static EventLog backupLog2 = null;
        public static EventLog backupLog3 = null;
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string port = "202";
            string address = String.Format("net.tcp://localhost:{0}/BackupService", port);
            BackupService backupService = new BackupService();
            backupLog1 = backupService.CreateLog("1stLog1Backup", "1FirstLogSource");
            backupLog2 = backupService.CreateLog("2ndLog2Backup", "2SecondLogSource");
            backupLog3 = backupService.CreateLog("3rdLog3Backup", "3ThridLogSource");

            ServiceHost host = new ServiceHost(typeof(BackupService));
            host.AddServiceEndpoint(typeof(iBackupService), binding, address);
            host.Open();
            Console.WriteLine("Pokrenut backup servis!");
            Console.ReadLine();
            host.Close();

        }
    }
}
