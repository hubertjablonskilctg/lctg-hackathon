using System;
using System.Collections.Generic;
using System.Linq;
using Common.Mongo;
using Common.Mongo.Repositories;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace hackaton_engine.Models
{
    public class Group : Entity
    {
        private Dictionary<int, Preference> _userPreferences = new Dictionary<int, Preference>();
        private Dictionary<int, int[]> _userHotelUpVotes = new Dictionary<int, int[]>();
        private int[] _userIds = new int[0];
        private IList<Tuple<string, DateTime, string>> _comments = new List<Tuple<string, DateTime, string>>();

        public Group()
        {
            CurrentTripPreparationStage = TripPreparationStages.GatherPreferences;
            IsPublic = false;
        }

        public int AdminUserId { get; set; }
        public string Name { get; set; }

        public int[] UserIds
        {
            get { return _userIds; }
            set { _userIds = value; }
        }

        /// <summary>
        /// Shows full user data from UserIds... Can be hydrated by HydrateUsers().
        /// </summary>
        public IEnumerable<User> Users { get; set; }

        public void HydrateUsers(IMongoRepository<User> userRepository)
        {
            Users = UserIds.Select(userId => userRepository.Get(userId)).ToList();
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

        //username, creation date, comment
        public IList<Tuple<string, DateTime, string>> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public bool IsPublic { get; set; }
    }
}