using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace hackaton_engine.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Localizations
    {
        Spain,
        Portugal,
        France,
        Germany,
        Poland,
        England,
        Italy
    }
}