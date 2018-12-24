using FantasyPermierLeague;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FantasyPremierLeague.Testbed
{
    class Program
    {
        static async Task<StaticResponse> GetFPLStaticAsync()
        {
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(
                "https://fantasy.premierleague.com/drf/bootstrap-static");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Got bad response from FPL Web API");
                return null;
            }

            string responseContentText = await response.Content.ReadAsStringAsync();

            StaticResponse responseObject = JsonConvert.DeserializeObject<StaticResponse>(responseContentText);
            return responseObject;
        }

        static async Task<ElementDetailResponse> GetFPLElementDetailAsync(int id)
        {
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(
                $"https://fantasy.premierleague.com/drf/element-summary/{id}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Got bad response from FPL Web API");
                return null;
            }

            string responseContentText = await response.Content.ReadAsStringAsync();

            ElementDetailResponse responseObject = JsonConvert.DeserializeObject<ElementDetailResponse>(responseContentText);
            return responseObject;
        }

        static void Main(string[] args)
        {
            StaticResponse responseObject = GetFPLStaticAsync().Result;
            foreach (Element element in responseObject.Elements)
            {
                ElementDetailResponse elementDetail = GetFPLElementDetailAsync(element.Id).Result;
            }
        }
    }
}
