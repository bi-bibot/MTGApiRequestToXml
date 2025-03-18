using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using MTGApiRequestToXml.Domain;
using MTGApiRequestToXml.Domain.Entities;
using MTGApiRequestToXml.Common.Utils;

namespace MTGApiRequestToXml.Usecases
{
    public class XmlDocumentBuilder : AttributeBaseClass
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public XmlDocumentBuilder(AttributeBaseClass attributeBaseClass)
        {
            folderPath = attributeBaseClass.folderPath;
        }

        /// <returns></returns>
        public async Task GenerateXmlFile(Card card)
        {
            //Before this i have to map the json file to xml
            try
            {
                string path = Path.Combine(this.folderPath, "Asset", "XmlFiles");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(folderPath);

                }
                XDocument doc = SerializeToXml(card);

                string filePath = Path.Combine(path, $"{RegExUtil.FormatCardName(card.name)}.xml");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                doc.Save(filePath);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error generating XML file: {e.Message}");
            }
        }

        /// <summary>
        /// Serialize MappingTool object to XML
        /// </summary>
        /// <param name="card">Mapping object</param>
        /// <returns>xmlfile</returns>
        private XDocument SerializeToXml(Card card)
        {
            //TODO:: if that exists...
            XElement root = new XElement(typeof(Card).Name);
            foreach (PropertyInfo prop in typeof(Card).GetProperties())
            {
                if (prop.PropertyType == typeof(Dictionary<string, string>))
                {
                    XElement dictElement = new XElement(prop.Name);
                    var dict = (Dictionary<string, string>)prop.GetValue(card);
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
                    var list = (List<string>)prop.GetValue(card);
                    foreach (var item in list)
                    {
                        listElement.Add(new XElement("Item", item));
                    }
                    root.Add(listElement);
                }
                else
                {
                    root.Add(new XElement(prop.Name, prop.GetValue(card)));
                }
            }
            return new XDocument(new XDeclaration("1.0", "utf-8", "yes"), root);
        }

        public  Card DeserializeFromXml <Card>(string fileName) where Card : new()
        {
            Card obj = new Card();
            string path = Path.Combine(folderPath, fileName);

            if (File.Exists(path))
            {
                XDocument doc = XDocument.Load(path);
                
                foreach (PropertyInfo prop in typeof(Card).GetProperties())
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

