using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using SaveUpBackend.Common;
using SaveUpModels.Models;
using System.Reflection;

namespace SaveUpBackend.Data
{
    public class CollectionWrapper<T>
        where T : BaseModel
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<T> _collection;
        private readonly IMongoDatabase _database;

        public CollectionWrapper(IMapper mapper, IMongoDatabase database, string collectionName)
        {
            _mapper = mapper;
            _database = database;
            var collectionList = _database.ListCollectionNames().ToList();

            if (!collectionList.Contains(collectionName))
            {
                _database.CreateCollection(collectionName);
            }
            _collection = _database.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Finds all entities in the collection and returns them as a list.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>list</returns>
        public async Task<List<T>> FindWithProxies(FilterDefinition<T> filter)
        {
            var aggregation = _collection.Aggregate<T>()
                .Match(filter);

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
            var list = await aggregation.ToListAsync();
            return list;
        }

        /// <summary>
        /// Adds a proxy to the collection.
        /// </summary>
        /// <returns>await _collection.InsertOneAsync(entity)</returns>
        public async Task<bool> Any()
        {
            return await _collection.CountDocumentsAsync(FilterDefinition<T>.Empty) > 0;
        }

        /// <summary>
        /// Inserts a single entity into the collection.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertOneAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// Finds a user by username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>result.FirstOrDefault()</returns>
        public async Task<T> FindByUsernameAsync(string username)
        {
            var result = await FindWithProxies(Builders<T>.Filter.Eq("name", username));
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Finds a user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>result.FirstOrDefault()</returns>
        public async Task<T> FindByIdAsync(ObjectId id)
        {
            var result = await FindWithProxies(Builders<T>.Filter.Eq("_id", id));
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Replaces an entity in the collection with the given entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task ReplaceOneAsync(T entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        /// <summary>
        /// Deletes an entity from the collection by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteOneAsync(ObjectId id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        /// <summary>
        /// Finds all entities in the collection and returns them as a list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> FindAll()
        {
            return await _collection.Find(FilterDefinition<T>.Empty).ToListAsync();
        }
    }
}
