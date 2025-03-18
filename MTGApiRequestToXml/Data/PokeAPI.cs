using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.Data
{
    public class PokeAPI
    {
        private static readonly HttpClient client = new HttpClient();
        public class Pokemon
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public async Task<string> GetResponseFromApiAsync(string goodcardName)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://pokeapi.co/api/v2/pokemon{goodcardName}.json");
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

    }
}
