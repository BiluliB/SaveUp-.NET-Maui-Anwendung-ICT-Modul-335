using MongoDB.Bson;
using MongoDB.Driver;
using SaveUpBackend.Common;
using SaveUpModels.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace SaveUpBackend.Data
{
    public class CollectionWrapper<T> where T : BaseModel
    {
        private readonly IMongoCollection<T> _collection;

        public CollectionWrapper(IMongoDatabase database, string collectionName)
        {
            var collectionList = database.ListCollectionNames().ToList();

            if (!collectionList.Contains(collectionName))
            {
                database.CreateCollection(collectionName);
            }
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<List<T>> FindWithProxies(FilterDefinition<T> filter)
        {
            var aggregation = _collection.Aggregate().Match(filter);

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<ProxyAttribute>();
                if (attribute != null)
                {
                    var lookup = new BsonDocument
                    {
                        {
                            "$lookup", new BsonDocument
                            {
                                { "from", attribute.PluralName },
                                { "localField", $"{attribute.ForeignKey}" },
                                { "foreignField", "_id" },
                                { "as", property.Name.ToLower() }
                            }
                        }
                    };

                    var unwind = new BsonDocument
                    {
                        {
                            "$unwind", new BsonDocument
                            {
                                { "path", $"${property.Name.ToLower()}" },
                                { "preserveNullAndEmptyArrays", true }
                            }
                        }
                    };

                    aggregation = aggregation.AppendStage<T>(lookup).AppendStage<T>(unwind);
                }
            }

            return await aggregation.ToListAsync();
        }

        public async Task<List<T>> FindAll()
        {
            return await _collection.Find(FilterDefinition<T>.Empty).ToListAsync();
        }

        public async Task<T> FindByIdAsync(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task InsertOneAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }
    }
}
