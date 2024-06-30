using SaveUpModels.DTOs;
using System.Net;

namespace SaveUp.Interfaces
{
    /// <summary>
    /// Interface for the HTTPResponse
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IHTTPResponse<TResponse>
    {
        bool IsSuccess { get; }
        HttpStatusCode StatusCode { get; }

        Task<ErrorData?> ParseError();
        Task<TResponse?> ParseSuccess();
    }
}
