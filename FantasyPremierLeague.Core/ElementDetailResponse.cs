using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyPermierLeague
{
    public class ElementDetailResponse
    {
        [JsonProperty("history")]
        public IEnumerable<ElementHistory> History { get; set; }
    }
}