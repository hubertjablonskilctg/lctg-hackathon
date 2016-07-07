using System;

namespace hackaton_engine.Models
{
    public class Preference
    {
        public Tags[] Tags { get; set; }
        public Localizations[] Localizations { get; set; }
        public MustHaves[] MustHaves { get; set; }
        public Tuple<double, double> PriceRange { get; set; }
        public Tuple<DateTime, DateTime> DateRange { get; set; }
    }
}