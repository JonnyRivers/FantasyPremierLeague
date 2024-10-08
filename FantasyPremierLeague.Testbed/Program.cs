﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        static async Task BuildTrainingDataAsync(WebApiClient fplWebApiClient, StaticResponse staticResponse, string path)
        {
            var trainingDataBuilder = new TrainingDataBuilder();

            await trainingDataBuilder.Build(fplWebApiClient, staticResponse, path);
        }

        static async Task MakePredictionsAsync(WebApiClient fplWebApiClient, StaticResponse staticResponse, string path)
        {
            var predictionsMaker = new PredictionsMaker();

            await predictionsMaker.Make(fplWebApiClient, staticResponse, path);
        }

        static async Task PrintForwardFirstSeasonStatsAsync(WebApiClient fplWebApiClient, StaticResponse staticResponse)
        {
            foreach (Element element in staticResponse.Elements)
            {
                if (element.ElementType != (int)Position.Forward)
                    continue;

                ElementSummaryResponse elementResponse = await fplWebApiClient.GetElementSummaryAsync(element.Id);

                if (elementResponse == null)
                    continue;

                ElementHistoryPast firstSeason = elementResponse.HistoryPast.FirstOrDefault();

                if (firstSeason == null)
                    continue;

                Console.WriteLine($"{element.WebName} - £{firstSeason.StartCost / 10d}m - {firstSeason.Minutes} - {firstSeason.TotalPoints}");
            }
        }

        static void DumpToCSV(WebApiClient fplWebApiClient, StaticResponse staticResponse, int elementType, string path)
        {
            using (StreamWriter writer = new StreamWriter(File.Create(path)))
            {
                writer.WriteLine("id,second_name,team,selected_by_percent,now_cost,minutes,total_points");
                foreach (Element element in staticResponse.Elements.Where(e => e.ElementType == elementType))
                {
                    string teamName = staticResponse.Teams.Single(t => t.Id == element.Team).Name;
                    writer.WriteLine($"{element.Id},{element.SecondName},{teamName},{element.SelectedByPercent},{element.NowCost},{element.Minutes},{element.TotalPoints}");
                }
            }
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
            else if (command == "build-training-data")
            {
                string path = args[1];
                await BuildTrainingDataAsync(fplWebApiClient, staticResponse, path);
            }
            else if (command == "make-predictions")
            {
                string path = args[1];
                await MakePredictionsAsync(fplWebApiClient, staticResponse, path);
            }
            else if (command == "to-csv")
            {
                string directory = args[1];
                DumpToCSV(fplWebApiClient, staticResponse, 1, Path.Combine(directory, "gkp.csv"));
                DumpToCSV(fplWebApiClient, staticResponse, 2, Path.Combine(directory, "def.csv"));
                DumpToCSV(fplWebApiClient, staticResponse, 3, Path.Combine(directory, "mid.csv"));
                DumpToCSV(fplWebApiClient, staticResponse, 4, Path.Combine(directory, "fwd.csv"));
            }
            else
            {
                foreach(Team team in staticResponse.Teams.OrderByDescending(t => t.Strength))
                {
                    Console.WriteLine($"{team.Name} [{team.Strength}]");
                }

                Dictionary<int, string> elementShortNamesByType = new Dictionary<int, string>()
                {
                    { 1, "GKP" },
                    { 2, "DEF" },
                    { 3, "MID" },
                    { 4, "FWD" },
                };
                IEnumerable<Element> playersByEPNext = staticResponse.Elements.OrderByDescending(t => double.Parse(t.EPNext));
                for (int elementType = 1; elementType <= 4; elementType++)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{elementShortNamesByType[elementType]}");
                    foreach (Element element in playersByEPNext.Where(e => e.ElementType == elementType))
                    {
                        string teamName = staticResponse.Teams.Single(t => t.Id == element.Team).Name;
                        string nowCost = $"£{element.NowCost / 10d}m";
                        Console.WriteLine($"{element.WebName} ({teamName}) = {element.EPNext} (at {nowCost})");
                    }
                }
                IEnumerable<Element> playersByEPNextValue = staticResponse.Elements.OrderByDescending(t => double.Parse(t.EPNext) / (double)t.NowCost);
                for (int elementType = 1; elementType <= 4; elementType++)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{elementShortNamesByType[elementType]}");
                    foreach (Element element in playersByEPNextValue.Where(e => e.ElementType == elementType))
                    {
                        string teamName = staticResponse.Teams.Single(t => t.Id == element.Team).Name;
                        string nowCost = $"£{element.NowCost / 10d}m";
                        Console.WriteLine($"{element.WebName} ({teamName}) = {(double.Parse(element.EPNext) / (double)element.NowCost * 10):N2} (at {nowCost})");
                    }
                }
            }
        }
    }
}
