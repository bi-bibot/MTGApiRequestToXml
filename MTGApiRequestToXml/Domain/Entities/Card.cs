using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.Domain.Entities
{
    /// <summary>
    /// Card class for mapping json to object
    /// </summary>
    public class Card
    {
        /// <summary>
        /// id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// image_uris
        /// </summary>
        public Dictionary<string, string> image_uris { get; set; }

        /// <summary>
        /// mana_cost
        /// </summary>
        public string mana_cost { get; set; }

        /// <summary>
        /// cmc
        /// </summary>
        public double cmc { get; set; }

        /// <summary>
        /// type_line
        /// </summary>
        public string type_line { get; set; }

        /// <summary>
        /// oracle_text
        /// </summary>
        public string oracle_text { get; set; }

        /// <summary>
        /// colors
        /// </summary>
        public List<string> colors { get; set; }

        /// <summary>
        /// color_identity
        /// </summary>
        public List<string> color_identity { get; set; }

        /// <summary>
        /// Creates constructor
        /// </summary>        
        public Card() 
        {
            // Empty constructor
        }
    }
}
