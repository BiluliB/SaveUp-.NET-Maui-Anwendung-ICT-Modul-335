using Microsoft.Extensions.Configuration;
using SaveUp.Common;
using SaveUp.Interfaces;
using SaveUp.Models;
using SaveUpModels.DTOs.Requests;
using SaveUpModels.DTOs.Responses;

namespace SaveUp.Services
{
    /// <summary>
    /// Service for the SavedMoneyServiceAPI
    /// </summary>
    public class SavedMoneyServiceAPI : BaseAPIService<SavedMoneyCreateDTO, IUpdate, SavedMoneyDTO>, ISavedMoneyServiceAPI
    {
        public SavedMoneyServiceAPI(IConfiguration configuration) : base(configuration, "SavedMoney")
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
