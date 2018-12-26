﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace FantasyPremierLeague
{
    public class WebApiClient
    {
        private const string StaticRequestUri = "https://fantasy.premierleague.com/drf/bootstrap-static";
        private const string ElementDetailRequestBaseUri = "https://fantasy.premierleague.com/drf/element-summary/";
        private const string TeamRequestBaseUri = "https://fantasy.premierleague.com/drf/my-team/";

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

        public async Task<ElementDetailResponse> GetElementDetailAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(ElementDetailRequestBaseUri + $"{id}");
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Received non-success status code {httpResponseMessage.ReasonPhrase} from FPL Web API");
                }

                string httpResponseContentText = await httpResponseMessage.Content.ReadAsStringAsync();

                ElementDetailResponse elementDetailResponse = JsonConvert.DeserializeObject<ElementDetailResponse>(httpResponseContentText);
                return elementDetailResponse;
            }
        }

        public async Task<TeamResponse> GetTeamAsync(int id)
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(TeamRequestBaseUri + $"{id}");
                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Received non-success status code {httpResponseMessage.ReasonPhrase} from FPL Web API");
                }

                string httpResponseContentText = await httpResponseMessage.Content.ReadAsStringAsync();

                TeamResponse elementDetailResponse = JsonConvert.DeserializeObject<TeamResponse>(httpResponseContentText);
                return elementDetailResponse;
            }
        }
    }
}
