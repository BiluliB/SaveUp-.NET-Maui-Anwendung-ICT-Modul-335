using AutoMapper;
using SaveUpBackend.DTOs.Responses;
using SaveUpBackend.DTOs.Requests;
using SaveUpBackend.Models;

namespace SaveUpBackend.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<SavedMoney, SavedMoneyDTO>();
            CreateMap<SavedMoneyCreateDTO, SavedMoney>();
        }
    }
}
