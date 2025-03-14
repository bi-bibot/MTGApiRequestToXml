using System.Xml;
using System.IO;
using System.Xml.Serialization;
using MTGApiRequestToXml.ApiController;
using System.Xml.Linq;
using System.Reflection;

namespace MTGApiRequestToXml.ApiController
{
    public class XmlDocumentBuilder
    {
        private string folderPath;
        public XmlDocumentBuilder(string folderPath)
        {
            this.folderPath = folderPath;
        }

        /// <returns></returns>
        public async Task GenerateXmlFile(MappingTool mappingTool)
        {
            //Before this i have to map the json file to xml
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);

                }
                XDocument doc = SerializeToXml(mappingTool);
                doc.Save(Path.Combine(folderPath, $"{mappingTool.name}.xml"));

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error generating XML file: {e.Message}");
            }
        }

        private XDocument SerializeToXml(MappingTool mappingTool)
        {
            XElement root = new XElement(typeof(MappingTool).Name);
            foreach (PropertyInfo prop in typeof(MappingTool).GetProperties())
            {
                if (prop.PropertyType == typeof(Dictionary<string, string>))
                {
                    XElement dictElement = new XElement(prop.Name);
                    var dict = (Dictionary<string, string>)prop.GetValue(mappingTool);
                    foreach (var kvp in dict)
                    {
                        dictElement.Add(new XElement("Attribute",
                            new XAttribute("Key", kvp.Key),
                            new XAttribute("Value", kvp.Value)));
                    }
                    root.Add(dictElement);
                }
                else
                {
                    root.Add(new XElement(prop.Name, prop.GetValue(mappingTool)));
                }
            }
            return new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        }
    }
}
