using MTGApiRequestToXml.tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MTGApiRequestToXml.ApiController
{
    public class XmlDocumentBuilder
    {
        private string folderPath;
        public XmlDocumentBuilder(string folderPath)
        {
            this.folderPath = folderPath;
        }

        public async void BuildXmlDocument(Dictionary<string, List<string>> dict)
        {
            foreach (var kvp in dict)
            {
                // Create directory for each key
                string path = Path.Combine(folderPath, kvp.Key);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var value in kvp.Value)
                {
                    string filePath = Path.Combine(path, $"{value}.xml");
                    if (File.Exists(filePath))
                    {
                        File.Delete(path);
                    }
                    string FormattedValue = RegExTool.FormatCardName(value);
                    await GenerateXmlFile(filePath, FormattedValue);

                }
            }
        }

        /// <summary>
        /// Generates XML file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private async Task GenerateXmlFile(string filePath, string value)
        {
            //Before this i have to map the json file to xml
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                string url = $"https://api.scryfall.com/cards/named?exact={value}";

                XmlElement parentElement = xmlDoc.CreateElement("Root");
                xmlDoc.AppendChild(parentElement);
                XmlElement childElement = xmlDoc.CreateElement("Item");
                childElement.InnerText = value;
                parentElement.AppendChild(childElement);
                xmlDoc.Save(filePath);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error generating XML file: {e.Message}");
            }
        }
        //MapCardJsonToXml

    }
}
