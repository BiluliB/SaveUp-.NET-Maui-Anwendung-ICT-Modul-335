using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveUp.Common.Helpers
{
    public static class HTTPClientFactory
    {
        public static HttpClient Create()
        {
            var client = new HttpClient(new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                }

            });
            return client;
        }
    }
}
