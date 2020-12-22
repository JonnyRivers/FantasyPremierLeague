using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace FantasyPremierLeague.Testbed
{
    public class PreviousMatch
    {
        public double IsValid;
        public double Minutes;
        public double Points;
        public double Influence;
        public double Creativity;
        public double Threat;
        public double AtHome;
        public double Difficulty;

        public override string ToString()
        {
            return $"{IsValid:F1},{Minutes:F1},{Points:F1},{Influence:F1},{Creativity:F1},{Threat:F1},{AtHome:F1},{Difficulty}";
        }

        public static string GetHeaderRow(string match)
        {
            return $"isvalid{match},minutes{match},points{match},influence{match},creativity{match},threat{match},athome{match},difficulty{match}";
        }
    }

    public class PreviousSeason
    {
        public double IsValid;
        public double Minutes;
        public double Points;
        public double Influence;
        public double Creativity;
        public double Threat;

        public override string ToString()
        {
            return $"{IsValid:F1},{Minutes:F1},{Points:F1},{Influence:F1},{Creativity:F1},{Threat:F1}";
        }

        public static string GetHeaderRow(string season)
        {
            return $"isvalid{season},minutes{season},points{season},influence{season},creativity{season},threat{season}";
        }
    }

    public class TrainingDataRow
    {
        public PreviousSeason Season2017;
        public PreviousSeason Season2018;
        public PreviousSeason Season2019;

        public PreviousMatch[] PreviousGameweeks;

        public double Difficulty;
        public double AtHome;
        public double Value;
        public double TotalPoints;
    }

    public class TrainingDataBuilder
    {
        private PreviousMatch BuildPreviousMatch(ElementHistory history, Fixture fixture)
        {
            var previousMatch = new PreviousMatch();
            if (history != null || fixture != null)
            {
                previousMatch.IsValid = 1;
                previousMatch.Minutes = history.Minutes;
                previousMatch.Points = history.TotalPoints;
                previousMatch.Influence = double.Parse(history.Influence);
                previousMatch.Creativity = double.Parse(history.Creativity);
                previousMatch.Threat = double.Parse(history.Threat);

                previousMatch.AtHome = history.WasHome ? 1 : 0;
                previousMatch.Difficulty = history.WasHome ? fixture.HomeTeamDifficulty : fixture.AwayTeamDifficulty;
            }
            else
            {
                previousMatch.IsValid = 0;
                previousMatch.Minutes = 0;
                previousMatch.Points = 0;
                previousMatch.Influence = 0;
                previousMatch.Creativity = 0;
                previousMatch.Threat = 0;
                previousMatch.AtHome = 0;
                previousMatch.Difficulty = 0;
            }

            return previousMatch;
        }

        private PreviousSeason BuildPreviousSeason(ElementHistoryPast history)
        {
            var previousSeason = new PreviousSeason();
            if (history != null)
            {
                previousSeason.IsValid = 1;
                previousSeason.Minutes = history.Minutes;
                previousSeason.Points = history.TotalPoints;
                previousSeason.Influence = double.Parse(history.Influence);
                previousSeason.Creativity = double.Parse(history.Creativity);
                previousSeason.Threat = double.Parse(history.Threat);
            }
            else
            {
                previousSeason.IsValid = 0;
                previousSeason.Minutes = 0;
                previousSeason.Points = 0;
                previousSeason.Influence = 0;
                previousSeason.Creativity = 0;
                previousSeason.Threat = 0;
            }

            return previousSeason;
        }

        private static string BuildGameweeksHeader(int currentRound)
        {
            var headerBuilder = new StringBuilder();
            for(int round = 1; round < currentRound; ++round)
            {
                headerBuilder.Append(PreviousMatch.GetHeaderRow($"gw-less-{round}"));
                if (round < (currentRound - 1))
                    headerBuilder.Append(",");
            }

            return headerBuilder.ToString();
        }

        public async Task Build(WebApiClient fplWebApiClient, StaticResponse staticResponse, string path)
        {
            IEnumerable<Fixture> fixtures = await fplWebApiClient.GetFixturesAsync();

            using (StreamWriter writer = new StreamWriter(File.OpenWrite(path)))
            {
                Event currentEvent = staticResponse.Events.Single(e => e.IsCurrent);
                int numPreviousGameweeks = currentEvent.Id - 1;

                string gameweeksHeader = BuildGameweeksHeader(currentEvent.Id);
                writer.WriteLine($"{PreviousSeason.GetHeaderRow("2017")},{PreviousSeason.GetHeaderRow("2018")},{PreviousSeason.GetHeaderRow("2019")},{gameweeksHeader},position,diff,home,value,points");

                int progress = 0;
                //IEnumerable<Element> elementsToProcess = staticResponse.Elements.Take(1);
                IEnumerable<Element> elementsToProcess = staticResponse.Elements;
                int max = elementsToProcess.Count();
                //foreach (Element element in staticResponse.Elements)
                foreach (Element element in elementsToProcess)
                {
                    Console.WriteLine($"Processing {++progress} of {max}");

                    ElementSummaryResponse elementResponse = await fplWebApiClient.GetElementSummaryAsync(element.Id);

                    if (elementResponse == null)
                        continue;

                    ElementHistoryPast history2017 = elementResponse.HistoryPast.SingleOrDefault(hp => hp.SeasonName == "2017/18");
                    ElementHistoryPast history2018 = elementResponse.HistoryPast.SingleOrDefault(hp => hp.SeasonName == "2018/19");
                    ElementHistoryPast history2019 = elementResponse.HistoryPast.SingleOrDefault(hp => hp.SeasonName == "2019/20");

                    PreviousSeason season2017 = BuildPreviousSeason(history2017);
                    PreviousSeason season2018 = BuildPreviousSeason(history2018);
                    PreviousSeason season2019 = BuildPreviousSeason(history2019);

                    // First pass to build all previous match data by gameweek
                    var previousMatchesByGameweek = new Dictionary<int, PreviousMatch>();
                    foreach (ElementHistory history in elementResponse.History)
                    {
                        // TODO: add support for multi-match weeks
                        if (previousMatchesByGameweek.ContainsKey(history.Round))
                            continue;

                        Fixture fixture = fixtures.Single(f => f.Id == history.FixtureId);
                        PreviousMatch gameweek = BuildPreviousMatch(history, fixture);

                        previousMatchesByGameweek.Add(history.Round, gameweek);
                    }

                    PreviousMatch gameweekNull = BuildPreviousMatch(null, null);

                    foreach (ElementHistory history in elementResponse.History)
                    {
                        TrainingDataRow row = new TrainingDataRow();
                        row.Season2017 = season2017;
                        row.Season2018 = season2018;
                        row.Season2019 = season2019;

                        row.PreviousGameweeks = new PreviousMatch[numPreviousGameweeks];
                        int previousRound = history.Round - 1;
                        for (int round = 0; round < numPreviousGameweeks; ++round)
                        {
                            if(previousMatchesByGameweek.ContainsKey(previousRound))
                            {
                                row.PreviousGameweeks[round] = previousMatchesByGameweek[previousRound];
                            }
                            else
                            {
                                row.PreviousGameweeks[round] = gameweekNull;
                            }

                            --previousRound;
                        }

                        Fixture fixture = fixtures.Single(f => f.Id == history.FixtureId);
                        row.AtHome = history.WasHome ? 1 : 0;
                        if (history.WasHome)
                        {
                            row.Difficulty = fixture.HomeTeamDifficulty;
                        }
                        else
                        {
                            row.Difficulty = fixture.AwayTeamDifficulty;
                        }
                        row.Value = history.Value;
                        row.TotalPoints = history.TotalPoints;

                        string seasonsData = $"{row.Season2017},{row.Season2018},{row.Season2019}";
                        var previousGameweeksBuilder = new StringBuilder();
                        for (int round = 0; round < numPreviousGameweeks; ++round)
                        {
                            previousGameweeksBuilder.Append(row.PreviousGameweeks[round]);
                            if (round < (numPreviousGameweeks - 1))
                                previousGameweeksBuilder.Append(",");
                        }
                        string gameweeksData = previousGameweeksBuilder.ToString();
                        string otherData = $"{element.ElementType},{row.Difficulty},{row.AtHome},{row.Value:F1},{row.TotalPoints:F1}";
                        writer.WriteLine($"{seasonsData},{gameweeksData},{otherData}");
                    }
                }
            }
        }
    }
}
