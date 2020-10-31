using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FantasyPremierLeague
{
    public class FixtureStatPair
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("element")]
        public int ElementId { get; set; }
    }

    public class FixtureStat
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("a")]
        public IEnumerable<FixtureStatPair> AwayTeamEntries { get; set; }

        [JsonProperty("h")]
        public IEnumerable<FixtureStatPair> HomeTeamEntries { get; set; }
    }

    public class Fixture
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("event")]
        public int? EventId { get; set; }

        [JsonProperty("finished")]
        public bool IsFinished { get; set; }

        [JsonProperty("finished_provisional")]
        public bool IsFinishedProvisional { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("kickoff_time")]
        public DateTime? KickoffTime { get; set; }

        [JsonProperty("minutes")]
        public int Minutes { get; set; }

        [JsonProperty("provisional_start_time")]
        public bool IsProvisionalStartTime { get; set; }

        [JsonProperty("started")]
        public bool? HasStarted { get; set; }

        [JsonProperty("team_a")]
        public int AwayTeamId { get; set; }

        [JsonProperty("team_a_score")]
        public int? AwayTeamScore { get; set; }

        [JsonProperty("team_h")]
        public int HomeTeamId { get; set; }

        [JsonProperty("team_h_score")]
        public int? HomeTeamScore { get; set; }

        [JsonProperty("stats")]
        public IEnumerable<FixtureStat> Stats { get; set; }

        [JsonProperty("team_h_difficulty")]
        public int HomeTeamDifficulty { get; set; }

        [JsonProperty("team_a_difficulty")]
        public int AwayTeamDfficulty { get; set; }

        [JsonProperty("pulse_id")]
        public int PulseId { get; set; }
    }
}
