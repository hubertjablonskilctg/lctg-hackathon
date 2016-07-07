using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Mongo.Repositories;
using hackaton_engine.Helpers;
using hackaton_engine.Models;
using MongoDB.Driver;
using Ninject;
using NUnit.Framework;

namespace hackaton_engine.UT
{
    [TestFixture]
    public class GroupHotelSorterTests
    {
        private void SeedHotels()
        {
            var hotelRepo = IocConfig.GetKernel().Get<MongoRepository<Hotel>>();
            hotelRepo.Remove(h => true);
            hotelRepo.Add(new Hotel()
            {
                Id = 1,
                Name = "hotel1",
                Localization = Localizations.Poland,
                MustHaves = new[] { MustHaves.ChildFriendly, MustHaves.Disability1, },
                Tags = new[] { Tags.Beaches, Tags.Cities, Tags.Nature, Tags.Nightlife },
                Price = 500,
                Rating = HotelRating.Star5
            });
            hotelRepo.Add(new Hotel()
            {
                Id = 2,
                Name = "hotel2",
                Localization = Localizations.Poland,
                MustHaves = new[] { MustHaves.ChildFriendly },
                Tags = new[] { Tags.Cities },
                Price = 400,
                Rating = HotelRating.Star4
            });
            hotelRepo.Add(new Hotel()
            {
                Id = 3,
                Name = "hotel3",
                Localization = Localizations.Poland,
                MustHaves = new[] { MustHaves.ChildFriendly },
                Tags = new[] { Tags.Beaches },
                Price = 100,
                Rating = HotelRating.Star1
            });
            hotelRepo.Add(new Hotel()
            {
                Id = 4,
                Name = "hotel4",
                Localization = Localizations.Germany,
                MustHaves = new MustHaves[] { },
                Tags = new[] { Tags.Cities },
                Price = 100,
                Rating = HotelRating.Star1
            });

            var groupRepo = IocConfig.GetKernel().Get<MongoRepository<Group>>();
            groupRepo.Remove(g => true);
            groupRepo.Add(new Group()
            {
                Id = 1,
                UserPreferences = new Dictionary<int, Preference>() {
                    { 1, new Preference()
                        {
                            Tags = new Tags[] {Tags.Cities},
                            MustHaves = new MustHaves[] {MustHaves.ChildFriendly},
                            Localizations = new Localizations[] {Localizations.Poland},
                            PriceRange = new Tuple<double, double>(0,1000),
                            DateRange = new Tuple<DateTime,DateTime>(DateTime.MinValue, DateTime.MaxValue)
                        }
                    },
                    { 2, new Preference()
                        {
                            Tags = new Tags[] {Tags.Cities, Tags.Beaches, },
                            MustHaves = new MustHaves[] {},
                            Localizations = new Localizations[] {Localizations.Poland},
                            PriceRange = new Tuple<double, double>(0,1000),
                            DateRange = new Tuple<DateTime,DateTime>(DateTime.MinValue, DateTime.MaxValue)
                        }
                    },
                    { 3, new Preference()
                        {
                            Tags = new Tags[] {},
                            MustHaves = new MustHaves[] {},
                            Localizations = new Localizations[] {},
                            PriceRange = new Tuple<double, double>(0,1000),
                            DateRange = new Tuple<DateTime,DateTime>(DateTime.MinValue, DateTime.MaxValue)
                        }
                    },
                }
            });
        }

        [Test]
        public void SortsByMatchedPreferences()
        {
            IocConfig.GetKernel().Rebind<IMongoClient>()
                .To<MongoClient>().InSingletonScope()
                .WithConstructorArgument(typeof(string), "mongodb://localhost:27017");

            SeedHotels();
            var sortedHotels = GroupHotelSorter.GetHotelsByGroupPreferences(1).ToArray();

            Assert.AreEqual(3, sortedHotels.Count());
            Assert.AreEqual(HotelRating.Star5, sortedHotels.First().Rating);
            Assert.AreEqual("hotel3", sortedHotels[2].Name);
        }
    }
}
