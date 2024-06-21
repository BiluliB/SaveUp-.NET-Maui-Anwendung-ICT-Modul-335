using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SaveUpModels.Models
{
    /// <summary>
    /// Base model for all models
    /// </summary>
    public abstract class BaseModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
