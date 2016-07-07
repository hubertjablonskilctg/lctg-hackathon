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

            for (int i = 1; i <= 100; ++i)
            {
                var hotel = new Hotel();
                hotel.Id = i;
                hotel.Price = 100 + random.Next(900);
                hotel.Name = string.Format("hotel{0}", i);
                hotel.Address = "";
                hotel.Localization = (Localizations)values.GetValue(random.Next(values.Length));
                hotel.Address = String.Format("{0} Street, {1}", i, hotel.Localization.ToString());
                hotel.Tags = new[] { (Tags)(i % 6), (Tags)((i + 1) % 6), (Tags)((i + 4) % 6), (Tags)((i + 7) % 6) };
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

            Random random = new Random();

            for (int i = 1; i <= 10; ++i)
            {
                var group = new Group();
                group.Id = i;
                group.UserIds = new[] { i, i + 1, i + 2 };

                group.UserHotelUpVotes = new Dictionary<int, int[]>();
                group.UserHotelUpVotes.Add(i, new[] { i, i + 1 });
                group.UserHotelUpVotes.Add(i + 1, new[] { i, i + 1, i + 2 });
                //group.UserHotelUpVotes.Add(i + 2, new[] { i, i + 3 });

                group.UserPreferences = new Dictionary<int, Preference>();
                group.UserPreferences.Add(i, new Preference()
                {
                    DateRange = new Tuple<DateTime, DateTime>(DateTime.MinValue, DateTime.MaxValue),
                    PriceRange = new Tuple<double, double>(random.Next(100), 300 + random.Next(1700)),
                    Localizations = new[] { (Localizations)(i % 7), (Localizations)((i + 1) % 7), (Localizations)((i + 2) % 7), (Localizations)((i + 6) % 7) },
                    Tags = new[] { (Tags)(i % 6), (Tags)((i + 1) % 6), (Tags)((i + 2) % 6) },
                    MustHaves = new MustHaves[0]
                });
                group.UserPreferences.Add(i + 1, new Preference()
                {
                    DateRange = new Tuple<DateTime, DateTime>(DateTime.MinValue, DateTime.MaxValue),
                    PriceRange = new Tuple<double, double>(random.Next(100), 700 + random.Next(500)),
                    Localizations = new[] { (Localizations)(i % 7), (Localizations)((i + 2) % 7), (Localizations)((i + 3) % 7) },
                    Tags = new[] { (Tags)(i % 6), (Tags)((i + 2) % 6), (Tags)((i + 4) % 6) },
                    MustHaves = new[] { (MustHaves)random.Next(2) }
                });
                //group.UserPreferences.Add(i + 2, new Preference()
                //{
                //    DateRange = new Tuple<DateTime, DateTime>(DateTime.MinValue, DateTime.MaxValue),
                //    PriceRange = new Tuple<double, double>(200, 200 + random.Next(2000)),
                //    Localizations = new[] { (Localizations)random.Next(7), (Localizations)random.Next(7) },
                //    Tags = new[] { (Tags)random.Next(6), (Tags)random.Next(6) },
                //    MustHaves = new[] { (MustHaves)random.Next(2) }
                //});

                _groupRepository.Add(group);
            }
        }
    }
}
