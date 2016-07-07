using Common;
using Common.Mongo.Repositories;
using hackaton_engine.Models;
using System;
using Ninject;
using System.Web.Http;
using System.Collections.Generic;

namespace hackaton_engine.Controllers
{
    public class DbSeedController : ApiController
    {
        IMongoRepository<Hotel> _hotelRepository;
        IMongoRepository<Group> _groupRepository;
        IMongoRepository<User> _userRepository;

        public DbSeedController(
            IMongoRepository<Hotel> hotelRepository,
            IMongoRepository<Group> groupRepository,
            IMongoRepository<User> userRepository)
        {
            this._hotelRepository = hotelRepository;
            this._groupRepository = groupRepository;
            this._userRepository = userRepository;
        }

        public IHttpActionResult Get()
        {
            GenerateHotels();
            GenerateUsers();
            GenerateGroups();

            return Ok();
        }

        private void GenerateHotels()
        {
            Array values = Enum.GetValues(typeof(Localizations));
            Random random = new Random();

            _hotelRepository.Remove(x => true);

            for (int i = 1; i <= 22; ++i)
            {
                var hotel = new Hotel();
                hotel.Id = i;
                hotel.Price = 100 + i * random.Next(900);
                hotel.Name = string.Format("hotel{0}", i);
                hotel.Address = "";
                hotel.Localization = (Localizations)values.GetValue(random.Next(values.Length));
                hotel.Address = String.Format("{0} Street, {1}", i, hotel.Localization.ToString());
                hotel.Tags = new[] { (Tags)(random.Next(5)), (Tags)(random.Next(5)) };
                hotel.MustHaves = new[] { (MustHaves)(i % 2) };
                hotel.ImageUrl = hotel.Name;

                _hotelRepository.Add(hotel);
            }
        }


        private void GenerateUsers()
        {
            _userRepository.Remove(x => true);
            for (int i = 0; i < 30; i++)
            {
                var user = new User();
                user.Id = i;
                user.Name = String.Format("user_{0}", i);
                _userRepository.Add(user);
            }
        }

        private void GenerateGroups()
        {
            _groupRepository.Remove(x => true);

            for (int i = 1; i <= 10; ++i)
            {
                var group = new Group();
                group.Id = i;
                group.UserIds = new[] { i, i + 1, i + 2 };

                group.UserHotelUpVotes = new Dictionary<int, int[]>();
                group.UserHotelUpVotes.Add(i, new[] { i, i + 1 });
                group.UserHotelUpVotes.Add(i + 1, new[] { i, i + 1, i + 2 });
                group.UserHotelUpVotes.Add(i + 2, new[] { i, i + 3 });

                group.UserPreferences = new Dictionary<int, Preference>();
                group.UserPreferences.Add(i, new Preference());
                group.UserPreferences.Add(i + 1, new Preference());
                group.UserPreferences.Add(i + 2, new Preference());

                _groupRepository.Add(group);
            }
        }
    }
}
