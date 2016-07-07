using Common.Mongo;

namespace hackaton_engine.Models
{
    public class Hotel : Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public HotelRating Rating { get; set; }
        public string Address { get; set; }
        public byte[] ImageSource { get; set; }
        public string ImageUrl { get; set; }
        public Tags[] Tags { get; set; }
        public Localizations Localization { get; set; }
        public MustHaves[] MustHaves { get; set; }
    }
}