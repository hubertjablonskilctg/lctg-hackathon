﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common;
using Common.Mongo.Repositories;
using hackaton_engine.Models;
using Ninject;

namespace hackaton_engine.Helpers
{
    public static class GroupHotelSorter
    {
        public static IEnumerable<Hotel> GetHotelsByGroupPreferences(int groupId)
        {
            var group = IocConfig.GetKernel().Get<MongoRepository<Group>>().Get(groupId);
            if (group == null)
            {
                //throw new ArgumentNullException("Group " + groupId + " not found!");
                return IocConfig.GetKernel().Get<MongoRepository<Hotel>>().GetAll();
            }
            List<Preference> preferences = group.UserPreferences.Values.ToList();
            if (preferences.Count < 1)
            {
                //throw new Exception("Group " + groupId + " has no preferences");
                return IocConfig.GetKernel().Get<MongoRepository<Hotel>>().GetAll();
            }

            Preference joinedPreference = JoinPreferences(preferences);
            var filteredHotels = IocConfig.GetKernel().Get<MongoRepository<Hotel>>()
                .Find(GetDbFilterExpression(joinedPreference))
                .ToArray();

            List<Hotel> preferenceMatchedHotels = new List<Hotel>();
            foreach (var preference in preferences)
            {
                preferenceMatchedHotels.AddRange(
                    filteredHotels.Where(h => GetFilterExpression(preference)(h)));
            }

            foreach (var hotelToBeScored in filteredHotels)
            {
                hotelToBeScored.SearchScore = preferenceMatchedHotels.Count(pmh => pmh.Id == hotelToBeScored.Id) +
                    //if have same preference hits take Rating into account (not bigger than 1.0)
                    (double)hotelToBeScored.Rating / ((double)Enum.GetValues(typeof(HotelRating)).Cast<HotelRating>().Max() + 1.0);
            }

            return filteredHotels.OrderByDescending(h => h.SearchScore);
        }

        private static Expression<Func<Hotel, bool>> GetDbFilterExpression(Preference joinedPreference)
        {
            return h => (joinedPreference.Localizations.Length == 0 || joinedPreference.Localizations.Contains(h.Localization)) &&
                        (joinedPreference.MustHaves.Length == 0 || joinedPreference.MustHaves.All(mh => h.MustHaves.Contains(mh))) &&
                        joinedPreference.PriceRange.Item1 <= h.Price &&
                        joinedPreference.PriceRange.Item2 >= h.Price &&
                        (joinedPreference.Tags.Length == 0 || joinedPreference.Tags.Any(t => h.Tags.Contains(t)));
        }
        private static Func<Hotel, bool> GetFilterExpression(Preference joinedPreference)
        {
            return h => (joinedPreference.Localizations.Length == 0 || joinedPreference.Localizations.Contains(h.Localization)) &&
                        (joinedPreference.MustHaves.Length == 0 || joinedPreference.MustHaves.All(mh => h.MustHaves.Contains(mh))) &&
                        joinedPreference.PriceRange.Item1 <= h.Price &&
                        joinedPreference.PriceRange.Item2 >= h.Price &&
                        (joinedPreference.Tags.Length == 0 || joinedPreference.Tags.All(t => h.Tags.Contains(t)));
        }

        private static Preference JoinPreferences(List<Preference> preferences)
        {
            Preference joinedPreference = preferences.First();
            foreach (var preference in preferences)
            {
                joinedPreference = JoinPreferences(joinedPreference, preference);
            }
            return joinedPreference;
        }

        private static Preference JoinPreferences(Preference preference1, Preference preference2)
        {
            return new Preference()
            {
                DateRange = new Tuple<DateTime, DateTime>(
                    preference1.DateRange.Item1 < preference2.DateRange.Item1 ? preference1.DateRange.Item1 : preference2.DateRange.Item1,
                    preference1.DateRange.Item2 > preference2.DateRange.Item2 ? preference1.DateRange.Item2 : preference2.DateRange.Item2),
                Localizations = preference1.Localizations.Concat(preference2.Localizations).Distinct().ToArray(),
                MustHaves = preference1.MustHaves.Concat(preference2.MustHaves).Distinct().ToArray(),
                PriceRange = new Tuple<double, double>(
                    Math.Max(preference1.PriceRange.Item1, preference2.PriceRange.Item1),
                    Math.Min(preference1.PriceRange.Item2, preference2.PriceRange.Item2)),
                Tags = preference1.Tags.Concat(preference2.Tags).Distinct().ToArray()
            };
        }
    }
}