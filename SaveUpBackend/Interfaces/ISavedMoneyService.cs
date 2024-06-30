using SaveUpModels.DTOs.Requests;
using SaveUpModels.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaveUpBackend.Interfaces
{
    public interface ISavedMoneyService
    {
        Task<IEnumerable<SavedMoneyDTO>> GetAll();
        Task<SavedMoneyDTO> GetById(string id);
        Task<SavedMoneyDTO> Create(SavedMoneyCreateDTO savedMoneyCreateDTO);
        Task<List<SavedMoneyDTO>> GetEntriesForToday();
    }
}
