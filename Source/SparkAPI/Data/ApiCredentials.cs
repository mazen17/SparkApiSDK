using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SparkAPI.Data
{
    
    /// <summary>
    /// Security credentials required to access Spark API
    /// </summary>
    [Serializable]
    public class ApiCredentials
    {

        private const string CONFIG_FILENAME = "config.xml";

        /// <summary>Spark username</summary>
        [XmlAttribute]
        public string Username { get; set; }

        /// <summary>Spark password</summary>
        [XmlAttribute]
        public string Password { get; set; }


        /// <summary>
        /// Default configuration directory for storing credentials
        /// </summary>
        private static string ConfigurationDirectory
        {
            get 
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\SparkAPI\";            
            }            
        }

        /// <summary>
        /// Load credentials from file using default filename
        /// </summary>
        public static ApiCredentials LoadFromFile()
        {
            return LoadFromFile(ConfigurationDirectory + CONFIG_FILENAME);
        }

        /// <summary>
        /// Load credentials from file using specified filename
        /// </summary>
        /// <param name="fileName">Configuration filename</param>
        public static ApiCredentials LoadFromFile(string fileName)
        {
            ApiCredentials result = null;
            if (System.IO.File.Exists(fileName)) return Common.Serialization.XmlSerializer<ApiCredentials>.DeserializeFromFile(fileName);
            return result;
        }

        /// <summary>
        /// Save credentials to file using default filename
        /// </summary>
        public void SaveToFile()
        {
            SaveToFile(ConfigurationDirectory + CONFIG_FILENAME);
        }

        /// <summary>
        /// Save credentials to specified filename
        /// </summary>
        /// <param name="fileName">File name</param>
        public void SaveToFile(string fileName)
        {
            string folderName = System.IO.Path.GetDirectoryName(fileName);
            if (!System.IO.Directory.Exists(folderName)) System.IO.Directory.CreateDirectory(folderName);
            Common.Serialization.XmlSerializer<ApiCredentials>.SerializeToFile(this, fileName);
        }

        /// <summary>
        /// Checks that Spark login credentials are valid
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            bool result = true;
            if (ApiControl.Instance.ConnectionStatus == SparkConnectionStatus.Disconnected)
            {
                ApiControl.Instance.Connect();
                result = ApiControl.Instance.Connect();
            }
            return result;
        }


    }

}
