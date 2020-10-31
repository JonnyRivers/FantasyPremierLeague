using Newtonsoft.Json;
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

        //public async Task<FixturesResponse> GetFixturesAsync()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(FixturesRequestUri);
        //        if (!httpResponseMessage.IsSuccessStatusCode)
        //        {
        //            throw new HttpRequestException(
        //                $"Received non-success status code {httpResponseMessage.ReasonPhrase} from FPL Web API");
        //        }

        //        string httpResponseContentText = await httpResponseMessage.Content.ReadAsStringAsync();

        //        FixturesResponse fixturesResponse = JsonConvert.DeserializeObject<FixturesResponse>(httpResponseContentText);
        //        return fixturesResponse;
        //    }
        //}

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

                TeamResponse elementDetailResponse = JsonConvert.DeserializeObject<TeamResponse>(httpResponseContentText);
                return elementDetailResponse;
            }
        }
    }
}
