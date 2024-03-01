using System.Xml.Serialization;

namespace Werkzeugverleih
{
    public class XmlHandler
    {

        // public Serialize method
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

        // public Serialize method
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

        // public Dezerialize method
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

    }

}
