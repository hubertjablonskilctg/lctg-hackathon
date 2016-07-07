using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace hackaton_engine.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Tags
    {
        Cities,
        Beaches,
        Nature,
        Skiing,
        Shopping,
        Nightlife
    }
}