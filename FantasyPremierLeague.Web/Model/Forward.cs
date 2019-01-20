namespace FantasyPremierLeague.Web.Model
{
    public class Forward
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public float NowCost { get; set; }
        public int MinutesPlayed { get; set; }
        public int Points { get; set; }
        public float PointsPerMinutePlayed { get; set; }
        public float PointsPerMinutePlayedPerNowCost { get; set; }
        public float IctIndex { get; set; }
        public float IctIndexPerMinutePlayed { get; set; }
        public float IctIndexPerMinutePlayedPerNowCost { get; set; }
    }
}
