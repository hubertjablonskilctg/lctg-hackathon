using Common;
using Common.Mongo.Repositories;
using hackaton_engine.Models;
using System;
using Ninject;
using System.Web.Http;

namespace hackaton_engine.Controllers
{
    public class DbSeedController : ApiController
    {
        IMongoRepository<Hotel> _hotelRepository;

        public DbSeedController()
        {
            this._hotelRepository = IocConfig.GetKernel().Get<IMongoRepository<Hotel>>();
        }

        public IHttpActionResult Get()
        {
            GenerateHotels();

            return Ok();
        }

        private void GenerateHotels()
        {
            Array values = Enum.GetValues(typeof(Localizations));
            Random random = new Random();

            _hotelRepository.Remove(x => true);

            for (int i = 1; i <= 22; ++i)
            {
                var hotel = new Hotel();
                hotel.Id = i;
                hotel.Price = 100 + i * random.Next(900);
                hotel.Name = string.Format("hotel{0}", i);
                hotel.Address = "";
                hotel.Localization = (Localizations)values.GetValue(random.Next(values.Length));
                hotel.Address = String.Format("{0} Street, {1}", i, hotel.Localization.ToString());
                hotel.Tags = new[] { (Tags)(random.Next(5)), (Tags)(random.Next(5)) };
                hotel.MustHaves = new[] { (MustHaves)(i % 2) };
                hotel.ImageUrl = hotel.Name;

                _hotelRepository.Add(hotel);
            }
        }
    }
}
