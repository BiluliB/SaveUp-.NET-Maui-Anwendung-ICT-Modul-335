using Newtonsoft.Json;
using SaveUp.Interfaces;
using SaveUpModels.DTOs;
using System.Diagnostics;
using System.Net;

namespace SaveUp.Models
{
    /// <summary>
    /// Will be returned from the API service to streamline error handling as well as parsing
    /// ParseSuccess and ParseError will return the parsed response/error
    /// </summary>
    /// <typeparam name="TResponse">The type of the response (only applies on success)</typeparam>
    public class HTTPResponse<TResponse> : IHTTPResponse<TResponse>
        {
            /// <summary>
            /// Should be used when handling the response from the API
            /// It will allow for streamlined parsing and error handling
            /// </summary>
            /// <param name="response">The response received from the API</param>
            public HTTPResponse(HttpResponseMessage? response)
            {
                if (response != null)
                {
                    _response = response;
                }
                else
                {
                    Debug.WriteLine("SaveUp-WARNING: Response was null");

                    _response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }
            }

            /// <summary>
            /// The response from the API
            /// </summary>
            private HttpResponseMessage _response { get; set; }

            /// <summary>
            /// Accessor for the success flag of the response
            /// </summary>
            public bool IsSuccess => _response.IsSuccessStatusCode;

            /// <summary>
            /// Accessor for the status code of the response
            /// </summary>
            public HttpStatusCode StatusCode => _response.StatusCode;

            public HttpResponseMessage Raw => _response;

            /// <summary>
            /// Core parsing method intended to be used for any parsing-like operations
            /// </summary>
            /// <typeparam name="T">Type to parse the response to</typeparam>
            /// <returns>The parsed response in the passed type; will be empty if non of the declaired fields is contained in the response</returns>
            private async Task<T?> _parse<T>()
            {
                var json = await _response.Content.ReadAsStringAsync();
                try
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Failed to parse response to {typeof(T).Name}");
                    return default;
                }
            }

            /// <summary>
            /// Used to parse the successfull response content in the correct type
            /// </summary>
            /// <returns>The parsed Response</returns>
            public async Task<TResponse?> ParseSuccess()
            {
                return await _parse<TResponse>();
            }

            /// <summary>
            /// Used to parse the error response content
            /// </summary>
            /// <returns>The parsed ErrorResponse</returns>
            public async Task<ErrorData?> ParseError()
            {
                return await _parse<ErrorData>();
            }
        }
    
}
