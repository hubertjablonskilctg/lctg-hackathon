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
            "WheelchairAccessible",
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
        private Tags[] _tags = new Tags[0];
        private Localizations[] _localizations = new Localizations[0];
        private MustHaves[] _mustHaves = new MustHaves[0];

        public Preference()
        {
            Tags = new Tags[0];
            Localizations = new Localizations[0];
            MustHaves = new MustHaves[0];

            PriceRange = new Tuple<double, double>(0,0);
            DateRange = new Tuple<DateTime, DateTime>(DateTime.Now, DateTime.Now);
        }

        public Tags[] Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        public Localizations[] Localizations
        {
            get { return _localizations; }
            set { _localizations = value; }
        }

        public MustHaves[] MustHaves
        {
            get { return _mustHaves; }
            set { _mustHaves = value; }
        }

        public Tuple<double, double> PriceRange { get; set; }
        public Tuple<DateTime, DateTime> DateRange { get; set; }
    }
}