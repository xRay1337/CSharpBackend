using Newtonsoft.Json;

namespace Json
{
    class Country
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("population")]
        public int Population { get; set; }

        [JsonProperty("currencies")]
        public Currency[] Currencies { get; set; }
    }
}