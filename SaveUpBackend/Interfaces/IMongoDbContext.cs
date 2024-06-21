using SaveUpBackend.Data;
using SaveUpModels.Models;

namespace SaveUpBackend.Interfaces
{
    public interface IMongoDbContext
    {
        CollectionWrapper<SavedMoney> SavedMoney { get; }

        CollectionWrapper<T> Get<T>() where T : BaseModel;
    }
}