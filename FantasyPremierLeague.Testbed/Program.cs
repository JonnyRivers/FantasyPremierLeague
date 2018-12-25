using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyPremierLeague.Testbed
{
    class Program
    {
        static void Main(string[] args)
        {
            var fplWebApiClient = new WebApiClient();
            StaticResponse staticResponse = fplWebApiClient.GetStaticAsync().Result;

            List<Player> players = new List<Player>();
            double minimumMinutesRatio = 0.3d;
            double totalMinutes = staticResponse.CurrentEvent * 90;
            int minimumMinutes = (int)(minimumMinutesRatio * totalMinutes);
            foreach (Element element in staticResponse.Elements.Where(e => e.Minutes >= minimumMinutes))
            {
                var player = new Player
                {
                    Name = $"{element.FirstName} {element.SecondName}",
                    MinutesPlayed = element.Minutes,
                    IctIndex = double.Parse(element.IctIndex),
                    NowCost = element.NowCost / 10d,
                    TeamId = element.Team
                };
                players.Add(player);
            }

            Console.WriteLine("Players by ICT per minute played");
            foreach (Player player in players.OrderByDescending(p => p.IctIndexPerMinutePlayed).Take(25))
            {
                Console.WriteLine($"{player.Name} ICT per minute played {player.IctIndexPerMinutePlayed:N3}");
            }
            Console.WriteLine();

            Console.WriteLine("Players by ICT per minute played per cost");
            foreach (Player player in players.OrderByDescending(p => p.IctIndexPerMinutePlayedPerCost).Take(25))
            {
                Console.WriteLine($"{player.Name} ICT per minute played per cost {player.IctIndexPerMinutePlayedPerCost:N3}");
            }
            Console.WriteLine();

            int newcastleTeamId = staticResponse.Teams.Single(t => t.Name == "Newcastle").Id;
            Console.WriteLine("Newcastle players by ICT per minute played");
            foreach (Player player in players.Where(p => p.TeamId == newcastleTeamId).OrderByDescending(p => p.IctIndexPerMinutePlayed))
            {
                Console.WriteLine($"{player.Name} ICT per minute played {player.IctIndexPerMinutePlayed:N3}");
            }
            Console.WriteLine();
        }
    }
}
