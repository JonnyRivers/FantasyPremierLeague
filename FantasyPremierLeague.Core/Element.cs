﻿
using Newtonsoft.Json;
using System;

namespace FantasyPremierLeague
{
    public class Element
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("photo")]
        public string Photo { get; set; }
        [JsonProperty("web_name")]
        public string WebName { get; set; }
        [JsonProperty("team_code")]
        public int TeamCode { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("second_name")]
        public string SecondName { get; set; }
        [JsonProperty("squad_number")]
        public int? SquadNumber { get; set; }
        [JsonProperty("news")]
        public string News { get; set; }
        [JsonProperty("now_cost")]
        public int NowCost { get; set; }
        [JsonProperty("news_added")]
        public DateTime? NewsAdded { get; set; }
        [JsonProperty("chance_of_playing_this_round")]
        public int? ChanceOfPlayingThisRound { get; set; }
        [JsonProperty("chance_of_playing_next_round")]
        public int? ChanceOfPlayingNextRound { get; set; }
        [JsonProperty("value_form")]
        public string ValueForm { get; set; }
        [JsonProperty("value_season")]
        public string ValueSeason { get; set; }
        [JsonProperty("cost_change_start")]
        public int CostChangeStart { get; set; }
        [JsonProperty("cost_change_event")]
        public int CostChangeEvent { get; set; }
        [JsonProperty("cost_change_start_fall")]
        public int CostChangeStartFall { get; set; }
        [JsonProperty("cost_change_event_fall")]
        public int CostChangeEventFall { get; set; }
        [JsonProperty("in_dreamteam")]
        public bool IsInDreamTeam { get; set; }
        [JsonProperty("dreamteam_count")]
        public int DreamTeamCount { get; set; }
        [JsonProperty("selected_by_percent")]
        public string SelectedByPercent { get; set; }
        [JsonProperty("form")]
        public string Form { get; set; }
        [JsonProperty("transfers_out")]
        public int TransfersOut { get; set; }
        [JsonProperty("transfers_in")]
        public int TransfersIn { get; set; }
        [JsonProperty("transfers_out_event")]
        public int TransfersOutEvent { get; set; }
        [JsonProperty("transfers_in_event")]
        public int TransfersInEvent { get; set; }
        [JsonProperty("loans_in")]
        public int LoansIn { get; set; }
        [JsonProperty("loans_out")]
        public int LoansOut { get; set; }
        [JsonProperty("loaned_in")]
        public int LoanedIn { get; set; }
        [JsonProperty("loaned_out")]
        public int LoanedOut { get; set; }
        [JsonProperty("total_points")]
        public int TotalPoints { get; set; }
        [JsonProperty("event_points")]
        public int EventPoints { get; set; }
        [JsonProperty("points_per_game")]
        public string PointsPerGame { get; set; }
        [JsonProperty("ep_this")]
        public string EPThis { get; set; }
        [JsonProperty("ep_next")]
        public string EPNext { get; set; }
        [JsonProperty("special")]
        public bool Special { get; set; }
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
        public int BPS { get; set; }
        [JsonProperty("influence")]
        public string Influence { get; set; }
        [JsonProperty("creativity")]
        public string Creativity { get; set; }
        [JsonProperty("threat")]
        public string Threat { get; set; }
        [JsonProperty("ict_index")]
        public string IctIndex { get; set; }
        [JsonProperty("ea_index")]
        public int EAIndex { get; set; }
        [JsonProperty("element_type")]
        public int ElementType { get; set; }
        [JsonProperty("team")]
        public int Team { get; set; }

        [JsonProperty("influence_rank")]
        public int InfluenceRank { get; set; }
        [JsonProperty("influence_rank_type")]
        public int InfluenceRankType { get; set; }

        [JsonProperty("creativity_rank")]
        public int CreativityRank { get; set; }
        [JsonProperty("creativity_rank_type")]
        public int CreativityRankType { get; set; }

        [JsonProperty("threat_rank")]
        public int ThreatRank { get; set; }
        [JsonProperty("threat_rank_type")]
        public int ThreatRankType { get; set; }

        [JsonProperty("ict_index_rank")]
        public int IctIndexRank { get; set; }
        [JsonProperty("ict_index_rank_type")]
        public int IctIndexRankType { get; set; }

        [JsonProperty("corners_and_indirect_freekicks_order")]
        public int? CornersAndIndirectFreeKicksOrder { get; set; }
        [JsonProperty("corners_and_indirect_freekicks_text")]
        public string CornersAndIndirectFreeKicksText { get; set; }

        [JsonProperty("direct_freekicks_order")]
        public int? DirectFreeKicksOrder { get; set; }
        [JsonProperty("direct_freekicks_text")]
        public string DirectFreeKicksText { get; set; }

        [JsonProperty("penalties_order")]
        public int? PenaltiesOrder { get; set; }
        [JsonProperty("penalties_text")]
        public string PenaltiesText { get; set; }

        // xG ,etc.
        [JsonProperty("expected_goals")]
        public string XG { get; set; }

        [JsonProperty("expected_assists")]
        public string XA { get; set; }

        [JsonProperty("expected_goal_involvements")]
        public string XGI { get; set; }

        [JsonProperty("expected_goals_conceded")]
        public string XGC { get; set; }

        // per 90
        [JsonProperty("expected_goals_per_90")]
        public double XGPer90 { get; set; }

        [JsonProperty("saves_per_90")]
        public double SavesPer90 { get; set; }

        [JsonProperty("expected_assists_per_90")]
        public double XAPer90 { get; set; }

        [JsonProperty("expected_goal_involvements_per_90")]
        public double XGIPer90 { get; set; }

        [JsonProperty("expected_goals_conceded_per_90")]
        public double XGCPer90 { get; set; }

        [JsonProperty("goals_conceded_per_90")]
        public double GCPer90 { get; set; }

        [JsonProperty("starts_per_90")]
        public double StartsPer90 { get; set; }

        [JsonProperty("clean_sheets_per_90")]
        public double CleanSheetsPer90 { get; set; }
    }
}