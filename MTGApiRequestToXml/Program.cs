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


    }
}
