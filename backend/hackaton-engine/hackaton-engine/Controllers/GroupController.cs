using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Mongo.Repositories;
using hackaton_engine.Models;
using MongoDB.Driver;

namespace hackaton_engine.Controllers
{
    public class GroupController : ApiController
    {
        private readonly IMongoRepository<Group> _groupRepository;

        public GroupController(IMongoRepository<Group> groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public string Get()
        {
            return "wololo";
        }

        [HttpPost]
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
    }
}
