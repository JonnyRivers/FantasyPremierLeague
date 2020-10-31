using Newtonsoft.Json;
using System;

namespace FantasyPremierLeague
{
    public class ElementHistory
    {
        [JsonProperty("element")]
        public int ElementId { get; set; }

        [JsonProperty("fixture")]
        public int FixtureId { get; set; }

        [JsonProperty("opponent_team")]
        public int OpponentTeamId { get; set; }

        [JsonProperty("total_points")]
        public int TotalPoints { get; set; }

        [JsonProperty("was_home")]
        public bool WasHome { get; set; }

        [JsonProperty("kickoff_time")]
        public DateTime KickOffTime { get; set; }

        [JsonProperty("team_h_score")]
        public int HomeTeamScore { get; set; }

        [JsonProperty("team_a_score")]
        public int AwayTeamScore { get; set; }
        
        [JsonProperty("round")]
        public int Round { get; set; }
        
        [JsonProperty("minutes")]
        public int Minutes { get; set; }

        [JsonProperty("goals_scored")]
        public int GoalsScored { get; set; }

        [JsonProperty("assists")]
        public int Assists { get; set; }

        [JsonProperty("clean_sheets")]
        public int CleanSheets { get; set; }

        [JsonProperty("goals_conceded")]
        public int GoalsConceded { get; set; }

        [JsonProperty("own_goals")]
        public int OwnGoals { get; set; }

        [JsonProperty("penalties_saved")]
        public int PenaltiesSaved { get; set; }

        [JsonProperty("penalties_missed")]
        public int PenaltiesMissed { get; set; }

        [JsonProperty("yellow_cards")]
        public int YellowCards { get; set; }

        [JsonProperty("red_cards")]
        public int RedCards { get; set; }

        [JsonProperty("saves")]
        public int Saves { get; set; }

        [JsonProperty("bonus")]
        public int Bonus { get; set; }

        [JsonProperty("bps")]
        public int Bps { get; set; }

        [JsonProperty("influence")]
        public string Influence { get; set; }

        [JsonProperty("creativity")]
        public string Creativity { get; set; }

        [JsonProperty("threat")]
        public string Threat { get; set; }

        [JsonProperty("ict_index")]
        public string IctIndex { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("transfers_balance")]
        public int TransfersBalance { get; set; }

        [JsonProperty("selected")]
        public int Selected { get; set; }

        [JsonProperty("transfers_in")]
        public int TransfersIn { get; set; }

        [JsonProperty("transfers_out")]
        public int TransfersOut { get; set; }
    }
}