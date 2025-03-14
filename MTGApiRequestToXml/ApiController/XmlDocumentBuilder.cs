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

        /// <summary>
        /// Serialize MappingTool object to XML
        /// </summary>
        /// <param name="mappingTool">Mapping object</param>
        /// <returns>xmlfile</returns>
        private XDocument SerializeToXml(MappingTool mappingTool)
        {
            //TODO:: if that exists...
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
                else if(prop.PropertyType == typeof(List<string>))
                {
                    XElement listElement = new XElement(prop.Name);
                    var list = (List<string>)prop.GetValue(mappingTool);
                    foreach (var item in list)
                    {
                        listElement.Add(new XElement("Item", item));
                    }
                    root.Add(listElement);
                }
                else
                {
                    root.Add(new XElement(prop.Name, prop.GetValue(mappingTool)));
                }
            }
            return new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        }

        public  MappingTool DeserializeFromXml <MappingTool>(string fileName) where MappingTool : new()
        {
            MappingTool obj = new MappingTool();
            string path = Path.Combine(folderPath, fileName);

            if (File.Exists(path))
            {
                XDocument doc = XDocument.Load(path);
                
                foreach (PropertyInfo prop in typeof(MappingTool).GetProperties())
                {
                    if (prop.PropertyType == typeof(Dictionary<string, string>))
                    {
                        var dict = new Dictionary<string, string>();
                        foreach (var attribute in doc.Root.Element(prop.Name).Elements("Attribute"))
                        {
                            dict[attribute.Attribute("Key").Value] = attribute.Attribute("Value").Value;
                        }
                        prop.SetValue(obj, dict);
                    }
                    else if (prop.PropertyType == typeof(List<string>))
                    {
                        var list = new List<string>();
                        foreach (var element in doc.Root.Element(prop.Name).Elements("Item"))
                        {
                            list.Add(element.Value);
                        }
                        prop.SetValue(obj, list);
                    }
                    else
                    {
                        prop.SetValue(obj, Convert.ChangeType(doc.Root.Element(prop.Name).Value, prop.PropertyType));
                    }
                }
                return obj;
            }
            else
            {

                Console.WriteLine("Error: File not found");
                return obj;
            }
                
        }
    }
}

