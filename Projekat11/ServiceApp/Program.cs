using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceApp
{
    class Program
    {
        public static string logName = null;
        public static string logSourceName = null;

        static void Main(string[] args)
        {       
            string servNameCrt = "wcfservice";
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            string port = "202"; //Ovaj port mijenja sourcename!
            string MachineName = Environment.MachineName;
            string[] parts = MachineName.Split('-');
            string MachineNameSplit = String.Format("{0}{1}", parts[0], parts[1]);
            logName = String.Format("{0}LogFile", MachineNameSplit);
            logSourceName = String.Format("{0}LogSourceName", port);
            string address = String.Format("net.tcp://localhost:{0}/WCFService",port);
            Audit newLog = new Audit(logName, logSourceName);

            ServiceHost host = new ServiceHost(typeof(WCFService));
            host.AddServiceEndpoint(typeof(IWCFService), binding, address);
            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            host.Credentials.ServiceCertificate.Certificate = CertificationManager.GetSingleCertificate(StoreName.My, StoreLocation.LocalMachine, servNameCrt);
            host.Open();
            Console.WriteLine("WCFService is opened. Press <enter> to finish...");
            Console.ReadLine();

            host.Close();
        }

    }
}
