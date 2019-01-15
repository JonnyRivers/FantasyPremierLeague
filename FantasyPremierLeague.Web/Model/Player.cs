namespace FantasyPremierLeague.Web.Model
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }
        public int Points { get; set; }
        public float NowCost { get; set; }
        public int MinutesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Conceded { get; set; }
        public int CleanSheets { get; set; }
        public float IctIndex { get; set; }
    }
}
