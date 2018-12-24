using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyPermierLeague
{
    public class StaticResponse
    {
        [JsonProperty("phases")]
        public IEnumerable<Phase> Phases { get; set; }
        [JsonProperty("elements")]
        public IEnumerable<Element> Elements { get; set; }
        [JsonProperty("teams")]
        public IEnumerable<Team> Teams { get; set; }
    }
}
