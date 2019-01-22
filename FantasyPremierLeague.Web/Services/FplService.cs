using FantasyPremierLeague.Web.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyPremierLeague.Web.Services
{
    public class FplService
    {
        private const int MyTeamId = 2042915;

        private async Task<IEnumerable<Player>> GetPlayersByElementTypeAsync(ElementType elementType)
        {
            var fplApiClient = new WebApiClient();
            StaticResponse staticResponse = await fplApiClient.GetStaticAsync();

            TeamResponse teamResponse = await fplApiClient.GetTeamAsync(MyTeamId, staticResponse.CurrentEvent);
            var pickedElementIds = new HashSet<int>(teamResponse.Picks.Select(p => p.ElementId));

            double minimumMinutesRatio = 0.5d;
            double totalMinutes = staticResponse.CurrentEvent * 90;
            double minimumMinutesPlayed = totalMinutes * minimumMinutesRatio;
            Dictionary<int, string> teamNamesById = staticResponse.Teams.ToDictionary(
                t => t.Id,
                t => t.Name);
            IEnumerable<Element> elements = staticResponse.Elements
                .Where(e => e.ElementType == (int)elementType)
                .Where(e => e.Minutes >= minimumMinutesPlayed);
            IEnumerable<Player> players = elements.Select(
                e => Player.FromElement(e, teamNamesById, pickedElementIds));

            return players;
        }

        public async Task<IEnumerable<Player>> GetGoalkeepersAsync()
        {
            return await GetPlayersByElementTypeAsync(ElementType.Goalkeeper);
        }

        public async Task<IEnumerable<Player>> GetDefendersAsync()
        {
            return await GetPlayersByElementTypeAsync(ElementType.Defender);
        }

        public async Task<IEnumerable<Player>> GetMidfieldersAsync()
        {
            return await GetPlayersByElementTypeAsync(ElementType.Midfielder);
        }

        public async Task<IEnumerable<Player>> GetForwardsAsync()
        {
            return await GetPlayersByElementTypeAsync(ElementType.Forward);
        }
    }
}
