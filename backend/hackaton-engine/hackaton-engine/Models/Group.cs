using System.Collections.Generic;
using Common.Mongo;

namespace hackaton_engine.Models
{
    public class Group : Entity
    {
        public int AdminUserId { get; set; }
        public int[] UserIds { get; set; }
        public Dictionary<int, Preference> UserPreferences { get; set; }
        public Dictionary<int, int[]> UserHotelUpVotes { get; set; }
    }
}