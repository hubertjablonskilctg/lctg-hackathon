using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Common.Mongo.Repositories;
using hackaton_engine.Helpers;
using hackaton_engine.Models;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace hackaton_engine.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IMongoRepository<Group> _groupRepository;
        private readonly IMongoRepository<User> _userRepository;

        public UserController(IMongoRepository<Group> groupRepository, IMongoRepository<User> userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        //[Route("{userEmail}")]
        public IHttpActionResult Get([FromBody] string userEmail)
        {
            var user = _userRepository.Find(u => u.Email.Equals(userEmail)).FirstOrDefault();
            if (user == null)
            {
                throw new KeyNotFoundException("User with email " + userEmail + " was not found");
            }

            var groups = _groupRepository.Find(g => g.AdminUserId == user.Id || g.UserIds.Contains(user.Id)).ToArray();

            var result = new
            {
                User = user,
                Groups = groups
            };

            return Ok(result);
        }
    }
}