using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class ClientCertValidator: X509CertificateValidator
    {
        public override void Validate(X509Certificate2 certificate)
        {
            string name = certificate.Subject.ToString();
            string[] parts = name.Split('_');
            string company = parts[parts.Length - 1];

            if (company != "MainCertCA")
            {
                throw new Exception("The certificate issuer is not valid!");
            }
        }

    }
}
