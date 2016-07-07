﻿using System;
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

        public Group()
        {
            CurrentTripPreparationStage = TripPreparationStages.GatherPreferences;
            IsPublic = false;
            Comments = new List<Tuple<int, DateTime, string>>();
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

        //userId, DateTime.Now, comment
        public IList<Tuple<int, DateTime, string>> Comments { get; set; }

        public bool IsPublic { get; set; }
    }
}