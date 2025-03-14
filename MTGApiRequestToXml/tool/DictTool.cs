using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.tool
{
    /// <summary>
    /// DictTool class for handling dictionary
    /// </summary>
    public class DictTool
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DictTool() { }

        /// <summary>
        /// gets Set of cards to just list of cards
        /// </summary>
        /// <param name="cardlists">list of cards</param>
        /// <returns></returns>
        public static List<string> FlatCards(Dictionary<string, List<string>> cardlists)
        {
            List<string> flatCards = new List<string>();
            foreach (KeyValuePair<string, List<string>> cardlist in cardlists)
            {
                foreach (string card in cardlist.Value)
                {
                    flatCards.Add(card);
                }
            }
            return flatCards;
        }
    }
}
