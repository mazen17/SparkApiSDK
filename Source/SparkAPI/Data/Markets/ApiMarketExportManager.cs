using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SparkAPI.Common.Diagnostics;
using SparkAPI.Common.Events;

namespace SparkAPI.Data.Markets
{
    /// <summary>
    /// Manages the download and export to file of the current days event data for the entire market via the Spark API.
    /// </summary>
    /// <remarks>
    /// Due to the way Spark represents Chi-X internally, requesting 'ASX' will return CXA as well as ASX stocks. 'ASX' will 
    /// also return ASX warrants. 
    /// </remarks>
    public class ApiMarketEventExportManager
    {

        /// <summary>Security exchange</summary>
        public string Exchange { get; private set; }

        /// <summary>Primary data output folder</summary>
        public string DataFolder { get; private set; }

        /// <summary>Executed when export progress is updated</summary>
        public event EventHandler<GenericEventArgs<string>> OnProgressUpdate;

        private string _eventFileName;

        private StreamWriter _streamWriter;

        private int _eventCount;

        /// <summary>
        /// ApiEventExportManager constructor
        /// </summary>
        /// <param name="exchange">Security exchange</param>
        /// <param name="dataFolder">Primary data output folder</param>
        public ApiMarketEventExportManager(string exchange, string dataFolder)
        {
            Exchange = exchange;
            DataFolder = dataFolder;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Export()
        {

            //Determine output folder
            string folderName = DataFolder;

            //Determine start date
            DateTime date = DateTime.Today;

            //Check if date is potential trading day
            if ((date.DayOfWeek != DayOfWeek.Saturday) && (date.DayOfWeek != DayOfWeek.Sunday))
            {

                //Determine output event file name
                _eventFileName = folderName + @"\" + Exchange + "_Event_" + date.ToString("yyyyMMdd") + ".txt";

                //Request file if not already downloaded
                string progressText = Exchange + "\t" + date.ToString("yyyyMMdd") + "\t";
                if (!System.IO.File.Exists(_eventFileName))
                {

                    //Request data
                    Benchmark executeBench = new Benchmark("");
                    try
                    {

                        //Execute query
                        ApiMarketEventFeed query = new ApiMarketEventFeed(Exchange);
                        query.OnEvent += query_OnEvent;
                        query.Execute();

                        if (_streamWriter != null) _streamWriter.Close();
                        progressText += "Events:\t" + _eventCount + "\tRequest Time:\t" + executeBench.Elapsed;

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("ERROR: " + ex.Message);
                        progressText += ex.Message;
                    }

                }
                else
                {

                    //File already downloaded
                    progressText += "\tFile already downloaded";

                }

                //Write progress to console
                if (OnProgressUpdate != null) OnProgressUpdate(this, new GenericEventArgs<string>(DateTime.Now, progressText));

            }

        }

        /// <summary>
        /// Executed when an event is read from the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void query_OnEvent(object sender, EventFeedArgs e)
        {
            if (_streamWriter == null) _streamWriter = File.AppendText(_eventFileName);
            _streamWriter.WriteLine(ApiEventReaderWriter.ToFileString(e.Event));
            _eventCount++;
        }
        

    }

}
