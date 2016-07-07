using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace hackaton_engine.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MustHaves
    {
        Disability1,
        ChildFriendly
    }
}