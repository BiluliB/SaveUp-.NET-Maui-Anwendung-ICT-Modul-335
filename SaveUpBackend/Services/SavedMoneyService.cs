using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using SaveUpBackend.Interfaces;
using SaveUpModels.DTOs.Requests;
using SaveUpModels.DTOs.Responses;
using SaveUpModels.Models;

namespace SaveUpBackend.Services
{
    /// <summary>
    /// Service for the SavedMoney
    /// </summary>
    public class SavedMoneyService : ISavedMoneyService
    {
        private readonly IMongoDbContext _context;
        private readonly IMapper _mapper;

        public SavedMoneyService(IMongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all SavedMoney entries
        /// </summary>
        /// <returns>SavedMoney entries</returns>
        public async Task<IEnumerable<SavedMoneyDTO>> GetAll()
        {
            var savedMoney = await _context.SavedMoney.FindAll();
            return _mapper.Map<IEnumerable<SavedMoneyDTO>>(savedMoney);
        }

        /// <summary>
        /// Get a SavedMoney entry by id
        /// </summary>
        /// <param name="id">id of the SavedMoney entry</param>
        /// <returns>savedMoney entry</returns>
        public async Task<SavedMoneyDTO> GetById(string id)
        {
            var savedMoney = await _context.SavedMoney.FindByIdAsync(ObjectId.Parse(id));
            return _mapper.Map<SavedMoneyDTO>(savedMoney);
        }

        /// <summary>
        /// Create a new SavedMoney entry
        /// </summary>
        /// <param name="savedMoneyCreateDTO">savedMoneyCreateDTO</param>
        /// <returns>savedMoney entry</returns>
        public async Task<SavedMoneyDTO> Create(SavedMoneyCreateDTO savedMoneyCreateDTO)
        {
            var savedMoney = _mapper.Map<SavedMoney>(savedMoneyCreateDTO);

            Console.WriteLine(savedMoneyCreateDTO.Date);

            savedMoney.Date = savedMoneyCreateDTO.Date;
            await _context.SavedMoney.InsertOneAsync(savedMoney);
            return _mapper.Map<SavedMoneyDTO>(savedMoney);
        }

        /// <summary>
        /// Get all SavedMoney entries for a specific date
        /// </summary>
        /// <param name="date">time of the day</param> 
        /// <returns>savedMoney entries</returns>
        public async Task<List<SavedMoneyDTO>> GetByDateAsync(DateTime date)
        {
            var startOfDayUtc = DateTime.SpecifyKind(date.Date, DateTimeKind.Local);
            var endOfDayUtc = startOfDayUtc.AddDays(1);

            var filter = Builders<SavedMoney>.Filter.Gte(sm => sm.Date, startOfDayUtc) &
                         Builders<SavedMoney>.Filter.Lt(sm => sm.Date, endOfDayUtc);

            var savedMoney = await _context.SavedMoney.FindWithProxies(filter);
            return _mapper.Map<List<SavedMoneyDTO>>(savedMoney);
        }

        /// <summary>
        /// Get all SavedMoney entries for today
        /// </summary>
        /// <returns>get all savedMoney entries for today</returns>
        public async Task<List<SavedMoneyDTO>> GetEntriesForToday()
        {
            var today = DateTime.Now.Date;
            return await GetByDateAsync(today);
        }
    }
}
