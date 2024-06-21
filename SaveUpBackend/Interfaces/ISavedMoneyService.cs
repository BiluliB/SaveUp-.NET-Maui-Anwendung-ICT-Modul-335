using SaveUpModels.DTOs.Requests;
using SaveUpModels.DTOs.Responses;

namespace SaveUpBackend.Interfaces
{
    public interface ISavedMoneyService
    {
        Task<IEnumerable<SavedMoneyDTO>> GetAll();
        Task<SavedMoneyDTO> GetById(string id);

        Task<SavedMoneyDTO> Create(SavedMoneyCreateDTO savedMoneyCreateDTO);
    }
}