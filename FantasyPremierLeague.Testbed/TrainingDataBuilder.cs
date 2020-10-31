using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague.Testbed
{
    public class TrainingDataBuilder
    {
        public async Task Build(WebApiClient fplWebApiClient, StaticResponse staticResponse)
        {
            // Get James Milner ('cos he's played lots of seasons)
            int jamesMilnerElementId = 241;
            ElementSummaryResponse jamesMilnerElementResponse = await fplWebApiClient.GetElementSummaryAsync(jamesMilnerElementId);

            foreach(ElementFixture fixture in jamesMilnerElementResponse.Fixtures)
            {
                Team homeTeam = staticResponse.Teams.Single(t => t.Id == fixture.HomeTeamId);
                Team awayTeam = staticResponse.Teams.Single(t => t.Id == fixture.AwayTeamId);
                Console.WriteLine($"Event {fixture.EventName} - {homeTeam.Name} vs {awayTeam.Name} at {fixture.KickOffTime}");
            }            
        }
    }
}
