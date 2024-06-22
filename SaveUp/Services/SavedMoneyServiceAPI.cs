using Microsoft.Extensions.Configuration;
using SaveUp.Common;
using SaveUpModels.DTOs.Responses;
using SaveUpModels.DTOs.Requests;
using SaveUp.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SaveUp.Models;

namespace SaveUp.Services
{
    public class SavedMoneyServiceAPI : BaseAPIService<SavedMoneyCreateDTO, IUpdate, SavedMoneyDTO>, ISavedMoneyServiceAPI
    {
        public SavedMoneyServiceAPI(IConfiguration configuration) : base(configuration, "savedmoney")
        {
        }

        public async Task<HTTPResponse<List<SavedMoneyDTO>>> GetAllAsync()
        {
            var res = await _sendRequest(HttpMethod.Get, _url());
            return new HTTPResponse<List<SavedMoneyDTO>>(res);
        }

        public async Task<HTTPResponse<SavedMoneyDTO>> CreateAsync(SavedMoneyCreateDTO savedMoneyCreateDTO)
        {
            var res = await _sendRequest(HttpMethod.Post, _url(), savedMoneyCreateDTO);
            return new HTTPResponse<SavedMoneyDTO>(res);
        }

        public async Task<HTTPResponse<List<SavedMoneyDTO>>> GetTodayAsync()
        {
            var res = await _sendRequest(HttpMethod.Get, _url("today"));
            return new HTTPResponse<List<SavedMoneyDTO>>(res);
        }

    }
}
