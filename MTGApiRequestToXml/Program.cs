using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using MTGApiRequestToXml.Data;
using MTGApiRequestToXml.Usecases;
using MTGApiRequestToXml.Domain;
using MTGApiRequestToXml.Domain.Entities;
using MTGApiRequestToXml.Common.Utils;

namespace MTGApiRequestToXml
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <returns></returns>
        public static async Task Main()
        {
            //TODO : Need to pass the current folder Directory

            AttributeBaseClass attributeBaseClass = new AttributeBaseClass();
            attributeBaseClass.folderPath = @"C:\Users\BATSAIKHAN_0001\source\repos\MTGApiRequestToXml\MTGApiRequestToXml";

            //await TestSerialize(attributeBaseClass);
            await TestImagedownload(attributeBaseClass);
            //await TestDeserialize();
            


        }

        /// <summary>
        /// Tests wether Image is downloading
        /// </summary>
        /// <returns><Task/returns>
        private static async Task TestImagedownload(AttributeBaseClass attributeBase)
        {
            Card card = new Card();
            //XmlDocumentBuilder xmlDocumentBuilder = new XmlDocumentBuilder($"C:\\Users\\BATSAIKHAN_0001\\source\\repos\\MTGApiRequestToXml\\MTGApiRequestToXml\\XmlFiles");
            CardMapper cardMapper = new CardMapper(attributeBase);

            card = cardMapper.DeserializeFromXml<Card>($"atraxa,-praetors'-voice.xml");
            
            if(card == null)
            {
                Console.WriteLine("Error: No response from Scryfall API");
                return;
            }

            ImageDownload imageDownload = new ImageDownload(attributeBase);
            ImageInfo imageInfo = new ImageInfo(card.image_uris["normal"], card.name);

            await imageDownload.Run(imageInfo);
        }

        public static void Load()
        {


        }

        /// <summary>
        /// Test Serialize method
        /// </summary>
        /// <returns></returns>
        private static async Task TestSerialize(AttributeBaseClass attributeBaseClass)
        {
            SryfallAPI sryFallApiRequest = new SryfallAPI();
            MapCard getCard;
            Card card;

            string cardName = "Atraxa, Praetors' Voice";
            cardName = RegExUtil.FormatCardName(cardName);
            var jsonResponse = await sryFallApiRequest.GetResponseFromApiAsync(cardName);

            if (jsonResponse != null)
            {
                getCard = new MapCard(jsonResponse);
                card = getCard.Run();

                //XmlDocumentBuilder xmlDocumentBuilder = new XmlDocumentBuilder($"C:\\Users\\BATSAIKHAN_0001\\source\\repos\\MTGApiRequestToXml\\MTGApiRequestToXml\\XmlFiles");
                XmlDocumentBuilder xmlDocumentBuilder = new XmlDocumentBuilder(attributeBaseClass);
                await xmlDocumentBuilder.GenerateXmlFile(card);

            }
            else
            {
                Console.WriteLine("Error: No response from Scryfall API");
            }
        }

        /// <summary>
        /// Test Deserialize method
        /// </summary>
        /// <returns></returns>
        private static async Task TestDeserialize(AttributeBaseClass attributeBaseClass)
        {
            Card card;

            XmlDocumentBuilder xmlDocumentBuilder = new XmlDocumentBuilder(attributeBaseClass);

            card = xmlDocumentBuilder.DeserializeFromXml<Card>($"Edgar Markov.xml");

        }
    }
}
