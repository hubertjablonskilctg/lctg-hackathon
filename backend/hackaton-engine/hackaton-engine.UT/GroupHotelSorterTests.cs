using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Mongo.Repositories;
using hackaton_engine.Helpers;
using hackaton_engine.Models;
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

            var groupRepo = IocConfig.GetKernel().Get<MongoRepository<Group>>();
            groupRepo.Remove(g => true);
            groupRepo.Add(new Group()
            {
                Id = 1,
                UserPreferences = new Dictionary<int, Preference>() { { 1, new Preference()
                {
                    Tags = new Tags[] {Tags.Cities},
                    MustHaves = new MustHaves[] {MustHaves.ChildFriendly},
                    Localizations = new Localizations[] {Localizations.Poland},
                    PriceRange = new Tuple<double, double>(0,1000),
                    DateRange = new Tuple<DateTime,DateTime>(DateTime.MinValue, DateTime.MaxValue)
                } } }
            });
        }

        [Test]
        public void SortsByMatchedPreferences()
        {
            SeedHotels();
            var sortedHotels = GroupHotelSorter.GetHotelsByGroupPreferences(1);

            Assert.AreEqual(1, sortedHotels.Count());
        }
    }
}
