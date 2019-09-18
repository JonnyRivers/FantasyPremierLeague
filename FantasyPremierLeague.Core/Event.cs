using Newtonsoft.Json;

namespace FantasyPremierLeague
{
    public class Event
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("is_current")]
        public bool IsCurrent { get; set; }

        // TODO: complete this
    }
}
