using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyPremierLeague
{
    public class TeamResponse
    {
        [JsonProperty("picks")]
        public IEnumerable<Pick> Picks { get; set; }
        [JsonProperty("ce")]
        public int CurrentEvent { get; set; }
    }
}