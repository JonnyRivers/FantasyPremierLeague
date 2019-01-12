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
            IEnumerable<Player> players = staticResponse.Elements.Select(GetPlayerFromElement);
            return Json(players);
        }

        private static Player GetPlayerFromElement(FantasyPremierLeague.Element element)
        {
            return new Player
            {
                Id = element.Id,
                Name = $"{element.FirstName} {element.WebName}",
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