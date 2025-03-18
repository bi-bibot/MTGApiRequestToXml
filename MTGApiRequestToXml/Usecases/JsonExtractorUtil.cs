using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MTGApiRequestToXml.Domain;

namespace MTGApiRequestToXml.Usecases
{
    public class JsonExtractorUtil
    {
        /// <summary>
        /// jsonResponse
        /// </summary>
        public string jsonResponse { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jsonResponse"></param>
        public JsonExtractorUtil(string jsonResponse)
        {
            this.jsonResponse = jsonResponse;
        }

        /// <summary>
        /// Map json to object
        /// </summary>
        /// <returns>returns mapped table</returns>
        /*
        public MappingTool map()
        {
            CardMapper cardMapper = CardMapper.DeserializeFromXml<Card>("$\"Markov Baron.xml\"");
            MappingTool middleware = JsonConvert.DeserializeObject<MappingTool>(jsonResponse);

            return middleware;
        }
        */
    }
}
