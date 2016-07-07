using System.Collections.Generic;
using Common.Mongo;

namespace hackaton_engine.Models
{
    public class Group : Entity
    {
        public int AdminUserId { get; set; }
        public HashSet<int> UserIds { get; set; }

        // id, preference
        public Dictionary<int, Preference> UserPreferences { get; set; }

        // id, preference
        public Dictionary<int, int[]> UserHotelUpVotes { get; set; }
    }
}