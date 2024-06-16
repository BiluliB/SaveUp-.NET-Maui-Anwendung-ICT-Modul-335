using SaveUpBackend.DTOs.Responses;
using SaveUpBackend.DTOs.Requests;

namespace SaveUpBackend.Interfaces
{
    public interface ISavedMoneyService
    {
        Task<IEnumerable<SavedMoneyDTO>> GetAll();
        Task<SavedMoneyDTO> GetById(string id);

        Task<SavedMoneyDTO> Create(SavedMoneyCreateDTO savedMoneyCreateDTO);
    }
}