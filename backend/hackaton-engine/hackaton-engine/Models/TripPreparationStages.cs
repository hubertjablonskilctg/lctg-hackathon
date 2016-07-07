using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace hackaton_engine.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TripPreparationStages
    {
        Unknown = 0,
        GatherPreferences = 1,
        VoteForDestination = 2,
        Final = 3,

        Canceled = -666
    }
}