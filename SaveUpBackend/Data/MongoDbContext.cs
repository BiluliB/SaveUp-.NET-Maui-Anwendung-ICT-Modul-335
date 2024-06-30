using AutoMapper;
using MongoDB.Driver;
using SaveUpBackend.Interfaces;
using SaveUpModels.Models;

namespace SaveUpBackend.Data
{
    /// <summary>
    /// MongoDbContext for the MongoDB
    /// </summary>
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMapper _mapper;

        public MongoDbContext(IConfiguration configuration, IMapper mapper)
        {
            _mapper = mapper;

            var mongoSection = configuration.GetSection("MongoDb");
            var url = mongoSection.GetValue<string>("Url");
            var database = mongoSection.GetValue<string>("Database");

            var client = new MongoClient(url);
            _database = client.GetDatabase(database);
        }

        /// <summary>
        /// Collection wrapper for the SavedMoney collection
        /// </summary>
        public CollectionWrapper<SavedMoney> SavedMoney => new(_database, "SavedMoney");

        public CollectionWrapper<T> Get<T>() where T : BaseModel
        {
            var propertyInfo = GetType()
                .GetProperties()
                .FirstOrDefault(p => p.PropertyType == typeof(CollectionWrapper<T>));
            if (propertyInfo != null)
            {
                return (CollectionWrapper<T>)propertyInfo.GetValue(this)!;
            }
            else
            {
                throw new InvalidOperationException($"No collection wrapper found for type {typeof(T).Name}");
            }
        }
    }
}
