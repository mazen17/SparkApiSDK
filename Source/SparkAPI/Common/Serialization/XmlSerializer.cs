using System.IO;
using System.Xml.Serialization;

namespace SparkAPI.Common.Serialization
{

    /// <summary>
    /// Provides generic methods to save and load a serializable object to and from XML 
    /// </summary>
    /// <typeparam name="T">Object type to be serialized</typeparam>    
    public class XmlSerializer<T> where T : class, new()
    {

        /// <summary>
        /// Reads and deserializes object from specified xml data text
        /// </summary>
        /// <param name="xml">XML text</param>
        public static T DeserializeFromString(string xml)
        {
            T result;
            if (string.IsNullOrEmpty(xml))
            {
                result = new T();
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (StringReader reader = new StringReader(xml))
                {
                    result = (T)serializer.Deserialize(reader);
                }
            }
            return result;
        }

        /// <summary>
        /// Reads and deserializes object from specified file name
        /// </summary>
        /// <param name="fileName">Schema list file name</param>
        public static T DeserializeFromFile(string fileName)
        {
            T result = null;
            if (System.IO.File.Exists(fileName))
            {
                string xmlData = System.IO.File.ReadAllText(fileName);
                result = DeserializeFromString(xmlData);
            }
            return result;
        }

        /// <summary>
        /// Serializes and saves object to specified file name
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fileName"></param>
        public static void SerializeToFile(T item, string fileName)
        {
            System.IO.FileStream fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            System.Xml.Serialization.XmlSerializer sr = new System.Xml.Serialization.XmlSerializer(item.GetType());
            sr.Serialize(fileStream, item);
            fileStream.Close();
        }

        /// <summary>
        /// Serializes and saves object to specified file name
        /// </summary>
        /// <param name="item"></param>
        public static string SerializeToString(T item)
        {
            StringWriter writer = new StringWriter();
            System.Xml.Serialization.XmlSerializer sr = new System.Xml.Serialization.XmlSerializer(item.GetType());
            sr.Serialize(writer, item);
            return writer.ToString();
        }

    }
}
