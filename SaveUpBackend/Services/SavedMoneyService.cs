using AutoMapper;
using MongoDB.Bson;
using SaveUpBackend.Interfaces;
using SaveUpModels.DTOs.Requests;
using SaveUpModels.DTOs.Responses;
using SaveUpModels.Models;

namespace SaveUpBackend.Services
{
    public class SavedMoneyService : ISavedMoneyService
    {
        private readonly IMongoDbContext _context;
        private readonly IMapper _mapper;

        public SavedMoneyService(IMongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SavedMoneyDTO>> GetAll()
        {
            var savedMoney = await _context.SavedMoney.FindAll();

            return _mapper.Map<IEnumerable<SavedMoneyDTO>>(savedMoney);
        }

        public async Task<SavedMoneyDTO> GetById(string id)
        {
            var savedMoney = await _context.SavedMoney.FindByIdAsync(ObjectId.Parse(id));

            return _mapper.Map<SavedMoneyDTO>(savedMoney);
        }

        public async Task<SavedMoneyDTO> Create(SavedMoneyCreateDTO savedMoneyCreateDTO)
        {
            var savedMoney = _mapper.Map<SavedMoney>(savedMoneyCreateDTO);

            await _context.SavedMoney.InsertOneAsync(savedMoney);

            return _mapper.Map<SavedMoneyDTO>(savedMoney);
        }
    }
}
