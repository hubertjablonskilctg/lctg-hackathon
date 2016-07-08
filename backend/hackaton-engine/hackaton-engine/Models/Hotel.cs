using System.Collections.Generic;
using System.Linq;
using Common.Mongo;
using Common.Mongo.Repositories;
using MongoDB.Bson.Serialization.Attributes;

namespace hackaton_engine.Models
{
    public class Hotel : Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public HotelRating Rating { get; set; }
        public string Address { get; set; }
        public byte[] ImageSource { get; set; }
        public string ImageUrl { get; set; }
        public Tags[] Tags { get; set; }
        public Localizations Localization { get; set; }
        public MustHaves[] MustHaves { get; set; }

        // used only for search results
        [BsonIgnore]
        public double SearchScore { get; set; }

        public IEnumerable<User> UsersWhoUpvoted { get; set; }

        public void HydrateUsersWhoUpvoted(int groupId, IMongoRepository<Group> groupRepository, IMongoRepository<User> userRepository)
        {
            var group = groupRepository.GetAll().FirstOrDefault(g => g.Id == groupId);
            var usersWhoUpvoted = new List<User>();

            foreach (KeyValuePair<int, int[]> kvp in group.UserHotelUpVotes)
            {
                var userId = kvp.Key;
                var userUpvotedHotelIds = kvp.Value;

                if (userUpvotedHotelIds.Contains(Id))
                {
                    usersWhoUpvoted.Add(userRepository.Get(userId));
                }
            }

            UsersWhoUpvoted = usersWhoUpvoted.ToArray();
        }
    }
}