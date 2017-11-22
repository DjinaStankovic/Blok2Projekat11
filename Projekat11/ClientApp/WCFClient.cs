using Common;
using SecurityManager;
using ServiceApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public class WCFClient: ChannelFactory<IWCFService>, IWCFService, IDisposable
    {
        IWCFService factory;

        public WCFClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            this.Credentials.ClientCertificate.Certificate = CertificationManager.GetSingleCertificate(StoreName.My, StoreLocation.LocalMachine,Program.user);
            factory = this.CreateChannel();
        }

        public bool CreateFile(string path)
        {
            bool allowed = false;
            factory.SendUser(Program.user);
            try
            {
                allowed = factory.CreateFile(path);
                Console.WriteLine(">> CreateFile() -> {0}", allowed);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to CreateFile(). {0}", e.Message);
            }

            return allowed;
        }

        public bool DeleteFile(string path)
        {
            bool allowed = false;
            factory.SendUser(Program.user);
            try
            {
                allowed = factory.DeleteFile(path);
                Console.WriteLine(">> DeleteFile() -> {0}", allowed);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to DeleteFile(). {0}", e.Message);
            }

            return allowed;
        }

        public bool WriteInFile(string path, string content)
        {
            bool allowed = false;
            factory.SendUser(Program.user);
            try
            {
                allowed = factory.WriteInFile(path, content);
                Console.WriteLine(">> WriteInFile() -> {0}", allowed);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to WriteInFile(). {0}", e.Message);
            }

            return allowed;
        }


        public string ReadFromFile(string path)
        {
            string allowed = String.Empty;
            factory.SendUser(Program.user);
            try
            {
                allowed = factory.ReadFromFile(path);
                Console.WriteLine(">> ReadFromFile() -> \n {0}", allowed);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to ReadFromFile(). {0}", e.Message);
            }

            return allowed;
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }
            this.Close();
        }

        public void SendUser(string user)
        {

        }

    }
}
