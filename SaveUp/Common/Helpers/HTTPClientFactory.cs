namespace SaveUp.Common.Helpers
{
    public static class HTTPClientFactory
    {
        /// <summary>
        /// Create a new HttpClient with the ability to ignore SSL certificate errors
        /// </summary>
        /// <returns></returns>
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
