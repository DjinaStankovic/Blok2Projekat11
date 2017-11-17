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

            /// Check whether the subjectName of the certificate is exactly the same as the given "subjectName"
            //foreach (X509Certificate2 c in certCollection)
            //{
            //    if (c.SubjectName.Name.Equals(string.Format("CN={0}", subjectName)))
            //    {
            //        return c;
            //    }
            //}

            return certCollection;
            
        }
    }
}
