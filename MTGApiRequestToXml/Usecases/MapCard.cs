using MTGApiRequestToXml.Domain.Entities;
using MTGApiRequestToXml.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.Usecases
{
    public class MapCard
    {
        /// <summary>
        /// jsonResponse
        /// </summary>
        public string jsonResponse { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jsonResponse"></param>
        public MapCard(string jsonResponse)
        {
            this.jsonResponse = jsonResponse;
        }

        public Card Run()
        {
            Card card = JsonConvert.DeserializeObject<Card>(jsonResponse);

            return card;
        }
    }
}

