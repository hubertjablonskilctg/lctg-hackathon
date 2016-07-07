using System;
using Common.Mongo;

namespace hackaton_engine.Models
{
    public class User : Entity
    {
        public User()
        {
            Adults = 1;
        }

        public string Name { get; set; }

		public string Email { get; set; }
        public int Adults { get; set; }
        public int Youths { get; set; }
        public int Infants { get; set; }

        public override string ToString()
        {
            return String.Format("{0} a{1}y{2}i{3}", Name, Adults, Youths, Infants);
        }
    }
}