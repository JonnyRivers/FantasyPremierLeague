using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public class WebApiClient
    {
        private const string StaticRequestUri = "https://fantasy.premierleague.com/api/bootstrap-static/";
        private const string FixturesRequestUri = "https://fantasy.premierleague.com/api/fixtures/";
        private const string ElementDetailRequestBaseUri = "https://fantasy.premierleague.com/api/element-summary/";

        public async Task<StaticResponse> GetStaticAsync()
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(StaticRequestUri);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Received non-success status code {httpResponseMessage.ReasonPhrase} from FPL Web API");
                }

                string httpResponseContentText = await httpResponseMessage.Content.ReadAsStringAsync();

                StaticResponse staticResponse = JsonConvert.DeserializeObject<StaticResponse>(httpResponseContentText);
                return staticResponse;
            }   
        }

        public async Task<IEnumerable<Fixture>> GetFixturesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(FixturesRequestUri);
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Received non-success status code {httpResponseMessage.ReasonPhrase} from FPL Web API");
                }

                string httpResponseContentText = await httpResponseMessage.Content.ReadAsStringAsync();

                IEnumerable<Fixture> fixtures = JsonConvert.DeserializeObject<IEnumerable<Fixture>>(httpResponseContentText);
                return fixtures;
            }
        }

        public async Task<ElementSummaryResponse> GetElementSummaryAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"{ElementDetailRequestBaseUri}{id}/");
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Received non-success status code {httpResponseMessage.ReasonPhrase} from FPL Web API");
                }

                string httpResponseContentText = await httpResponseMessage.Content.ReadAsStringAsync();

                try
                {
                    ElementSummaryResponse elementSummaryResponse = JsonConvert.DeserializeObject<ElementSummaryResponse>(httpResponseContentText, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    return elementSummaryResponse;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Failed to get element summary for {id}");
                    Console.WriteLine(ex);

                    return null;
                }
            }
        }

        public async Task<TeamResponse> GetTeamAsync(int id, int eventNumber)
        {
            using (var httpClient = new HttpClient())
            {
                // TODO https://fantasy.premierleague.com/api/entry/1494020/event/19/picks/
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(
                    $"https://fantasy.premierleague.com/api/entry/{id}/event/{eventNumber}/picks/");
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Received non-success status code {httpResponseMessage.ReasonPhrase} from FPL Web API");
                }

                string httpResponseContentText = await httpResponseMessage.Content.ReadAsStringAsync();

                TeamResponse teamResponse = JsonConvert.DeserializeObject<TeamResponse>(httpResponseContentText);
                return teamResponse;
            }
        }
    }
}
