using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class ServiceCertValidator : X509CertificateValidator
    {
        public override void Validate(X509Certificate2 certificate)
        {
            string name = certificate.Subject.ToString();
            string[] parts = name.Split('_');
            string a = parts[parts.Length - 1];

            if (a != "MainCertCA")
            {
                throw new Exception("Nije validan izdavalac sertifikata");
            }
        }
    }
}
