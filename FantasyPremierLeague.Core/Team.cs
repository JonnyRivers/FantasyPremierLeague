using Newtonsoft.Json;

namespace FantasyPremierLeague
{
    public class Team
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("strength")]
        public int Strength { get; set; }
    }
}