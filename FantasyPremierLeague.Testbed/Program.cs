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
                    Console.WriteLine($"{rank++}. {player.Name} ({player.NowCost:N1}m): {player.IctIndexPerMinutePlayed:N3} ({player.MinutesPlayed} minutes played)");
                }
                Console.WriteLine();
            }
        }

        static void PrintPlayerRows(IEnumerable<Player> players)
        {
            Console.WriteLine(
                "Player," + 
                "Threat,Goals," + 
                "Creativity,Assists," + 
                "Influence,Bonus," + 
                "ICT,Points");
            foreach (Player player in players)
            {
                Console.WriteLine(
                    $"{player.Name}," + 
                    $"{player.Threat},{player.Goals}," + 
                    $"{player.Creativity},{player.Assists}," + 
                    $"{player.Influence},{player.Bonus}," + 
                    $"{player.IctIndex},{player.TotalPoints}");
            }
        }

        static void PrintByPositionTable(IEnumerable<Player> players, int currentEvent)
        {
            double minimumMinutesRatio = 0.5d;
            double totalMinutes = currentEvent * 90;
            int minimumMinutes = (int)(minimumMinutesRatio * totalMinutes);
            IEnumerable<Player> playersThatPlayedOften = players.Where(p => p.MinutesPlayed >= minimumMinutes);
            IEnumerable<Player> defendersThatPlayedOften = playersThatPlayedOften.Where(p => p.Position == Position.Defender);
            IEnumerable<Player> midfieldersThatPlayedOften = playersThatPlayedOften.Where(p => p.Position == Position.Midfielder);
            IEnumerable<Player> forwardsThatPlayedOften = playersThatPlayedOften.Where(p => p.Position == Position.Forward);

            Console.WriteLine("Defenders");
            Console.WriteLine("*********");
            PrintPlayerRows(defendersThatPlayedOften);
            Console.WriteLine();

            Console.WriteLine("Midfielders");
            Console.WriteLine("***********");
            PrintPlayerRows(midfieldersThatPlayedOften);
            Console.WriteLine();

            Console.WriteLine("Forwards");
            Console.WriteLine("********");
            PrintPlayerRows(forwardsThatPlayedOften);
            Console.WriteLine();
        }

        static async Task PrintAllExpectedPointsAsync(StaticResponse staticResponse)
        {
            var expectedPointsService = new ExpectedPointsService();

            // Start with Rondon
            Element rondon = staticResponse.Elements.Single(e => e.Id == 493);
            float expectedPoints = await expectedPointsService.CalculateExpectedPointsAsync(rondon);
            Console.WriteLine($"Expected points: {expectedPoints:N2}");
        }

        static async Task Main(string[] args)
        {
            var fplWebApiClient = new WebApiClient();

            StaticResponse staticResponse = await fplWebApiClient.GetStaticAsync();

            //int myTeamId = 1231491;
            //TeamResponse teamResponse = await fplWebApiClient.GetTeamAsync(
            //    myTeamId,
            //    staticResponse.CurrentEvent);

            var players = new List<Player>();
            foreach (Element element in staticResponse.Elements)
            {
                var player = new Player
                {
                    Name = $"{element.FirstName} {element.SecondName}",
                    MinutesPlayed = element.Minutes,
                    TotalPoints = element.TotalPoints,
                    IctIndex = double.Parse(element.IctIndex),
                    Threat = double.Parse(element.Threat),
                    Goals = element.GoalsScored,
                    Creativity = double.Parse(element.Creativity),
                    Assists = element.Assists,
                    Influence = double.Parse(element.Influence),
                    Bonus = element.Bonus,
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
            else if (command == "by-position-table")
            {
                PrintByPositionTable(players, staticResponse.CurrentEvent);
            }
            else if (command == "expected-points")
            {
                await PrintAllExpectedPointsAsync(staticResponse);
            }
        }
    }
}
