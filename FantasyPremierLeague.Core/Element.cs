using Newtonsoft.Json;
using System;

namespace FantasyPermierLeague
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

        [JsonProperty("team")]
        public int Team { get; set; }
    }
}