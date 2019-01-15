using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyPremierLeague.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace FantasyPremierLeague.Web.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var fplApiClient = new WebApiClient();
            StaticResponse staticResponse = await fplApiClient.GetStaticAsync();
            var positionsById = new Dictionary<int, string>
            {
                {1, "Goalkeeper" },
                {2, "Defender" },
                {3, "Midfielder" },
                {4, "Forward" }
            };
            Dictionary<int, string> teamNamesById = staticResponse.Teams.ToDictionary(
                t => t.Id,
                t => t.Name);
            IEnumerable<Player> players = staticResponse.Elements.Select(
                e => GetPlayerFromElement(e, positionsById, teamNamesById));
            return Json(players);
        }

        private static Player GetPlayerFromElement(
            Element element, 
            Dictionary<int, string> positionsById, 
            Dictionary<int, string> teamNamesById)
        {
            return new Player
            {
                Id = element.Id,
                Name = (element.WebName == element.SecondName) ? $"{element.FirstName} {element.SecondName}" : element.WebName,
                Position = positionsById[element.ElementType],
                Team = teamNamesById[element.Team],
                Points = element.TotalPoints,
                NowCost = (float)Math.Round(element.NowCost * 0.1d, 1),
                MinutesPlayed = element.Minutes,
                Goals = element.GoalsScored,
                Assists = element.Assists,
                Conceded = element.GoalsConceded,
                CleanSheets = element.CleanSheets,
                IctIndex = float.Parse(element.IctIndex)
            };
        }
    }
}