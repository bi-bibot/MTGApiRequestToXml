using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MTGApiRequestToXml.ApiController;

namespace MTGApiRequestToXml.tool
{
    public class JsonExtractorTool
    {
        public string jsonResponse { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jsonResponse"></param>
        public JsonExtractorTool(string jsonResponse)
        {
            this.jsonResponse = jsonResponse;
        }

        /// <summary>
        public MappingTool map()
        {

            MappingTool middleware = JsonConvert.DeserializeObject<MappingTool>(jsonResponse);

            return middleware;
        }

        //TODO:: have to generalize
        public async Task<Dictionary<string, object>> getJsontoDict()
        {
            Dictionary<string, object> cardlists = new Dictionary<string, object>();

            using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
            {
                JsonElement root = doc.RootElement;

                foreach (JsonProperty property in root.EnumerateObject())
                {
                    Console.WriteLine($"{property.Name}: {property.Value}");

                    if (property.Value.ValueKind == JsonValueKind.Number)
                    {
                        cardlists.Add(property.Name, property.Value.GetDouble());
                    }
                    if (property.Value.ValueKind == JsonValueKind.String)
                    {
                        cardlists.Add(property.Name, property.Value.GetString());
                    }
                    if (property.Value.ValueKind == JsonValueKind.Array)
                    {
                        List<string> cardlist_cards = new List<string>();
                        foreach (JsonElement card in property.Value.EnumerateArray())
                        {
                            cardlist_cards.Add(card.GetProperty("name").GetString());
                        }
                        cardlists.Add(property.Name, cardlist_cards);
                    }
                }
            }

            return cardlists;
        }
    }
}
