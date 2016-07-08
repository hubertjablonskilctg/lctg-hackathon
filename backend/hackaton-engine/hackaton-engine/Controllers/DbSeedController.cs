using Common;
using Common.Mongo.Repositories;
using hackaton_engine.Models;
using System;
using Ninject;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;

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
            _hotelRepository.Remove((x) => true);

            _hotelRepository.Add(new Hotel()
            {
                Id = 1,
                Name = "El Palace Hotel",
                Price = 421,
                Address = "Spain, Barcelona",
                Localization = Localizations.Spain,
                Tags = new[] { Tags.Beaches, Tags.Nightlife, Tags.Nature },
                MustHaves = new[] { MustHaves.ChildFriendly, MustHaves.WheelchairAccessible },
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
                MustHaves = new[] { MustHaves.ChildFriendly, MustHaves.WheelchairAccessible },
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
                MustHaves = new[] { MustHaves.WheelchairAccessible },
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
                MustHaves = new[] { MustHaves.WheelchairAccessible },
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
                MustHaves = new MustHaves[] { MustHaves.ChildFriendly, MustHaves.WheelchairAccessible },
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
                MustHaves = new[] { MustHaves.WheelchairAccessible },
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
                MustHaves = new[] { MustHaves.WheelchairAccessible },
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
                MustHaves = new[] { MustHaves.WheelchairAccessible },
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
                MustHaves = new MustHaves[] { },
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
                MustHaves = new MustHaves[] { },
                Rating = HotelRating.Star4,
                ImageUrl = ""
            });
        }


        private void GenerateUsers()
        {
            _userRepository.Remove(x => true);

            _userRepository.Add(new User()
            {
                Id = 1,
                Name = "Mikolaj Kopernik",
                Email = "mikolaj.kopernik@moon.pl",
                Adults = 1,
            });

            _userRepository.Add(new User()
            {
                Id = 2,
                Name = "John Smith",
                Email = "John.Smith@matrix.com",
                Adults = 1,
            });

            _userRepository.Add(new User()
            {
                Id = 3,
                Name = "Hanz Muller",
                Email = "muller66@mail.de",
                Adults = 1,
            });

            _userRepository.Add(new User()
            {
                Id = 4,
                Name = "Joanna Kowalska",
                Email = "joko@wp.pl",
                Adults = 1,
            });
        }

        private void GenerateGroups()
        {
            _groupRepository.Remove(x => true);

            _groupRepository.Add(new Group()
            {
                Id = 1,
                AdminUserId = 1,
                CurrentTripPreparationStage = TripPreparationStages.GatherPreferences,
                Name = "TestowaWycieczka",
                UserIds = new[] { 1, 2 },
                Users = _userRepository.Find((u) => new[] { 1, 2 }.Contains(u.Id)),
                UserHotelUpVotes = new Dictionary<int, int[]>(),
                UserPreferences = new Dictionary<int, Preference>()
                {
                    { 1,new Preference()
                    {
                        Tags = new [] {Tags.Beaches, Tags.Cities},
                        MustHaves = new MustHaves[] {},
                        Localizations = new Localizations[] {Localizations.Poland, Localizations.France, },
                        PriceRange = new Tuple<double,double>(100,500),
                        DateRange = new Tuple<DateTime, DateTime>(new DateTime(2016,7,1), new DateTime(2016,7,30))
                    } },
                    { 2,new Preference()
                    {
                        Tags = new [] {Tags.Beaches, Tags.Nature},
                        MustHaves = new MustHaves[] {},
                        Localizations = new Localizations[] {Localizations.Germany, Localizations.France, },
                        PriceRange = new Tuple<double,double>(100,500),
                        DateRange = new Tuple<DateTime, DateTime>(new DateTime(2016,7,15), new DateTime(2016,8,15))
                    } }
                }
            });

            _groupRepository.Add(new Group()
            {
                Id = 2,
                AdminUserId = 2,
                CurrentTripPreparationStage = TripPreparationStages.GatherPreferences,
                Name = "Woah trip",
                UserIds = new[] { 2, 3, 4 },
                Users = _userRepository.Find((u) => new[] { 1, 2 }.Contains(u.Id)),
                UserHotelUpVotes = new Dictionary<int, int[]>(),
                UserPreferences = new Dictionary<int, Preference>()
                {
                    { 2,new Preference()
                    {
                        Tags = new [] {Tags.Beaches, Tags.Cities},
                        MustHaves = new MustHaves[] {},
                        Localizations = new Localizations[] {Localizations.Poland, Localizations.Italy, },
                        PriceRange = new Tuple<double,double>(400,800),
                        DateRange = new Tuple<DateTime, DateTime>(new DateTime(2016,8,10), new DateTime(2016,9,10))
                    } },
                    { 3,new Preference()
                    {
                        Tags = new [] {Tags.Cities, Tags.Nature},
                        MustHaves = new MustHaves[] {MustHaves.ChildFriendly, },
                        Localizations = new Localizations[] {Localizations.Poland, Localizations.France, },
                        PriceRange = new Tuple<double,double>(400,800),
                        DateRange = new Tuple<DateTime, DateTime>(new DateTime(2016,8,20), new DateTime(2016,9,1))
                    } }
                }
            });

            _groupRepository.Add(new Group()
            {
                Id = 3,
                AdminUserId = 3,
                CurrentTripPreparationStage = TripPreparationStages.VoteForDestination,
                Name = "VOTEVOTEVOTE!11",
                UserIds = new[] { 3, 4 },
                Users = _userRepository.Find((u) => new[] { 1, 2 }.Contains(u.Id)),
                UserHotelUpVotes = new Dictionary<int, int[]>()
                {
                    {3,new[] { 5} }, {4,new [] {9} }
                },
                UserPreferences = new Dictionary<int, Preference>()
                {
                    { 3,new Preference()
                    {
                        Tags = new [] {Tags.Beaches, Tags.Cities},
                        MustHaves = new MustHaves[] {},
                        Localizations = new Localizations[] {Localizations.United_Kingdom, Localizations.Italy, },
                        PriceRange = new Tuple<double,double>(600,800),
                        DateRange = new Tuple<DateTime, DateTime>(new DateTime(2016,7,1), new DateTime(2016,9,1))
                    } },
                    { 4,new Preference()
                    {
                        Tags = new [] {Tags.Cities, Tags.Nature},
                        MustHaves = new MustHaves[] {MustHaves.ChildFriendly, },
                        Localizations = new Localizations[] {Localizations.United_Kingdom, Localizations.France, },
                        PriceRange = new Tuple<double,double>(600,800),
                        DateRange = new Tuple<DateTime, DateTime>(new DateTime(2016,8,1), new DateTime(2016,9,1))
                    } }
                }
            });

            _groupRepository.Add(new Group()
            {
                Id = 4,
                AdminUserId = 1,
                CurrentTripPreparationStage = TripPreparationStages.VoteForDestination,
                Name = "EmptyPref",
                UserIds = new[] { 4 },
                Users = _userRepository.Find((u) => new[] { 4}.Contains(u.Id)),
                UserHotelUpVotes = new Dictionary<int, int[]>(),
                UserPreferences = new Dictionary<int, Preference>()
            });
        }
    }
}
