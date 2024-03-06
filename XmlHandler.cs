using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Werkzeugverleih
{
    public class XmlHandler
    {
        /*
* Here are all static methods, so we cannot instantiate objects from this class.
* The advantage is, that we can call these methods in any context i.e. classes
* or methods from classes and so on.
*/

        /// <summary>
        /// Serialize XML-Data: Object
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="FileName">Name of the XML-file</param>
        /// <param name="objectToSerialize">Object which should be serialized</param>
        public static void SerializeXmlObject<T>(string FileName, T objectToSerialize)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamWriter writer = new StreamWriter(FileName))
                {
                    // Serialize data to xml
                    serializer.Serialize(writer, objectToSerialize);
                }

                Console.WriteLine("Process finished...");
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
        /// Serialize XML-Data: List of Objects
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="FileName">Name of the XML-file</param>
        /// <param name="objectToSerialize">List of Objects which should be serialized</param>
        public static void SerializeXmlListOfObjects<T>(string FileName, List<T> objectToSerialize)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (StreamWriter writer = new StreamWriter(FileName))
                {
                    // Serialize data to xml
                    serializer.Serialize(writer, objectToSerialize);
                }
                Console.WriteLine("Serialize Process finished...");
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
        /// Deserialize XML-Data: Object
        /// </summary>
        /// <typeparam name="T">Type of Object</typeparam>
        /// <param name="FileName">Name of the XML-file</param>
        /// <returns>Deserialized Object</returns>
        public static T DeserializeXmlObjects<T>(string FileName)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StreamReader reader = new StreamReader(FileName))
                {
                    // Call of deserialize-method and casting the return value
                    return (T)serializer.Deserialize(reader)!;

                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. File not found.");
                return default!;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}. Input/Output error.");
                return default!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return default!;
            }


        }

        /// <summary>
        /// Represents an XML-Document Object (whole XML-Document)
        /// </summary>
        /// <param name="source">Joined FilePath and FileName</param>
        /// <returns>Whole content of XML Document</returns>
        public static XDocument XmlDoc(string source)
        {
            // Get Content (objects) from XML
            XDocument Xdoc = new XDocument();

            // Read out the content of XML-File
            var xmlContent = File.ReadAllText(source, Encoding.UTF8);
            var finalContent = XDocument.Parse(xmlContent);

            // Get User a message back
            Console.WriteLine("Serialize Process finished...");
            return finalContent;

        }

        /// <summary>
        /// Check, if empty XML File exists.
        /// If not exists, create once.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool CheckXmlFileExists(string source)
        {
            // Check, if File exists on Path, respectively System 
            if (!File.Exists(source))
            {
                if (CreateEmptyXmlFile(source))
                {
                    Console.WriteLine($"Empty XML file created at: {source}");

                    // Get User a message back
                    Console.WriteLine("XML-File created on System...");
                    return true;
                };
            }

            // Get User a message back
            Console.WriteLine("XML-File File already exists on System...");
            return true;
        }

        /// <summary>
        /// Create an empty Xml-File if not exists
        /// </summary>
        /// <param name="source">Joined FilePath and FileName</param>
        /// <returns>Flag if File is created</returns>
        public static bool CreateEmptyXmlFile(string source)
        {
            // Create an XmlWriter for writing the XML document
            using (XmlWriter xmlWriter = XmlWriter.Create(source))
            {
                // Start writing the XML document
                xmlWriter.WriteStartDocument();

                // Write the root element
                xmlWriter.WriteStartElement("ArrayOfCustomer");

                // End the root element
                xmlWriter.WriteEndElement();

                // End the XML document
                xmlWriter.WriteEndDocument();

                // Get User a message back
                Console.WriteLine($"Empty XML-File was created on System: {source}");
                return true;
            }
            return false;
        }

    }

}
