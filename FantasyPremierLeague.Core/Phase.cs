using Newtonsoft.Json;

namespace FantasyPermierLeague
{
    public class Phase
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("num_winners")]
        public int NumWinners { get; set; }
        [JsonProperty("start_event")]
        public int StartEvent { get; set; }
        [JsonProperty("stop_event")]
        public int StopEvent { get; set; }
    }
}