using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyPremierLeague.Testbed
{
    public class ExpectedPointsService
    {
        private const float PointsPerYellowCard = -1;
        private const float PointsPerRedCard = -3;
        private const float PointsPerGoalForward = 4;
        private const float PointsPerGoalMidfielder = 5;
        private const float PointPerGoalDefender = 6;
        private const float ExpectedGoalsPerThreatForward = 96.5f;
        private const float ExpectedGoalsPerThreatMidfielder = 97.7f;
        private const float ExpectedGoalsPerThreatDefender = 122.6f;
        private const float HistoryGoalsFactor = 0.5f;
        private const float ThreatGoalsFactor = 0.5f;

        private static float PointsPerGoal(int elementType)
        {
            if (elementType == 4)
                return PointsPerGoalForward;
            if (elementType == 3)
                return PointsPerGoalMidfielder;

            return PointPerGoalDefender;
        }

        private static float ExpectedGoalsPerThreat(int elementType)
        {
            if (elementType == 4)
                return ExpectedGoalsPerThreatForward;
            if (elementType == 3)
                return ExpectedGoalsPerThreatMidfielder;

            return PointPerGoalDefender;
        }

        public async Task<float> CalculateExpectedPointsAsync(Element element)
        {
            var fplWebApiClient = new WebApiClient();
            ElementSummaryResponse elementSummaryResponse = await fplWebApiClient.GetElementSummaryAsync(element.Id);

            float minutesPlayed = elementSummaryResponse.History.Sum(h => h.Minutes);
            float ninetiesPlayed = minutesPlayed / 90;

            float yellowCards = element.YellowCards;
            float yellowCardsPerNinety = yellowCards / ninetiesPlayed;
            float expectedYellowCardPoints = (yellowCardsPerNinety * PointsPerYellowCard);
            float redCards = element.RedCards;
            float redCardsPerNinety = redCards / ninetiesPlayed;
            float expectedRedCardPoints = (redCardsPerNinety * PointsPerRedCard);

            // Goals contribution
            // TODO: consider discounting (100% for last 6 down to 50% for 20 games ago?)
            float pointsPerGoal = PointsPerGoal(element.ElementType);
            float goals = element.GoalsScored;
            float goalsPerNinety = goals / ninetiesPlayed;
            float expectedPointsFromGoalsOnHistory = pointsPerGoal * goalsPerNinety;

            float expectedGoalsPerThreat = ExpectedGoalsPerThreat(element.ElementType);
            float threat = elementSummaryResponse.History.Sum(h => float.Parse(h.Threat));
            float threatPerNinety = threat / ninetiesPlayed;
            float expectedGoalsFromThreat = threatPerNinety / expectedGoalsPerThreat;
            float expectedPointsFromGoalsOnThreat = pointsPerGoal * expectedGoalsFromThreat;

            float expectedPointsFromGoals =
                (expectedPointsFromGoalsOnHistory * HistoryGoalsFactor) +
                (expectedPointsFromGoalsOnThreat * ThreatGoalsFactor);

            return expectedYellowCardPoints +
                expectedRedCardPoints +
                expectedPointsFromGoals;
        }
    }
}
