using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Mongo.Repositories;
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
        public IHttpActionResult GetHotels(int id)
        {
            var group = _groupRepository.Get(id);
            var hotels = _hotelRepository.GetAll();

            var results = new List<Hotel>();
            var hotelsOrder = group.UserHotelUpVotes.SelectMany(x => x.Value).Distinct().ToArray();

            for (int i = 0; i < hotelsOrder.Count(); i++)
            {
                results.Add(hotels.First(x => x.Id == hotelsOrder[i]));
            }            

            return Json<IEnumerable<Hotel>>(results.Take(20));
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
                    UserIds = new HashSet<int>() {userId}
                };

                _groupRepository.Add(group);

            }
            else
            {
                group = _groupRepository.Get(groupId.Value);
                group.UserIds.Add(userId);

                _groupRepository.Update(groupId.Value, group);
            }

            return Ok(group);
        }

        [HttpPost]
        [Route("ChangePreferences")]
        public IHttpActionResult ChangeUserPreferences(Preference preferences)
        {
            //return Ok(DateTime.Now);
            return Ok(preferences);
        }
    }
}
