using System;
using System.Text;
using System.Diagnostics;

namespace SparkAPI.Common.Logging
{

    /// <summary>
    /// 
    /// </summary>
    public class FileBuffer : System.Collections.Queue
    {

        /// <summary>Specifies whether log should overwrite existing log or append to it</summary>
        public enum WriteStyle
        {
            Append = 0,
            Overwrite = 1
        }

        /// <summary>Destination output file name</summary>
        public string OutputFileName { get; private set; }

        /// <summary>Specifies whether log should overwrite existing log or append to it</summary>
        public WriteStyle OutputStyle { get; set; }

        /// <summary>
        /// FileWriteBuffer constructor
        /// </summary>
        /// <param name="outputFileName">Destination output file name</param>
        public FileBuffer(string outputFileName)
        {
            OutputFileName = outputFileName;
            OutputStyle = WriteStyle.Append;
        }

        /// <summary>
        /// FileWriteBuffer constructor
        /// </summary>
        /// <param name="outputFileName">Destination output file name</param>
        /// <param name="outputStyle">Output write style for the buffer</param>
        public FileBuffer(string outputFileName, WriteStyle outputStyle)
        {
            OutputFileName = outputFileName;
            OutputStyle = outputStyle;
        }

        /// <summary>Returns TRUE when buffer contains data</summary>
        public bool HasData { get { return (this.Count > 0); } }

        /// <summary>
        /// Add output text to the buffer queue
        /// </summary>
        public void Add(string text)
        {
            lock (this.SyncRoot)
            {
                this.Enqueue(text);
            }
        }

        /// <summary>
        /// Add output text with terminating CRLF to the buffer queue
        /// </summary>
        public void AddLine(string text)
        {
            Add(text + "\r\n");
        }

        /// <summary>
        /// Writes buffered text to file
        /// </summary>
        /// <returns></returns>
        public bool WriteToFile()
        {

            //Write text to output file
            bool result = true;
            System.IO.StreamWriter file = null;
            try
            {

                //Delete existing file if required
                if (OutputStyle == WriteStyle.Overwrite)
                {
                    if (System.IO.File.Exists(OutputFileName))
                    {
                        try
                        {
                            System.IO.File.Delete(OutputFileName);
                        }
                        catch { }
                    }
                }

                //Create output folder if required
                string outputFolder = System.IO.Path.GetDirectoryName(OutputFileName);
                if (!System.IO.Directory.Exists(outputFolder)) System.IO.Directory.CreateDirectory(outputFolder);

                //Open file
                file = new System.IO.StreamWriter(OutputFileName, true);

                //Get sync locked queue
                System.Collections.Queue syncQueue = Synchronized(this);

                //Copy queue to array
                object[] textList = syncQueue.ToArray();

                //Construct output text
                System.Text.StringBuilder outputText = new StringBuilder();
                if (OutputStyle == WriteStyle.Append)
                {

                    //Add each text string in queue
                    foreach (string textEntry in textList)
                    {
                        outputText.Append(textEntry);
                    }

                }
                else
                {

                    //Add last text string in queue
                    outputText.Append(textList[textList.Length - 1]);

                }

                //Write to file
                file.Write(outputText.ToString());

                //Empty array
                syncQueue.Clear();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                result = false;
            }
            finally
            {
                if (file != null) file.Dispose();
            }
            return result;

        }

    }
}
