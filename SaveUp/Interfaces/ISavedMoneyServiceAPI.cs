using SaveUp.Models;
using SaveUpModels.DTOs.Requests;
using SaveUpModels.DTOs.Responses;

namespace SaveUp.Interfaces
{
    /// <summary>
    /// Interface for the SavedMoneyServiceAPI
    /// </summary>
    public interface ISavedMoneyServiceAPI
    {
        Task<HTTPResponse<SavedMoneyDTO>> CreateAsync(SavedMoneyCreateDTO savedMoneyCreateDTO);
        Task<HTTPResponse<List<SavedMoneyDTO>>> GetAllAsync();
        Task<HTTPResponse<List<SavedMoneyDTO>>> GetTodayAsync();
    }
}