using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models
{
    public class BaseModel
    {
        [BsonId]
        public int Id { get; set; }
    }
}