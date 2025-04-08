using MTGApiRequestToXml.Data.Entities;
using MTGApiRequestToXml.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.Usecases
{
    public class CardFilterUtil : AttributeBaseClass
    {   
        /// <summary>
        /// Water
        /// </summary>
        public Boolean Water { get; set; }
        
        /// <summary>
        /// Fire
        /// </summary>
        public Boolean Fire { get; set; }

        /// <summary>
        /// White
        /// </summary>
        public Boolean White { get; set; }

        /// <summary>
        /// Black
        /// </summary>
        public Boolean Black { get; set; }

        /// <summary>
        /// Green
        /// </summary>
        public Boolean Green { get; set; }

        /// <summary>
        /// Multi color
        /// </summary>
        public Boolean Multi { get; set; }

        /// <summary>
        /// ImgFolderPath
        /// </summary>
        public String ImgFolderPath { get; set; }

        /// <summary>
        /// CardType
        /// </summary>
        public String CardType { get; set; }

        /// <summary>
        /// Creates Instance
        /// </summary>
        public CardFilterUtil(AttributeBaseClass attributeBase)
        {
            
            this.folderPath = attributeBase.folderPath;
           
            
            ImgFolderPath = Path.Combine(attributeBase.folderPath, "Asset", "Images");
        }
        public async Task<List<string>> Filter()
        {
            List<string> filteredFileNames = new List<string>();
            XmlDocumentBuilder xmlDocumentBuilder = new XmlDocumentBuilder(this);
            string XmlFolderPath = Path.Combine(folderPath, "Asset", "XmlFiles");

            string[] xmlFiles = Directory.GetFiles(XmlFolderPath);
            
            if (CardType == "None")
            {
                CardType = null;
            }

            foreach (string xmlFile in xmlFiles)
            {
                try
                {
                    Card card = xmlDocumentBuilder.DeserializeFromXml<Card>(xmlFile);

                    if (!string.IsNullOrEmpty(CardType) || Black || Water || White || Green || Fire)
                    {
                        if (string.IsNullOrEmpty(CardType))
                        {
                            if (MatchesColorIdentity(card))
                            {
                                filteredFileNames.Add(card.name);
                            }
                        }
                        else if (!Black && !Water && !White && !Green && !Fire)
                        {
                            if (card.type_line.Contains(CardType))
                            {
                                filteredFileNames.Add(card.name);
                            }
                        }
                        else
                        {
                            if (MatchesColorIdentity(card) && card.type_line.Contains(CardType))
                            {
                                filteredFileNames.Add(card.name);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {xmlFile}: {ex.Message}");
                }
            }

            return filteredFileNames;
        }

        private bool MatchesColorIdentity(Card card)
        {
            return (Black && card.mana_cost.Contains("B")) ||
                   (Water && card.mana_cost.Contains("U")) ||
                   (White && card.mana_cost.Contains("W")) ||
                   (Fire  && card.mana_cost.Contains("R")) ||
                   (Green && card.mana_cost.Contains("G"));
        }
    }
}
