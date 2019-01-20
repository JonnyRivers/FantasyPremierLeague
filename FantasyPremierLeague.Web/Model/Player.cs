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
        public int Saves { get; set; }
        public float SavesPerNinety { get; set; }
        public float SavesPerNinetyPerNowCost { get; set; }
        public int Conceded { get; set; }
        public float ConcededPerNinety { get; set; }
        public float ConcededPerNinetyPerNowCost { get; set; }
        public int CleanSheets { get; set; }
        public float CleanSheetsPerNinety { get; set; }
        public float CleanSheetsPerNinetyPerNowCost { get; set; }
        public int Points { get; set; }
        public float PointsPerNinety { get; set; }
        public float PointsPerNinetyPerNowCost { get; set; }
        public float IctIndex { get; set; }
        public float IctIndexPerNinety { get; set; }
        public float IctIndexPerNinetyPerNowCost { get; set; }

        internal static Player FromElement(
            Element element,
            Dictionary<int, string> teamNamesById)
        {
            float minutes = element.Minutes;
            float nineties = minutes / 90;
            float nowCost = (float)Math.Round(element.NowCost * 0.1d, 1);
            float saves = element.Saves;
            float savesPerNinety = minutes > 0 ? saves / nineties : 0;
            float savesPerNinetyPerNowCost = savesPerNinety / nowCost;
            float conceded = element.GoalsConceded;
            float concededPerNinety = minutes > 0 ? conceded / nineties : 0;
            float concededPerNinetyPerNowCost = concededPerNinety / nowCost;
            float cleanSheets = element.CleanSheets;
            float cleanSheetsPerNinety = minutes > 0 ? cleanSheets / nineties : 0;
            float cleanSheetsPerNinetyPerNowCost = cleanSheetsPerNinety / nowCost;
            float points = element.TotalPoints;
            float pointsPerNinety = minutes > 0 ? points / nineties : 0;
            float pointsPerNinetyPerNowCost = pointsPerNinety / nowCost;
            float ictIndex = float.Parse(element.IctIndex);
            float ictIndexPerNinety = minutes > 0 ? ictIndex / nineties : 0;
            float ictIndexPerNinetyPerNowCost = ictIndexPerNinety / nowCost;
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
                Saves = element.Saves,
                SavesPerNinety = (float)Math.Round(savesPerNinety, 2),
                SavesPerNinetyPerNowCost = (float)Math.Round(savesPerNinetyPerNowCost, 3),
                Conceded = element.GoalsConceded,
                ConcededPerNinety = (float)Math.Round(concededPerNinety, 2),
                ConcededPerNinetyPerNowCost = (float)Math.Round(concededPerNinetyPerNowCost, 3),
                CleanSheets = element.CleanSheets,
                CleanSheetsPerNinety = (float)Math.Round(cleanSheetsPerNinety, 2),
                CleanSheetsPerNinetyPerNowCost = (float)Math.Round(cleanSheetsPerNinetyPerNowCost, 3),
                Points = element.TotalPoints,
                PointsPerNinety = (float)Math.Round(pointsPerNinety, 2),
                PointsPerNinetyPerNowCost = (float)Math.Round(pointsPerNinetyPerNowCost, 3),
                IctIndex = ictIndex,
                IctIndexPerNinety = (float)Math.Round(ictIndexPerNinety, 1),
                IctIndexPerNinetyPerNowCost = (float)Math.Round(ictIndexPerNinetyPerNowCost, 2)
            };
        }
    }
}
