﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FantasyPremierLeague;
using FantasyPremierLeagueML.Model;

namespace FantasyPremierLeague.Testbed
{
    public class PredictionsMaker
    {
        private static void SetValueByAnyMeans(ModelInput input, PropertyInfo property, float value)
        {
            if (property.PropertyType == typeof(float))
            {
                property.SetValue(input, value);
            }
            else if (property.PropertyType == typeof(string))
            {
                property.SetValue(input, value.ToString());
            }
        }

        private static void AddPreviousGameweekToInput(ModelInput input, ElementHistory elementHistory, Fixture fixture, int currentEventId)
        {
            Type inputType = typeof(ModelInput);

            if (elementHistory.Round >= currentEventId)
                return;

            int gameweekLessIndex = currentEventId - elementHistory.Round;
            PropertyInfo isValidProperty = inputType.GetProperty($"Isvalidgw_less_{gameweekLessIndex}");
            PropertyInfo minutesProperty = inputType.GetProperty($"Minutesgw_less_{gameweekLessIndex}");
            PropertyInfo pointsProperty = inputType.GetProperty($"Pointsgw_less_{gameweekLessIndex}");
            PropertyInfo influenceProperty = inputType.GetProperty($"Influencegw_less_{gameweekLessIndex}");
            PropertyInfo creativityProperty = inputType.GetProperty($"Creativitygw_less_{gameweekLessIndex}");
            PropertyInfo threatProperty = inputType.GetProperty($"Threatgw_less_{gameweekLessIndex}");
            PropertyInfo atHomeProperty = inputType.GetProperty($"Athomegw_less_{gameweekLessIndex}");
            PropertyInfo difficultyProperty = inputType.GetProperty($"Difficultygw_less_{gameweekLessIndex}");

            SetValueByAnyMeans(input, isValidProperty, 1f);
            SetValueByAnyMeans(input, minutesProperty, elementHistory.Minutes);
            SetValueByAnyMeans(input, pointsProperty, elementHistory.TotalPoints);
            SetValueByAnyMeans(input, influenceProperty, float.Parse(elementHistory.Influence));
            SetValueByAnyMeans(input, creativityProperty, float.Parse(elementHistory.Creativity));
            SetValueByAnyMeans(input, threatProperty, float.Parse(elementHistory.Threat));

            float atHome = elementHistory.WasHome ? 1 : 0;
            float difficulty = elementHistory.WasHome ? fixture.HomeTeamDifficulty : fixture.AwayTeamDifficulty;

            SetValueByAnyMeans(input, atHomeProperty, atHome);
            SetValueByAnyMeans(input, difficultyProperty, difficulty);
        }

        public async Task Make(WebApiClient fplWebApiClient, StaticResponse staticResponse, string path)
        {
            IEnumerable<Fixture> fixtures = await fplWebApiClient.GetFixturesAsync();

            using (StreamWriter writer = new StreamWriter(File.Create(path)))
            {
                writer.WriteLine("event_id,player_id,player_name,difficulty,home,points");

                int progress = 0;
                int max = staticResponse.Elements.Count();
                foreach (Element element in staticResponse.Elements)
                {
                    Console.WriteLine($"Predicting {++progress} of {max}");

                    ElementSummaryResponse elementResponse = await fplWebApiClient.GetElementSummaryAsync(element.Id);

                    if (elementResponse == null)
                        continue;

                    ElementHistoryPast history2017 = elementResponse.HistoryPast.SingleOrDefault(hp => hp.SeasonName == "2017/18");
                    ElementHistoryPast history2018 = elementResponse.HistoryPast.SingleOrDefault(hp => hp.SeasonName == "2018/19");
                    ElementHistoryPast history2019 = elementResponse.HistoryPast.SingleOrDefault(hp => hp.SeasonName == "2019/20");

                    ModelInput fixtureData = new ModelInput()
                    {
                        Isvalid2017 = (history2017 == null) ? 0 : 1,
                        Minutes2017 = (history2017 == null) ? 0 : history2017.Minutes,
                        Points2017 = (history2017 == null) ? 0 : history2017.TotalPoints,
                        Influence2017 = (history2017 == null) ? 0 : float.Parse(history2017.Influence),
                        Creativity2017 = (history2017 == null) ? 0 : float.Parse(history2017.Creativity),
                        Threat2017 = (history2017 == null) ? 0 : float.Parse(history2017.Threat),
                        Isvalid2018 = (history2018 == null) ? 0 : 1,
                        Minutes2018 = (history2018 == null) ? 0 : history2018.Minutes,
                        Points2018 = (history2018 == null) ? 0 : history2018.TotalPoints,
                        Influence2018 = (history2018 == null) ? 0 : float.Parse(history2018.Influence),
                        Creativity2018 = (history2018 == null) ? 0 : float.Parse(history2018.Creativity),
                        Threat2018 = (history2018 == null) ? 0 : float.Parse(history2018.Threat),
                        Isvalid2019 = (history2019 == null) ? 0 : 1,
                        Minutes2019 = (history2019 == null) ? 0 : history2019.Minutes,
                        Points2019 = (history2019 == null) ? 0 : history2019.TotalPoints,
                        Influence2019 = (history2019 == null) ? 0 : float.Parse(history2019.Influence),
                        Creativity2019 = (history2019 == null) ? 0 : float.Parse(history2019.Creativity),
                        Threat2019 = (history2019 == null) ? 0 : float.Parse(history2019.Threat),

                        Playerid = element.Id,
                        Teamid = element.Team,
                        Position = element.ElementType,
                        Selectedby = float.Parse(element.SelectedByPercent),
                        Transfersin = element.TransfersInEvent,
                        Transfersout = element.TransfersOutEvent,
                        Diff = 2F,
                        Home = "0",
                        Value = element.NowCost
                    };

                    foreach (ElementHistory history in elementResponse.History)
                    {
                        Fixture gwFixture = fixtures.Single(f => f.Id == history.FixtureId);
                        AddPreviousGameweekToInput(fixtureData, history, gwFixture, staticResponse.CurrentEvent);
                    }

                    foreach (ElementFixture fixture in elementResponse.Fixtures.Take(5))
                    {
                        fixtureData.Diff = fixture.Difficulty;
                        fixtureData.Home = fixture.IsHome ? "1" : "0";

                        var predictionResult = ConsumeModel.Predict(fixtureData);

                        writer.WriteLine($"{fixture.EventId},{element.Id},{element.FirstName} {element.SecondName},{fixtureData.Diff},{fixtureData.Home},{predictionResult.Score}");
                    }
                }
            }
        }
    }
}
