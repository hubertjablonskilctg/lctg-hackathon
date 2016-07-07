using System.Collections.Generic;
using Common.Mongo;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace hackaton_engine.Models
{
    public class Group : Entity
    {
        private Dictionary<int, Preference> _userPreferences = new Dictionary<int, Preference>();
        private Dictionary<int, int[]> _userHotelUpVotes = new Dictionary<int, int[]>();

        public int AdminUserId { get; set; }
        public int[] UserIds { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, Preference> UserPreferences
        {
            get { return _userPreferences; }
            set { _userPreferences = value; }
        }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int[]> UserHotelUpVotes
        {
            get { return _userHotelUpVotes; }
            set { _userHotelUpVotes = value; }
        }
    }
}