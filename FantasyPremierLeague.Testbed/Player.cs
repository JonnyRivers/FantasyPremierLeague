namespace FantasyPremierLeague.Testbed
{
    public class Player
    {
        public string Name { get; set; }
        public int MinutesPlayed { get; set; }
        public int TotalPoints { get; set; }
        public double IctIndex { get; set; }
        public double NowCost { get; set; }
        public Position Position { get; set; }
        public Team Team { get; set; }

        public double IctIndexPerMinutePlayed { get { return IctIndex / MinutesPlayed; } }
        public double IctIndexPerMinutePlayedPerCost { get { return IctIndex / MinutesPlayed / NowCost; } }
    }
}
