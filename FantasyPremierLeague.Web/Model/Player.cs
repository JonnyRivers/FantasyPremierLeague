using System;
using System.Collections.Generic;

namespace FantasyPremierLeague.Web.Model
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }
        public float NowCost { get; set; }
        public int MinutesPlayed { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int Conceded { get; set; }
        public int CleanSheets { get; set; }
        public int Points { get; set; }
        public float PointsPerMinutePlayed { get; set; }
        public float PointsPerMinutePlayedPerNowCost { get; set; }
        public float IctIndex { get; set; }
        public float IctIndexPerMinutePlayed { get; set; }
        public float IctIndexPerMinutePlayedPerNowCost { get; set; }

        internal static Player FromElement(
            Element element,
            Dictionary<int, string> teamNamesById)
        {
            float minutes = element.Minutes;
            float nowCost = (float)Math.Round(element.NowCost * 0.1d, 1);
            float points = element.TotalPoints;
            float pointsPerMinutePlayed = minutes > 0 ? points / minutes : 0;
            float pointsPerMinutePlayedPerNowCost = pointsPerMinutePlayed / nowCost;
            float ictIndex = float.Parse(element.IctIndex);
            float ictIndexPerMinutePlayed = minutes > 0 ? ictIndex / minutes : 0;
            float ictIndexPerMinutePlayedPerNowCost = ictIndexPerMinutePlayed / nowCost;
            return new Player
            {
                Id = element.Id,
                Name = (element.WebName == element.SecondName) ? $"{element.FirstName} {element.SecondName}" : element.WebName,
                Position = ((ElementType)element.ElementType).ToString(),
                Team = teamNamesById[element.Team],
                NowCost = nowCost,
                MinutesPlayed = element.Minutes,
                Goals = element.GoalsScored,
                Assists = element.Assists,
                Conceded = element.GoalsConceded,
                CleanSheets = element.CleanSheets,
                Points = element.TotalPoints,
                PointsPerMinutePlayed = (float)Math.Round(pointsPerMinutePlayed, 3),
                PointsPerMinutePlayedPerNowCost = (float)Math.Round(pointsPerMinutePlayedPerNowCost, 4),
                IctIndex = ictIndex,
                IctIndexPerMinutePlayed = (float)Math.Round(ictIndexPerMinutePlayed, 3),
                IctIndexPerMinutePlayedPerNowCost = (float)Math.Round(ictIndexPerMinutePlayedPerNowCost, 4)
            };
        }
    }
}
