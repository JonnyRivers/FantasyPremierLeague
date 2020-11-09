using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace FantasyPremierLeague.Testbed
{
    public class TrainingDataRow
    {
        public double Mins2017;
        public double Pts2017;
        public double Valid2017;
        public double Mins2018;
        public double Pts2018;
        public double Valid2018;
        public double Mins2019;
        public double Pts2019;
        public double Valid2019;
        public double Difficulty;
        public double AtHome;
        public double TotalPoints;
    }

    public class TrainingDataBuilder
    {
        public async Task Build(WebApiClient fplWebApiClient, StaticResponse staticResponse)
        {
            IEnumerable<Fixture> fixtures = await fplWebApiClient.GetFixturesAsync();

            using (StreamWriter writer = new StreamWriter(File.OpenWrite("data.csv")))
            {
                writer.WriteLine("mins2017,pts2017,valid2017,mins2018,pts2018,valid2018,mins2019,pts2019,valid2019,diff,home,points");

                int progress = 0;
                int max = staticResponse.Elements.Count();
                foreach (Element element in staticResponse.Elements)
                {
                    Console.WriteLine($"Processing {++progress} of {max}");

                    ElementSummaryResponse elementResponse = await fplWebApiClient.GetElementSummaryAsync(element.Id);

                    if (elementResponse == null)
                        continue;

                    ElementHistoryPast history2017 = elementResponse.HistoryPast.SingleOrDefault(hp => hp.SeasonName == "2017/18");
                    ElementHistoryPast history2018 = elementResponse.HistoryPast.SingleOrDefault(hp => hp.SeasonName == "2018/19");
                    ElementHistoryPast history2019 = elementResponse.HistoryPast.SingleOrDefault(hp => hp.SeasonName == "2019/20");

                    foreach (ElementHistory history in elementResponse.History)
                    {
                        TrainingDataRow row = new TrainingDataRow();
                        if (history2017 != null)
                        {
                            row.Mins2017 = history2017.Minutes;
                            row.Pts2017 = history2017.TotalPoints;
                            row.Valid2017 = 1;
                        }
                        else
                        {
                            row.Mins2017 = 0;
                            row.Pts2017 = 0;
                            row.Valid2017 = 0;
                        }
                        if (history2018 != null)
                        {
                            row.Mins2018 = history2018.Minutes;
                            row.Pts2018 = history2018.TotalPoints;
                            row.Valid2018 = 1;
                        }
                        else
                        {
                            row.Mins2018 = 0;
                            row.Pts2018 = 0;
                            row.Valid2018 = 0;
                        }
                        if (history2019 != null)
                        {
                            row.Mins2019 = history2019.Minutes;
                            row.Pts2019 = history2019.TotalPoints;
                            row.Valid2019 = 1;
                        }
                        else
                        {
                            row.Mins2019 = 0;
                            row.Pts2019 = 0;
                            row.Valid2019 = 0;
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
                        row.TotalPoints = history.TotalPoints;

                        writer.WriteLine($"{row.Mins2017},{row.Pts2017},{row.Valid2017},{row.Mins2018},{row.Pts2018},{row.Valid2018},{row.Mins2019},{row.Pts2019},{row.Valid2019},{row.Difficulty},{row.AtHome},{row.TotalPoints}");
                    }
                }
            }

                // Get James Milner ('cos he's played lots of seasons)
            //    int jamesMilnerElementId = 241;
            //ElementSummaryResponse jamesMilnerElementResponse = await fplWebApiClient.GetElementSummaryAsync(jamesMilnerElementId);

            //foreach(ElementFixture fixture in jamesMilnerElementResponse.Fixtures)
            //{
            //    Team homeTeam = staticResponse.Teams.Single(t => t.Id == fixture.HomeTeamId);
            //    Team awayTeam = staticResponse.Teams.Single(t => t.Id == fixture.AwayTeamId);
            //    Console.WriteLine($"Event {fixture.EventName} - {homeTeam.Name} vs {awayTeam.Name} at {fixture.KickOffTime} (D: {fixture.Difficulty})");
            //}

            //foreach(ElementHistoryPast historyPast in jamesMilnerElementResponse.HistoryPast)
            //{
            //    Console.WriteLine($"Season {historyPast.SeasonName} - Mins: {historyPast.Minutes}; Goals: {historyPast.GoalsScored}; Assists: {historyPast.Assists}; Bonus: {historyPast.Bonus}; Total Points: {historyPast.TotalPoints}");
            //}

            //foreach (ElementHistory history in jamesMilnerElementResponse.History)
            //{
            //    Fixture fixture = fixtures.Single(f => f.Id == history.FixtureId);
            //    Event evnt = staticResponse.Events.Single(e => e.Id == fixture.EventId);
            //    Team opponentTeam = staticResponse.Teams.Single(t => t.Id == history.OpponentTeamId);
            //    Console.WriteLine($"Event {evnt.Name} (vs {opponentTeam.Name} at {history.KickOffTime}) - Mins: {history.Minutes}; Goals: {history.GoalsScored}; Assists: {history.Assists}; Bonus: {history.Bonus}; Total Points: {history.TotalPoints}");
            //}
        }
    }
}
