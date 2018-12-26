using Newtonsoft.Json;

namespace FantasyPremierLeague
{
    public class Pick
    {
        [JsonProperty("can_sub")]
        public bool CanSub { get; set; }
        [JsonProperty("has_played")]
        public bool HasPlayed { get; set; }
        [JsonProperty("is_sub")]
        public bool IsSub { get; set; }
        [JsonProperty("can_captain")]
        public bool CanCaptain { get; set; }
        [JsonProperty("selling_price")]
        public int SellingPrice { get; set; }
        [JsonProperty("multiplier")]
        public int Multiplier { get; set; }
        [JsonProperty("is_captain")]
        public bool IsCaptain { get; set; }
        [JsonProperty("is_vice_captain")]
        public bool IsViceCaptain { get; set; }
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("element")]
        public int ElementId { get; set; }
    }
}