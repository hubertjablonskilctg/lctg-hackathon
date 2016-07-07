using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace hackaton_engine.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MustHaves
    {
        WheelchairAccessible,
        ChildFriendly,
        Vegetarian
    }
}