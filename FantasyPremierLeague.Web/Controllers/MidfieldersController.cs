using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyPremierLeague.Web.Model;
using FantasyPremierLeague.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace FantasyPremierLeague.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MidfieldersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var fplService = new FplService();
            IEnumerable<Player> midfielders = await fplService.GetMidfieldersAsync();
            return Json(midfielders);
        }
    }
}