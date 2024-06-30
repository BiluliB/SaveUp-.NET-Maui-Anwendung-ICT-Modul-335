using AutoMapper;
using SaveUpModels.DTOs.Requests;
using SaveUpModels.DTOs.Responses;
using SaveUpModels.Models;

namespace SaveUpBackend.Common
{
    /// <summary>
    /// MappingProfile for the AutoMapper
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SavedMoney, SavedMoneyDTO>();
            CreateMap<SavedMoneyCreateDTO, SavedMoney>();
        }
    }
}
