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
        private int[] _userIds = new int[0];

        public Group()
        {
            CurrentTripPreparationStage = TripPreparationStages.GatherPreferences;
        }

        public int AdminUserId { get; set; }

        public int[] UserIds
        {
            get { return _userIds; }
            set { _userIds = value; }
        }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        // userId, preference
        public Dictionary<int, Preference> UserPreferences
        {
            get { return _userPreferences; }
            set { _userPreferences = value; }
        }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        // userId, hotelIds[]
        public Dictionary<int, int[]> UserHotelUpVotes
        {
            get { return _userHotelUpVotes; }
            set { _userHotelUpVotes = value; }
        }

        public TripPreparationStages CurrentTripPreparationStage { get; set; }
    }
}