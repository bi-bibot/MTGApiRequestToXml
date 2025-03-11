using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.ApiController
{
    public class MappingTool
    {
        public string id { get; set; }
        public string name { get; set; }

        public Dictionary<string, string> image_uris { get; set; }

        public string mana_cost { get; set; }

        public double cmc { get; set; }

        public string type_line { get; set; }

        public string oracle_text { get; set; }

        public List<string> colors { get; set; }

        public List<string> color_identity { get; set; }


        public MappingTool()
        {
            Console.WriteLine("MappingTool constructor");
        }
    }
}
