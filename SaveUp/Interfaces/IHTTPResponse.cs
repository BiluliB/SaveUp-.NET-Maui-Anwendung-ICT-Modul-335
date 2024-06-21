using SaveUpModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SaveUp.Interfaces
{
    public interface IHTTPResponse<TResponse>
    {
        bool IsSuccess { get; }
        HttpStatusCode StatusCode { get; }

        Task<ErrorData?> ParseError();
        Task<TResponse?> ParseSuccess();
    }
}
