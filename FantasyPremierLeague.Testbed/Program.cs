using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyPremierLeague.Testbed
{
    class Program
    {
        static void PrintPlayersByTopIctPerMinutePlayed(IEnumerable<Player> players)
        {
            int rank = 1;
            foreach (Player player in players.OrderByDescending(p => p.IctIndexPerMinutePlayed))
            {
                Console.WriteLine(
                    $"{rank++}. {player.Name} [{player.Team.Name}] ({player.NowCost:N1}m): {player.IctIndexPerMinutePlayed:N3}");
            }
        }

        static void PrintIctRankings(IEnumerable<Player> players, int currentEvent)
        {
            double minimumMinutesRatio = 0.5d;
            double totalMinutes = currentEvent * 90;
            int minimumMinutes = (int)(minimumMinutesRatio * totalMinutes);
            IEnumerable<Player> playersThatPlayedOften = players.Where(p => p.MinutesPlayed >= minimumMinutes);
            IEnumerable<Player> defendersThatPlayedOften = playersThatPlayedOften.Where(p => p.Position == Position.Defender);
            IEnumerable<Player> midfieldersThatPlayedOften = playersThatPlayedOften.Where(p => p.Position == Position.Midfielder);
            IEnumerable<Player> forwardsThatPlayedOften = playersThatPlayedOften.Where(p => p.Position == Position.Forward);

            Console.WriteLine("Defenders by ICT per minute played");
            Console.WriteLine("********************************");
            PrintPlayersByTopIctPerMinutePlayed(defendersThatPlayedOften);
            Console.WriteLine();

            Console.WriteLine("Midfielders by ICT per minute played");
            Console.WriteLine("********************************");
            PrintPlayersByTopIctPerMinutePlayed(midfieldersThatPlayedOften);
            Console.WriteLine();

            Console.WriteLine("Forwards by ICT per minute played");
            Console.WriteLine("********************************");
            PrintPlayersByTopIctPerMinutePlayed(forwardsThatPlayedOften);
            Console.WriteLine();
        }

        static void PrintTeamIctRankings(IEnumerable<Player> players, IEnumerable<Team> teams)
        {
            foreach (Team team in teams)
            {
                Console.WriteLine($"{team.Name} players by ICT per minute played");
                Console.WriteLine("********************************************************");
                int rank = 1;
                foreach (Player player in players
                    .Where(p => p.Team.Id == team.Id)
                    .OrderByDescending(p => p.IctIndexPerMinutePlayed))
                {
                    Console.WriteLine($"{rank++}. {player.Name} ({player.NowCost:N1}m) ICT per minute played {player.IctIndexPerMinutePlayed:N3}");
                }
                Console.WriteLine();
            }
        }

        static async Task Main(string[] args)
        {
            var fplWebApiClient = new WebApiClient();
            //int myTeamId = 2042915;
            //Task<TeamResponse> teamResponseTask = fplWebApiClient.GetTeamAsync(myTeamId);
            Task<StaticResponse> staticResponseTask = fplWebApiClient.GetStaticAsync();
            
            //TeamResponse teamResponse = await teamResponseTask;
            StaticResponse staticResponse = await staticResponseTask;

            var players = new List<Player>();
            
            foreach (Element element in staticResponse.Elements)
            {
                var player = new Player
                {
                    Name = $"{element.FirstName} {element.SecondName}",
                    MinutesPlayed = element.Minutes,
                    IctIndex = double.Parse(element.IctIndex),
                    NowCost = element.NowCost / 10d,
                    Position = (Position)element.ElementType,
                    Team = staticResponse.Teams.Single(t => t.Id == element.Team)
                };
                players.Add(player);
            }

            string command = args.Length >= 1 ? args[0] : String.Empty;

            if(command == "ict-rankings")
            {
                PrintIctRankings(players, staticResponse.CurrentEvent);
            }
            else if (command == "team-ict-rankings")
            {
                PrintTeamIctRankings(players, staticResponse.Teams);
            }
        }
    }
}
