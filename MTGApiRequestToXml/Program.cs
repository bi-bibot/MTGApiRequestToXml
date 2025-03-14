using System;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using MTGApiRequestToXml.ApiController;
using MTGApiRequestToXml.tool;


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

        await TestDeserialize();


    }

    /// <summary>
    /// Test Serialize method
    /// </summary>
    /// <returns></returns>
    private static async Task TestSerialize()
    {
        SryFallApiRequest sryFallApiRequest = new SryFallApiRequest();
        JsonExtractorTool jsonExtractorTool;
        MappingTool mappingTool;

        string cardName = "Edgar Markov";
        cardName = RegExTool.FormatCardName(cardName);
        var jsonResponse = await sryFallApiRequest.GetResponseFromApiAsync(cardName);

        if (jsonResponse != null)
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

    /// <summary>
    /// Test Deserialize method
    /// </summary>
    /// <returns></returns>
    private static async Task TestDeserialize()
    {
        MappingTool mappingTool;
        XmlDocumentBuilder xmlDocumentBuilder = new XmlDocumentBuilder($"C:\\Users\\BATSAIKHAN_0001\\source\\repos\\MTGApiRequestToXml\\MTGApiRequestToXml\\XmlFiles");

        mappingTool = xmlDocumentBuilder.DeserializeFromXml<MappingTool>($"Edgar Markov.xml");

        Console.Write(mappingTool.ToString());
    }
}
