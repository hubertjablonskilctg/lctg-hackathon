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
            //GenerateUsers();
            //GenerateGroups();

            return Ok();
        }

        private void GenerateHotels()
        {
            _hotelRepository.Add(new Hotel()
            {
                Id = 1,
                Name = "El Palace Hotel",
                Price = 421,
                Address = "Spain, Barcelona",
                Localization = Localizations.Spain,
                Tags = new[] { Tags.Beaches, Tags.Nightlife, Tags.Nature },
                MustHaves = new[] { MustHaves.ChildFriendly, MustHaves.Disability1 },
                Rating = HotelRating.Star4,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 2,
                Name = "Regent Berlin",
                Price = 224,
                Address = "Germany, Berlin",
                Localization = Localizations.Germany,
                Tags = new[] { Tags.Cities, Tags.Nightlife, Tags.Shopping },
                MustHaves = new[] { MustHaves.ChildFriendly, MustHaves.Disability1 },
                Rating = HotelRating.Star4,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 4,
                Name = "Maison Souquet",
                Price = 298,
                Address = "France, Paris",
                Localization = Localizations.France,
                Tags = new[] { Tags.Cities, Tags.Nightlife, Tags.Shopping },
                MustHaves = new[] { MustHaves.Disability1 },
                Rating = HotelRating.Star5,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 5,
                Name = "Portrait Firenze",
                Price = 699,
                Address = "Italy, Florence",
                Localization = Localizations.Italy,
                Tags = new[] { Tags.Cities, Tags.Nightlife },
                MustHaves = new[] { MustHaves.ChildFriendly },
                Rating = HotelRating.Star5,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 6,
                Name = "Hotel Rialto",
                Price = 135,
                Address = "Poland, Warsaw",
                Localization = Localizations.Poland,
                Tags = new[] { Tags.Shopping, Tags.Nightlife },
                MustHaves = new[] { MustHaves.Disability1 },
                Rating = HotelRating.Star3,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 7,
                Name = "Quinta Jardins do Lago",
                Price = 155,
                Address = "Portugal, Funchal",
                Localization = Localizations.Portugal,
                Tags = new[] { Tags.Nature, Tags.Nightlife },
                MustHaves = new MustHaves[] { },
                Rating = HotelRating.Star5,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 9,
                Name = "Taj 51 Buckingham Gate Suites and Residences",
                Price = 700,
                Address = "UK, London",
                Localization = Localizations.United_Kingdom,
                Tags = new[] { Tags.Cities, Tags.Nightlife, Tags.Shopping },
                MustHaves = new MustHaves[] { MustHaves.ChildFriendly, MustHaves.Disability1 },
                Rating = HotelRating.Star5,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 10,
                Name = "Corral del Rey",
                Price = 150,
                Address = "Spain, Seville",
                Localization = Localizations.Spain,
                Tags = new[] { Tags.Beaches, Tags.Nightlife },
                MustHaves = new[] { MustHaves.Disability1 },
                Rating = HotelRating.Star3,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 11,
                Name = "Ibis Berlin Kurfurstendamm",
                Price = 77,
                Address = "Germany, Berlin",
                Localization = Localizations.Germany,
                Tags = new[] { Tags.Cities },
                MustHaves = new[] { MustHaves.Disability1 },
                Rating = HotelRating.Star3,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 12,
                Name = "Citotel de France",
                Price = 69,
                Address = "France, Rochefort",
                Localization = Localizations.France,
                Tags = new[] { Tags.Nightlife },
                MustHaves = new MustHaves[] { },
                Rating = HotelRating.Star2,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 13,
                Name = "Residence Aramis Milan Downtown",
                Price = 469,
                Address = "Italy, Milan",
                Localization = Localizations.Italy,
                Tags = new[] { Tags.Nightlife, Tags.Cities },
                MustHaves = new MustHaves[] { },
                Rating = HotelRating.Star3,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 14,
                Name = "Ibis Poznan Centrum",
                Price = 39,
                Address = "Poland, Poznan",
                Localization = Localizations.Poland,
                Tags = new[] { Tags.Shopping },
                MustHaves = new[] { MustHaves.ChildFriendly },
                Rating = HotelRating.Star1,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 15,
                Name = "Hotel Laranjeira",
                Price = 59,
                Address = "Portugal, Viana do Castelo",
                Localization = Localizations.Portugal,
                Tags = new[] { Tags.Beaches, Tags.Nightlife },
                MustHaves = new[] {MustHaves.Disability1 },
                Rating = HotelRating.Star2,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 16,
                Name = "Summerhill Hotel",
                Price = 110,
                Address = "UK, Paignton",
                Localization = Localizations.United_Kingdom,
                Tags = new[] { Tags.Cities },
                MustHaves = new MustHaves[] {},
                Rating = HotelRating.Star1,
                ImageUrl = ""
            });

            _hotelRepository.Add(new Hotel()
            {
                Id = 17,
                Name = "Sanglard Sports",
                Price = 146,
                Address = "France, Chamonix",
                Localization = Localizations.France,
                Tags = new[] { Tags.Skiing },
                MustHaves = new MustHaves[] {},
                Rating = HotelRating.Star4,
                ImageUrl = ""
            });
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
