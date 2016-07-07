using System.Collections.Generic;
using Common.Mongo;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace hackaton_engine.Models
{
    public class Group : Entity
    {
        public int AdminUserId { get; set; }
        public HashSet<int> UserIds { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, Preference> UserPreferences { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int[]> UserHotelUpVotes { get; set; }
    }
}