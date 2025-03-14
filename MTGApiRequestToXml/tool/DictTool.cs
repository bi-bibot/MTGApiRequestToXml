using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.tool
{
    public class DictTool
    {
        public DictTool() { }

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
