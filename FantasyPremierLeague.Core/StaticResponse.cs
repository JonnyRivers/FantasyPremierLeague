using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FantasyPremierLeague
{
    public class StaticResponse
    {
        [JsonProperty("phases")]
        public IEnumerable<Phase> Phases { get; set; }
        [JsonProperty("elements")]
        public IEnumerable<Element> Elements { get; set; }
        [JsonProperty("teams")]
        public IEnumerable<Team> Teams { get; set; }
        [JsonProperty("events")]
        public IEnumerable<Event> Events { get; set; }

        public int CurrentEvent
        {
            get
            {
                Event currentEvent = Events.FirstOrDefault(e => e.IsCurrent);
                if (currentEvent != null)
                    return currentEvent.Id;

                return 0;// TODO: is this safe?
            }
        }
    }
}
