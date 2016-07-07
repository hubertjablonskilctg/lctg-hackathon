﻿using System;
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

        public GroupController(IMongoRepository<Group> groupRepository)
        {
            _groupRepository = groupRepository;
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

        [HttpPost]
        [Route("AddUsers/{groupId:int?}")]
        // if groupId empty, creates new group
        public IHttpActionResult AddUsersToGroup([FromBody] int[] newUserIds, [FromUri] int? groupId = null)
        {
            // trust the data - don't check the if user exists
            // var user = _userRepository.Get(userId);

            Group group;

            if (!groupId.HasValue)
            {
                group = new Group()
                {
                    Id = _groupRepository.GetHighestId() + 1,
                    AdminUserId = newUserIds.First(),
                    UserIds = newUserIds
                };

                _groupRepository.Add(group);

            }
            else
            {
                group = _groupRepository.Get(groupId.Value);
                group.UserIds = group.UserIds.Concat(newUserIds).ToArray();

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
