using Newtonsoft.Json;
using System;

namespace FantasyPremierLeague
{
    public class ElementFixture
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("team_h")]
        public int HomeTeamId { get; set; }

        // Makes no sense in this context but comes through as null
        //[JsonProperty("team_h_score")]
        //public int? HomeTeamScore { get; set; }

        [JsonProperty("team_a")]
        public int AwayTeamId { get; set; }

        // Makes no sense in this context but comes through as null
        //[JsonProperty("team_a_score")]
        //public int? AwayTeamScore { get; set; }

        [JsonProperty("event")]
        public int EventId { get; set; }

        [JsonProperty("finished")]
        public bool IsFinished { get; set; }

        // Makes no sense in this context but comes through as 0
        //[JsonProperty("minutes")]
        //public int Minutes { get; set; }

        [JsonProperty("provisional_start_time")]
        public bool IsProvisionalStartTime { get; set; }

        [JsonProperty("kickoff_time")]
        public DateTime KickOffTime { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("is_home")]
        public bool IsHome { get; set; }

        [JsonProperty("difficulty")]
        public int Difficulty { get; set; }
    }
}
