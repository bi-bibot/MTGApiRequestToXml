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
        /// <summary>
        /// jsonResponse
        /// </summary>
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
        /// Map json to object
        /// </summary>
        /// <returns>returns mapped table</returns>
        public MappingTool map()
        {

            MappingTool middleware = JsonConvert.DeserializeObject<MappingTool>(jsonResponse);

            return middleware;
        }
    }
}
