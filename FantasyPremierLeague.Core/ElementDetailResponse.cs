using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyPremierLeague
{
    // TODO: ElementSummaryResponse?
    public class ElementDetailResponse
    {
        //[JsonProperty("fixtures")]
        //public IEnumerable<ElementFixture> Fixtures { get; set; }

        [JsonProperty("history")]
        public IEnumerable<ElementHistory> History { get; set; }

        //[JsonProperty("history_past")]
        //public IEnumerable<ElementHistoryPast> HistoryPast { get; set; }
    }
}