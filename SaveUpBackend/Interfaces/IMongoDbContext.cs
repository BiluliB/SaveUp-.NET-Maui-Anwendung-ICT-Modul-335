using SaveUpBackend.Data;
using SaveUpModels.Models;

namespace SaveUpBackend.Interfaces
{
    /// <summary>
    /// Interface for the MongoDbContext
    /// </summary>
    public interface IMongoDbContext
    {
        CollectionWrapper<SavedMoney> SavedMoney { get; }

        CollectionWrapper<T> Get<T>() where T : BaseModel;
    }
}