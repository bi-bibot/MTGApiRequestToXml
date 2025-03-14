using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.ApiController
{
    /// <summary>
    /// Handling Api request of Edhrec
    /// </summary>
    public class EdhrecApiTool
    {
        /// <summary>
        /// HttpClient
        /// </summary>
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Get response from Edhrec API
        /// </summary>
        /// <param name="goodcardName">RegEx-ed good card name</param>
        /// <returns>returns json in string </returns>
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
    }
}