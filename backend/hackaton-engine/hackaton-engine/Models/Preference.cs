using System;

namespace hackaton_engine.Models
{
    /* sample json:
        {
          "Tags": [
            "Cities",
            "Beaches"
          ],
          "Localizations": [
            "Spain",
            "Portugal"
          ],
          "MustHaves": [
            "Disability1",
            "ChildFriendly"
          ],
          "PriceRange": {
            "m_Item1": 50,
            "m_Item2": 100
          },
          "DateRange": {
            "m_Item1": "2016-07-07T12:00:00+02:00",
            "m_Item2": "2016-07-07T12:00:00+02:00"
          }
        }
    */
    public class Preference
    {
        public Tags[] Tags { get; set; }
        public Localizations[] Localizations { get; set; }
        public MustHaves[] MustHaves { get; set; }
        public Tuple<double, double> PriceRange { get; set; }
        public Tuple<DateTime, DateTime> DateRange { get; set; }
    }
}