﻿using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.ApiController
{

    public class SryFallApiRequest
    {

        public SryFallApiRequest()
        {
            //
        }

        public async Task<string> GetResponseFromApiAsync(string goodcardName)
        {
            // Ensure TLS 1.2 is used (for older .NET Framework versions)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var client = new HttpClient())
            {
                // Set the required headers
                client.DefaultRequestHeaders.Add("User-Agent", "MTGApiRequestToXml (github.com/bi-bibot)");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                // Always use HTTPS in the URL
                var url = $"https://api.scryfall.com/cards/named?exact={goodcardName}";

                // Send the GET request
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // The response content is UTF-8 encoded by default
                    var data = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(data);
                    return data;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }
            }
        }
    }
}
