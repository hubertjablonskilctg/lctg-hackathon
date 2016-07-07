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
        public IHttpActionResult AddUser(int groupId, int userId)
        {
            // don't check the user availability now
            // var user = _userRepository.Get(userId);

            var group = _groupRepository.Get(groupId);
            group.UserIds.Add(userId);

            _groupRepository.Update(groupId, group);

            return Ok(group);
        }
    }
}
