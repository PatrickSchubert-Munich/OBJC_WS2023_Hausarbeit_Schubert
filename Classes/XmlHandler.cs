using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using Werkzeugverleih.Interfaces;

namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// Serializes and dezerializes xml files
    /// </summary>
    /// <typeparam name="T">only classes</typeparam>
    public class XmlHandler<T> : IStorageHandler where T : class
    {
        /// <summary>
        /// Serialize objects to xml
        /// </summary>
        /// <typeparam name="T">Datatype of the object</typeparam>
        /// <param name="FileName">Xml-file full filepath with filename and extension</param>
        /// <param name="objectToSerialize">Object which should be serialized</param>
        public void SerializeXmlObject<T>(string FileName, T objectToSerialize)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamWriter writer = new StreamWriter(FileName))
                {
                    // Serialize data to xml
                    serializer.Serialize(writer, objectToSerialize);
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. File not found.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Input/Output error.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        /// <summary>
        /// Serialize list of objects to xml
        /// </summary>
        /// <typeparam name="T">Datatype of the object</typeparam>
        /// <param name="FileName">Xml-file full filepath with filename and extension</param>
        /// <param name="objectToSerialize">List of Objects which should be serialized</param>
        public void SerializeXmlListOfObjects<T>(string FileName, List<T> objectToSerialize)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (StreamWriter writer = new StreamWriter(FileName))
                {
                    // Serialize data to xml
                    serializer.Serialize(writer, objectToSerialize);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. File not found.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Input/Output error.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        /// <summary>
        /// Deserialize xml-file data
        /// </summary>
        /// <typeparam name="T">Datatype of the object</typeparam>
        /// <param name="FileName">Xml-file full filepath with filename and extension</param>
        /// <returns>Deserialized Objects</returns>
        public T DeserializeXmlObjects<T>(string FileName)
        {
            if (!File.Exists(FileName))
            {
                return default;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(FileName))
            {
                // Call of deserialize-method and casting the return value
                return (T)serializer.Deserialize(reader);
            }
        }

        /// <summary>
        /// Represents an XML-Document Object (whole XML-Document)
        /// </summary>
        /// <param name="FileName">Xml-file full filepath with filename and extension</param>
        /// <returns>Whole content of XML Document</returns>
        public XDocument XmlDoc(string FileName)
        {
            // Get Content (objects) from XML
            XDocument Xdoc = new XDocument();

            // Read out the content of XML-File
            var xmlContent = File.ReadAllText(FileName, Encoding.UTF8);
            var finalContent = XDocument.Parse(xmlContent);

            return finalContent;
        }
    }

}
