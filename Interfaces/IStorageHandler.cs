using System.Xml.Linq;

namespace Werkzeugverleih.Interfaces
{
    /// <summary>
    /// Storage Management for serializing and dezerializing xml-files
    /// </summary>
    public interface IStorageHandler
    {
        void SerializeXmlObject<T>(string FileName, T objectToSerialize);
        void SerializeXmlListOfObjects<T>(string FileName, List<T> objectToSerialize);

        T DeserializeXmlObjects<T>(string FileName);
        XDocument XmlDoc(string source);
    }
}
