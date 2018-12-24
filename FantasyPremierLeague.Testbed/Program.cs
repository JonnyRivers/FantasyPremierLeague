using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FantasyPremierLeague.Testbed
{
    class Program
    {
        static void Main(string[] args)
        {
            var fplWebApiClient = new WebApiClient();
            StaticResponse staticResponse = fplWebApiClient.GetStaticAsync().Result;
            foreach (Element element in staticResponse.Elements)
            {
                ElementDetailResponse elementDetail = fplWebApiClient.GetElementDetailAsync(element.Id).Result;
                string teamName = staticResponse.Teams.Single(t => t.Id == element.Team).Name;
                int minutesPlayed = elementDetail.History.Sum(h => h.Minutes);
                decimal nowCostMillions = element.NowCost / 10;
                Console.WriteLine(
                    $"{element.FirstName} {element.SecondName} plays for {teamName}, " + 
                    $"played {minutesPlayed} minutes this season, and can be bought for {nowCostMillions:N1}.");
            }
        }
    }
}
