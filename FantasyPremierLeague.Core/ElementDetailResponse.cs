using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyPremierLeague
{
    public class ElementDetailResponse
    {
        [JsonProperty("history")]
        public IEnumerable<ElementHistory> History { get; set; }
    }
}