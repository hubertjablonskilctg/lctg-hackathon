using Common.Mongo;

namespace hackaton_engine.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}