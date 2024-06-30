using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using SaveUpBackend.Interfaces;
using SaveUpModels.DTOs.Requests;
using SaveUpModels.DTOs.Responses;
using SaveUpModels.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            Console.WriteLine(savedMoneyCreateDTO.Date);

            // Das Datum aus dem DTO verwenden
            savedMoney.Date = savedMoneyCreateDTO.Date;
            await _context.SavedMoney.InsertOneAsync(savedMoney);
            return _mapper.Map<SavedMoneyDTO>(savedMoney);
        }



        public async Task<List<SavedMoneyDTO>> GetByDateAsync(DateTime date)
        {
            // Start und Ende des Tages in UTC berechnen
            var startOfDayUtc = DateTime.SpecifyKind(date.Date, DateTimeKind.Local);
            var endOfDayUtc = startOfDayUtc.AddDays(1);

            var filter = Builders<SavedMoney>.Filter.Gte(sm => sm.Date, startOfDayUtc) &
                         Builders<SavedMoney>.Filter.Lt(sm => sm.Date, endOfDayUtc);

            var savedMoney = await _context.SavedMoney.FindWithProxies(filter);
            return _mapper.Map<List<SavedMoneyDTO>>(savedMoney);
        }

        public async Task<List<SavedMoneyDTO>> GetEntriesForToday()
        {
            var today = DateTime.Now.Date;
            return await GetByDateAsync(today);
        }




    }
}
