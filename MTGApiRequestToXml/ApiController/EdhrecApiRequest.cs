using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.ApiController
{
    public class EdhrecApiTool
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> GetApiResponseAsync(string goodcardName)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://json.edhrec.com/pages/commanders/{goodcardName}.json");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }
        private async Task<T> GetJsonResponseAsync<T>(string url)
        {
            string jsonResponse = await GetApiResponseAsync(url);
            if (jsonResponse != null)
            {
                return JsonSerializer.Deserialize<T>(jsonResponse);
            }
            return default;
        }

    }
}