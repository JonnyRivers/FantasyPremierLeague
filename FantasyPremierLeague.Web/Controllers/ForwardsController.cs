using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyPremierLeague.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace FantasyPremierLeague.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForwardsController : Controller
    {
        public static float IctIndexPerMinutePlayedPerNowCost { get; private set; }

        public async Task<IActionResult> Index()
        {
            var fplApiClient = new WebApiClient();
            StaticResponse staticResponse = await fplApiClient.GetStaticAsync();
            double minimumMinutesRatio = 0.5d;
            double totalMinutes = staticResponse.CurrentEvent * 90;
            double minimumMinutesPlayed = totalMinutes * minimumMinutesRatio;
            Dictionary<int, string> teamNamesById = staticResponse.Teams.ToDictionary(
                t => t.Id,
                t => t.Name);
            IEnumerable<Element> forwardElements = staticResponse.Elements
                .Where(e => e.ElementType == 4)
                .Where(e => e.Minutes >= minimumMinutesPlayed);
            IEnumerable<Forward> forwards = forwardElements.Select(
                e => GetForwardFromElement(e, teamNamesById));
            return Json(forwards);
        }

        private static Forward GetForwardFromElement(
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
            return new Forward
            {
                Id = element.Id,
                Name = (element.WebName == element.SecondName) ? $"{element.FirstName} {element.SecondName}" : element.WebName,
                Team = teamNamesById[element.Team],
                NowCost = nowCost,
                MinutesPlayed = element.Minutes,
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