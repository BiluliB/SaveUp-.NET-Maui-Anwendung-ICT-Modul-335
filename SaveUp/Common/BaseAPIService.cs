using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SaveUp.Common.Helpers;
using SaveUp.Interfaces;
using System.Diagnostics;
using System.Text;

namespace SaveUp.Common
{
    public abstract class BaseAPIService<TCreateRequest, TUpdateRequest, TResponse> : IBaseAPIService<TCreateRequest, TUpdateRequest, TResponse>
        where TCreateRequest : class
        where TUpdateRequest : class
        where TResponse : class
    {
        /// <summary>
        /// The Configuration instance used to load the base url for the API
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The individual endpoint for the API-Controller of interest
        /// </summary>
        protected readonly string _endpoint;

        public HttpClient Client => _httpClient;

        /// <summary>
        /// The HttpClient instance used to send requests to the API
        /// </summary>
        protected readonly HttpClient _httpClient;

        /// <summary>
        /// Default base url for the API. Used if no value is found in appsettings.json
        /// </summary>
        /// 
        string _defaultBaseUrl =
#if ANDROID
        "http://10.0.2.2:5035/api/";
#else
        "http://localhost:5035/api/";
#endif

        /// <summary>
        /// Base url for the API. Loaded from appsettings.json or set to default value
        /// </summary>
        string _baseUrl;

        public BaseAPIService(IConfiguration configuration, string endpoint)
        {
            _configuration = configuration;
            _endpoint = endpoint;
            _httpClient = HTTPClientFactory.Create();

            // Set the timeout value to a smaller duration, e.g., 5 seconds
            _httpClient.Timeout = TimeSpan.FromSeconds(5);

            var baseUrl = _configuration["API:BaseURL"];
            if (!string.IsNullOrEmpty(baseUrl))
            {
                _baseUrl = baseUrl;
            }
            else
            {
                Console.WriteLine("WARNING: API:BaseURL not found in appsettings.json. Using default value: " + _defaultBaseUrl);
                _baseUrl = _defaultBaseUrl;
            }
        }

        /// <summary>
        /// Helper method to create the url for the API request
        /// </summary>
        /// <param name="parmas">Additional data</param>
        /// <returns>The BaseUrl with the enpoint. All params are added seperated by '/'</returns>
        protected string _url(params string[] parmas)
        {
            string url = _baseUrl + _endpoint;
            foreach (var param in parmas)
            {
                url += "/" + param;
            }
            return url;
        }

        /// <summary>
        /// Helper method to send a HTTP request to the API
        /// </summary>
        /// <param name="method">The method to use</param>
        /// <param name="url">The URL to send the Request to</param>
        /// <param name="data">The data to send</param>
        /// <returns>The HttpResponseMessage object</returns>
        protected async Task<HttpResponseMessage?> _sendRequest(HttpMethod method, string url, object? data = null)
        {
            var request = new HttpRequestMessage(method, url);

            if (data != null)
            {
                var json = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            try
            {
                return await _httpClient.SendAsync(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to send request to {url} - {method}");
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
