using MTGApiRequestToXml.Common.Utils;
using MTGApiRequestToXml.Data;
using MTGApiRequestToXml.Data.Entities;
using MTGApiRequestToXml.Domain;
using MTGApiRequestToXml.Usecases;

namespace MTGApiRequestToXml
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        public AttributeBaseClass attributeBaseClass { get; set; }

        public Program()
        {
            attributeBaseClass = new AttributeBaseClass();
            attributeBaseClass.folderPath = Directory.Exists(@"C:\Users\BATSAIKHAN_0001\source\repos\MTGApiRequestToXml\MTGApiRequestToXml") ?
                                            @"C:\Users\BATSAIKHAN_0001\source\repos\MTGApiRequestToXml\MTGApiRequestToXml" : throw new Exception("Application Directory not found!");

        }
        /// <summary>
        /// Main method
        /// </summary>
        /// <returns></returns>
        public static async Task Main()
        {
            //TODO : Need to pass the current folder Directory



            Program program = new Program();
            await program.GenerateRandomCommanderCards(10);
            //await program.Serialize("Atraxa, Praetors' Voice");
            //await program.Deserialize($"Edgar Markov.xml");

        }

        /// <summary>
        /// Tests wether Image is downloading
        /// </summary>
        /// <returns><Task/returns>
        public async Task<Boolean> Imagedownload(string FileName)
        {
            Card card = new Card();
            //XmlDocumentBuilder xmlDocumentBuilder = new XmlDocumentBuilder($"C:\\Users\\BATSAIKHAN_0001\\source\\repos\\MTGApiRequestToXml\\MTGApiRequestToXml\\XmlFiles");
            CardMapper cardMapper = new CardMapper(attributeBaseClass);

            card = cardMapper.DeserializeFromXml<Card>(FileName);

            if (card == null)
            {
                Console.WriteLine("Error: No response from Scryfall API");
                return false;
            }

            ImageDownload imageDownload = new ImageDownload(attributeBaseClass);
            ImageInfo imageInfo = new ImageInfo(card.image_uris["normal"], card.name);

            await imageDownload.Run(imageInfo);
            return true;
        }

        public static void Load()
        {


        }

        public async Task<Card> GenerateRandomCommanderCards(int count)
        {
            Card card = new Card()  ;
            SryfallAPI sryFallApiRequest = new SryfallAPI();
            Program program = new Program();
            for (int i = 0; i < count; i++)
            {

                string response = await sryFallApiRequest.getRandomCommanderAsync();
                if (response == null)
                {
                    //No response messege
                    return card;
                }

                MapCard getCard = new MapCard(response);
                card = getCard.Run();
                XmlDocumentBuilder xmlDocumentBuilder = new XmlDocumentBuilder(attributeBaseClass);
                await xmlDocumentBuilder.GenerateXmlFile(card);

                if (card == null && card.image_uris == null)
                {
                    //Image not found
                    return card;
                }

                Boolean isDownloaded = await program.Imagedownload($"{RegExUtil.FormatCardName(card.name)}.xml");
                if (isDownloaded == false)
                {
                    //Image not downloaded
                    return card;
                }
            }

            return card;
        }

        /// <summary>
        /// Saves card data as XML file
        /// </summary>
        /// <returns>Card</returns>
        public async Task<Card> Serialize(string cardName)
        {
            SryfallAPI sryFallApiRequest = new SryfallAPI();
            MapCard getCard;
            Card card;

            //string cardName = "Atraxa, Praetors' Voice";
            cardName = RegExUtil.FormatCardName(cardName);
            var jsonResponse = await sryFallApiRequest.GetResponseFromApiAsync(cardName);

            if (jsonResponse != null)
            {
                getCard = new MapCard(jsonResponse);
                card = getCard.Run();

                //XmlDocumentBuilder xmlDocumentBuilder = new XmlDocumentBuilder($"C:\\Users\\BATSAIKHAN_0001\\source\\repos\\MTGApiRequestToXml\\MTGApiRequestToXml\\XmlFiles");
                XmlDocumentBuilder xmlDocumentBuilder = new XmlDocumentBuilder(attributeBaseClass);
                await xmlDocumentBuilder.GenerateXmlFile(card);
                return card;
            }
            else
            {
                Console.WriteLine("Error: No response from Scryfall API");
                return null;
            }
        }

        /// <summary>
        /// Test Deserialize method
        /// </summary>
        /// <returns></returns>
        public async Task Deserialize(string fileName)
        {
            Card card;

            XmlDocumentBuilder xmlDocumentBuilder = new XmlDocumentBuilder(attributeBaseClass);

            card = xmlDocumentBuilder.DeserializeFromXml<Card>(fileName);

        }
    }
}
