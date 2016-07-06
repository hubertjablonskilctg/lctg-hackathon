using MongoDB.Bson.Serialization.Attributes;

namespace Common.Mongo
{
    public class Entity
    {
        [BsonId]
        public int Id { get; set; }
    }
}