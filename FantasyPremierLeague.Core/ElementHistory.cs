using Newtonsoft.Json;

namespace FantasyPermierLeague
{
    public class ElementHistory
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("team_h_score")]
        public int HomeTeamScore { get; set; }
        [JsonProperty("team_a_score")]
        public int AwayTeamScore { get; set; }
        [JsonProperty("was_home")]
        public bool WasHome { get; set; }
        [JsonProperty("round")]
        public int Round { get; set; }
        [JsonProperty("total_points")]
        public int TotalPoints { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
        /*
         * id: 1,
kickoff_time: "2018-08-12T15:00:00Z",
kickoff_time_formatted: "12 Aug 16:00",
team_h_score: 0,
team_a_score: 2,
was_home: true,
round: 1,
total_points: 3,
value: 50,
transfers_balance: 0,
selected: 70767,
transfers_in: 0,
transfers_out: 0,
loaned_in: 0,
loaned_out: 0,
minutes: 90,
goals_scored: 0,
assists: 0,
clean_sheets: 0,
goals_conceded: 2,
own_goals: 0,
penalties_saved: 0,
penalties_missed: 0,
yellow_cards: 0,
red_cards: 0,
saves: 6,
bonus: 0,
bps: 24,
influence: "47.0",
creativity: "0.0",
threat: "0.0",
ict_index: "4.7",
ea_index: 0,
open_play_crosses: 0,
big_chances_created: 0,
clearances_blocks_interceptions: 2,
recoveries: 9,
key_passes: 0,
tackles: 0,
winning_goals: 0,
attempted_passes: 42,
completed_passes: 31,
penalties_conceded: 0,
big_chances_missed: 0,
errors_leading_to_goal: 0,
errors_leading_to_goal_attempt: 0,
tackled: 0,
offside: 0,
target_missed: 0,
fouls: 0,
dribbles: 0,
element: 1,
fixture: 1,
opponent_team: 13
         */
    }
}