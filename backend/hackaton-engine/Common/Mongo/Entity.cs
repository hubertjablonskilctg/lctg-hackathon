using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models
{
    public class Entity
    {
        [BsonId]
        public int Id { get; set; }
    }
}