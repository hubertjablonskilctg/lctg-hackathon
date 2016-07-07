using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Mongo.Repositories;
using hackaton_engine.Helpers;
using hackaton_engine.Models;
using MongoDB.Driver;

namespace hackaton_engine.Controllers
{
    [RoutePrefix("api/Group")]
    public class GroupController : ApiController
    {
        private readonly IMongoRepository<Group> _groupRepository;
        private readonly IMongoRepository<User> _userRepository;

        public GroupController(IMongoRepository<Group> groupRepository, IMongoRepository<User> userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var group = _groupRepository.Get(id);
            group.HydrateUsers(_userRepository);

            return Ok(group);
        }

        [HttpGet]
        [Route("{groupId}/hotelvotes")]
        public IHttpActionResult GetDHotelVotes(int groupId)
        {
            var group = _groupRepository.Get(groupId);
            var preferencedHotels = GroupHotelSorter.GetHotelsByGroupPreferences(groupId);

            var hotelIdUpVotes = group.UserHotelUpVotes.SelectMany(x => x.Value);
            var hotels = preferencedHotels.OrderByDescending(h => hotelIdUpVotes.Count(hid => hid == h.Id));

            foreach (var hotel in hotels)
            {
                hotel.HydrateUsersWhoUpvoted(groupId, _groupRepository, _userRepository);
            }

            return Json<IEnumerable<Hotel>>(hotels);
        }

        [HttpGet]
        [Route("{id}/hotelpreferences")]
        public IHttpActionResult GetHotelPreferences(int id)
        {
            return Json<IEnumerable<Hotel>>(GroupHotelSorter.GetHotelsByGroupPreferences(id).Take(20));
        }

        [HttpPost]
        [Route("AddUsers/{groupId:int?}")]
        // if groupId empty, creates new group
        public IHttpActionResult AddUsersToGroup([FromBody] string[] newUserEmails, [FromUri] int? groupId = null)
        {
            var userIds = CreateUsers(newUserEmails).ToArray();

            Group group;

            if (!groupId.HasValue)
            {
                group = new Group()
                {
                    Id = _groupRepository.GetHighestId() + 1,
                    AdminUserId = userIds.First(),
                    UserIds = userIds.ToArray()
                };

                _groupRepository.Add(group);

            }
            else
            {
                group = _groupRepository.Get(groupId.Value);
                group.UserIds = group.UserIds.Concat(userIds).Distinct().ToArray();

                _groupRepository.Update(groupId.Value, group);
            }

            return Ok(group);
        }

        private IEnumerable<int> CreateUsers(string[] userEmails)
        {
            var users = _userRepository.GetAll().ToList();
            var ids = new HashSet<int>();

            foreach (var userEmail in userEmails)
            {
                // FirstOrDefault, not SingleOD because we'd rather 
                // have incorrect data than exception during presentation
                var existingUser = users.FirstOrDefault(u => u.Email == userEmail);

                if (existingUser != null)
                {
                    ids.Add(existingUser.Id);
                }
                else
                {
                    var newUser = new User()
                    {
                        Email = userEmail,
                        Id = _userRepository.GetHighestId() + 1
                    };

                    // create user
                    _userRepository.Add(newUser);

                    ids.Add(newUser.Id);
                }
            }

            return ids;
        }

        /// <summary>
        /// BRUTAL NIGHTLY HACKATHON CODING
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <param name="preferenceName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ChangePreferences/{userId}/{groupId}/{preferenceName}")]
        public IHttpActionResult ChangeUserPreferences(int userId, int groupId, string preferenceName)
        {
            var group = _groupRepository.Get(groupId);

            try
            {
                Tags selectedTag = ParseEnum<Tags>(preferenceName);
                if (group.UserPreferences[userId].Tags.Contains(selectedTag))
                {
                    group.UserPreferences[userId].Tags =
                        group.UserPreferences[userId].Tags.Except(new[] {selectedTag}).ToArray();
                }
                else
                {
                    group.UserPreferences[userId].Tags = group.UserPreferences[userId].Tags.Concat(new[] {selectedTag}).ToArray();
                }

                _groupRepository.Update(groupId, group);
            }
            catch
            {
            }

            try
            {
                Localizations selectedTag = ParseEnum<Localizations>(preferenceName);
                if (group.UserPreferences[userId].Localizations.Contains(selectedTag))
                {
                    group.UserPreferences[userId].Localizations =
                        group.UserPreferences[userId].Localizations.Except(new[] { selectedTag }).ToArray();
                }
                else
                {
                    group.UserPreferences[userId].Localizations = group.UserPreferences[userId].Localizations.Concat(new[] { selectedTag }).ToArray();
                }

                _groupRepository.Update(groupId, group);
            }
            catch
            {
            }

            try
            {
                MustHaves selectedTag = ParseEnum<MustHaves>(preferenceName);
                if (group.UserPreferences[userId].MustHaves.Contains(selectedTag))
                {
                    group.UserPreferences[userId].MustHaves =
                        group.UserPreferences[userId].MustHaves.Except(new[] { selectedTag }).ToArray();
                }
                else
                {
                    group.UserPreferences[userId].MustHaves = group.UserPreferences[userId].MustHaves.Concat(new[] { selectedTag }).ToArray();
                }

                _groupRepository.Update(groupId, group);
            }
            catch
            {
            }

            return Ok(group);
        }

        static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        [HttpPost]
        [Route("ChangePreferences/{userId}/{groupId}")]
        public IHttpActionResult ChangeUserPreferences([FromUri] int userId, [FromUri] int groupId, Preference preferences)
        {
            var group = _groupRepository.Get(groupId);
            group.UserPreferences[userId] = preferences;
            _groupRepository.Update(groupId, group);

            return Ok(group);
        }

        [HttpGet]
        [Route("UpvoteHotel/{userId}/{groupId}/{hotelId}/{upvote}")]
        public IHttpActionResult UpvoteHotel(int userId, int groupId, int hotelId, bool upvote)
        {
            var group = _groupRepository.Get(groupId);

            var userHotelUpvotes = new HashSet<int>();
            if (upvote)
            {
                userHotelUpvotes.Add(hotelId);
                if (group.UserHotelUpVotes.ContainsKey(userId))
                {
                    userHotelUpvotes.UnionWith(group.UserHotelUpVotes[userId]);
                }
            }
            else
            {
                if (group.UserHotelUpVotes.ContainsKey(userId))
                {
                    userHotelUpvotes.UnionWith(group.UserHotelUpVotes[userId]);
                    userHotelUpvotes.Remove(hotelId);
                }
            }

            group.UserHotelUpVotes[userId] = userHotelUpvotes.ToArray();

            _groupRepository.Update(group.Id, group);

            return Ok(group);
        }

        [HttpGet]
        [Route("AdvanceTripStage/{groupId}")]
        public IHttpActionResult AdvanceTripStage(int groupId)
        {
            var group = _groupRepository.Get(groupId);

            var currentTripPreparationStageId = (int)group.CurrentTripPreparationStage;
            var maximumTripPreparationStagesValue = Enum.GetValues(typeof(TripPreparationStages)).Cast<int>().Max();

            if (currentTripPreparationStageId >= maximumTripPreparationStagesValue)
            {
                throw new InvalidOperationException("Maximum stage reached.");
            }

            group.CurrentTripPreparationStage = (TripPreparationStages) (currentTripPreparationStageId + 1);

            _groupRepository.Update(group.Id, group);

            return Ok(group);
        }
    }
}
