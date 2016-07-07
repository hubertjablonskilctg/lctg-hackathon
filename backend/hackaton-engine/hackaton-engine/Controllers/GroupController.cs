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
        private readonly IMongoRepository<Hotel> _hotelRepository;

        public GroupController(IMongoRepository<Group> groupRepository, IMongoRepository<Hotel> hotelRepository)
        {
            _groupRepository = groupRepository;
            _hotelRepository = hotelRepository;
        }

        [HttpGet]
        [Route("{id}/hotelvotes")]
        public IHttpActionResult GetHotelVotes(int id)
        {
            var group = _groupRepository.Get(id);
            var preferencedHotels = GroupHotelSorter.GetHotelsByGroupPreferences(id);

            var hotelIdUpVotes = group.UserHotelUpVotes.SelectMany(x => x.Value);
            var hotels = preferencedHotels.OrderByDescending(h => hotelIdUpVotes.Count(hid => hid == h.Id));

            return Json<IEnumerable<Hotel>>(hotels);
        }

        [HttpGet]
        [Route("{id}/hotelpreferences")]
        public IHttpActionResult GetHotelPreferences(int id)
        {
            return Json<IEnumerable<Hotel>>(GroupHotelSorter.GetHotelsByGroupPreferences(id).Take(20));
        }

        [HttpGet]
        [Route("AddUser")]
        // if groupId empty, creates new group
        public IHttpActionResult AddUserToGroup(int userId, int? groupId = null)
        {
            // trust the data - don't check the if user exists
            // var user = _userRepository.Get(userId);

            Group group;

            if (!groupId.HasValue)
            {
                group = new Group()
                {
                    Id = _groupRepository.GetHighestId() + 1,
                    AdminUserId = userId,
                    UserIds = new[] {userId}
                };

                _groupRepository.Add(group);

            }
            else
            {
                group = _groupRepository.Get(groupId.Value);
                var userIds = new HashSet<int>(group.UserIds) {userId};
                group.UserIds = userIds.ToArray();

                _groupRepository.Update(groupId.Value, group);
            }

            return Ok(group);
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
    }
}
