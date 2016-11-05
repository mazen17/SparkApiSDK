using System;
using System.Diagnostics;

namespace SparkAPI.Common.Logging
{

    /// <summary>
    /// 
    /// </summary>
    public class LogDefinition : ILogDefinition
    {

        /// <summary>Log definition key</summary>
        public string Key { get; private set; }

        /// <summary>Log file name</summary>
        public string Filename { get; private set; }

        /// <summary>Log entries are also written to console</summary>
        public bool WriteToConsole { get; set; }

        /// <summary>Line header format</summary>
        public string HeaderFormat { get; set; }

        /// <summary>
        /// LogDefinition constructor
        /// </summary>
        /// <param name="key">Log indentifier</param>
        /// <param name="filename">Log file name</param>
        public LogDefinition(string key, string filename)
        {
            Key = key;
            Filename = filename;
            WriteToConsole = true;
            HeaderFormat = "yyyy-MM-dd HH:mm:ss.fff\t";
        }

        /// <summary>
        /// Write text to log
        /// </summary>
        public void Write(string text)
        {
            string header = DateTime.Now.ToString(HeaderFormat);
            string outputText = header + Key + "\t" + text;
            FileBufferList.Instance[Filename].Add(outputText);
            if (WriteToConsole) Console.WriteLine(outputText);
        }

        /// <summary>
        /// Write text with a trailing CRLF to log
        /// </summary>
        public void WriteLine(string text)
        {
            Write(text + "\r\n");
        }

        /// <summary>
        /// Returns TRUE if the log file exists
        /// </summary>
        public bool Exists
        {
            get
            {
                return System.IO.File.Exists(Filename);
            }
        }

    }
}
