using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SaveUpBackend.Models
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
