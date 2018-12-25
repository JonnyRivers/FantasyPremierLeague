namespace FantasyPremierLeague.Testbed
{
    public class Player
    {
        public string Name { get; set; }
        public int MinutesPlayed { get; set; }
        public double IctIndex { get; set; }
        public double NowCost { get; set; }
        public int TeamId { get; set; }

        public double IctIndexPerMinutePlayed { get { return IctIndex / MinutesPlayed; } }
        public double IctIndexPerMinutePlayedPerCost { get { return IctIndex / MinutesPlayed / NowCost; } }
    }
}
