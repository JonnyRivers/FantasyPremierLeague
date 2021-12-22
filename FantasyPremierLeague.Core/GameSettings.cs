using Newtonsoft.Json;
using System.Collections.Generic;

namespace FantasyPremierLeague
{
    public class GameSettings
    {
        [JsonProperty("league_join_private_max")]
        public int LeagueJoinPrivateMax { get; set; }

        [JsonProperty("league_join_public_max")]
        public int LeagueJoinPublicMax { get; set; }

        [JsonProperty("league_max_size_public_classic")]
        public int LeagueMaxSizePublicClassic { get; set; }

        [JsonProperty("league_max_size_public_h2h")]
        public int LeagueMaxSizePublicHeadToHead { get; set; }

        [JsonProperty("league_max_size_private_h2h")]
        public int LeagueMaxSizePrivateHeadToHead { get; set; }

        [JsonProperty("league_max_ko_rounds_private_h2h")]
        public int LeagueMaxKORoundsPrivateHeadToHaead { get; set; }

        [JsonProperty("league_prefix_public")]
        public string LeaguePrefixPublic { get; set; }

        [JsonProperty("league_points_h2h_win")]
        public int LeaguePointsHeadToHeadWin { get; set; }

        [JsonProperty("league_points_h2h_lose")]
        public int LeaguePointsHeadToHeadLose { get; set; }

        [JsonProperty("league_points_h2h_draw")]
        public int LeaguePointsHeadToHeadDraw { get; set; }

        [JsonProperty("league_ko_first_instead_of_random")]
        public bool LeagueKOFirstInsteadOfRandom { get; set; }

        [JsonProperty("cup_start_event_id")]
        public int? CupStartEventId { get; set; }

        [JsonProperty("cup_stop_event_id")]
        public int? CupStopEventId { get; set; }

        [JsonProperty("cup_qualifying_method")]
        public string CupQualifyingMethod { get; set; }

        [JsonProperty("cup_type")]
        public string CupType { get; set; }

        [JsonProperty("squad_squadplay")]
        public int SquadSquadPlay { get; set; }

        [JsonProperty("squad_squadsize")]
        public int SquadSquadSize { get; set; }

        [JsonProperty("squad_team_limit")]
        public int SquadTeamLimit { get; set; }

        [JsonProperty("squad_total_spend")]
        public int SquadTotalSpend { get; set; }

        [JsonProperty("ui_currency_multiplier")]
        public int UICurrencyMultiplier { get; set; }

        [JsonProperty("ui_use_special_shirts")]
        public bool UIUseSpecialShirts { get; set; }

        // TODO - determine the type of this array
        //[JsonProperty("ui_special_shirt_exclusions")]
        //public IEnumerable<string> UISpecialShirtExclusions { get; set; }

        [JsonProperty("stats_form_days")]
        public int SysViceCaptainEnabled { get; set; }

        [JsonProperty("transfers_cap")]
        public int TransfersCap { get; set; }

        [JsonProperty("transfers_sell_on_fee")]
        public double TransfersSellOnFee { get; set; }

        [JsonProperty("league_h2h_tiebreak_stats")]
        public IEnumerable<string> LeagueHeadToHeadTiebreakStats { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }
    }
}
