using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using MTGApiRequestToXml.ApiController;
using MTGApiRequestToXml.tool;


public class Program
{
    public static async Task Main()
    {
        SryFallApiRequest sryFallApiRequest = new SryFallApiRequest();
        JsonExtractorTool jsonExtractorTool;
        MappingTool mappingTool;

        string cardName = "Edgar Markov";
        cardName = RegExTool.FormatCardName(cardName);
        var jsonResponse = await sryFallApiRequest.GetResponseFromApiAsync(cardName);

        if(jsonResponse != null)
        {
            jsonExtractorTool = new JsonExtractorTool(jsonResponse);
            mappingTool = jsonExtractorTool.map();
            XmlDocumentBuilder xmlDocumentBuilder = new XmlDocumentBuilder($"C:\\Users\\BATSAIKHAN_0001\\source\\repos\\MTGApiRequestToXml\\MTGApiRequestToXml\\XmlFiles");
            await xmlDocumentBuilder.GenerateXmlFile(mappingTool);

        }
        else
        {
            Console.WriteLine("Error: No response from Scryfall API");
        }

        

    }
}
