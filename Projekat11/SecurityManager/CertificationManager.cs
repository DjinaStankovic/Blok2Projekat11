using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class CertificationManager
    {
        public static List<X509Certificate2> GetCertificateFromStorage(StoreName storeName, StoreLocation storeLocation)
        {
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);

            List<X509Certificate2> certCollection = new List<X509Certificate2>();
            foreach (var cert in store.Certificates)
            {
                certCollection.Add(cert);
            }

            return certCollection;          
        }

        public static X509Certificate2 GetSingleCertificate(StoreName storeName,StoreLocation storeLocation,string user)
        {
            string userCN = String.Format("CN={0}", user);
            X509Store store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2 certificate = new X509Certificate2();
            List<X509Certificate2> certCollection = new List<X509Certificate2>();
            foreach (var cert in store.Certificates)
            {
                certCollection.Add(cert);
            }

            foreach (X509Certificate2 cert in certCollection)
            {
                string[] names = cert.Subject.Split('_');

                if (names[0] == userCN)
                {
                    certificate = cert;
                    break;
                }      
            }
            return certificate;
        }

    }
}
