using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MTGApiRequestToXml.Domain;

namespace MTGApiRequestToXml.Domain
{
    /// <summary>
    /// CardMapper class for mapping json to object
    /// </summary>
    public class CardMapper : AttributeBaseClass
    {
        /// <summary>
        /// Creates constructor
        /// </summary>
        public CardMapper(AttributeBaseClass attributeBaseClass)
        {
            folderPath = Path.Combine(attributeBaseClass.folderPath, "Asset","XmlFiles");
        }

        public Card DeserializeFromXml<Card>(string fileName) where Card : new()
        {
            Card obj = new Card();
            string path = Path.Combine(this.folderPath, fileName);

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
