using System;
using System.Collections.Generic;
using System.Diagnostics;
using SparkAPI.Common.Diagnostics;
using SparkAPI.Common.Events;

namespace SparkAPI.Data.Securities
{
    /// <summary>
    /// ApiSecurityEventExportManager manages the download and export to file of historical event 
    /// data for a specified security and exchange via the Spark API.
    /// </summary>
    public class ApiSecurityEventExportManager
    {

        /// <summary>Security symbol</summary>
        public string Symbol { get; private set; }

        /// <summary>Security exchange</summary>
        public string Exchange { get; private set; }

        /// <summary>Number of trading days to export</summary>
        public int Days { get; private set; }

        /// <summary>Primary data output folder</summary>
        public string DataFolder { get; private set; }

        /// <summary>Include current day even if incomplete</summary>
        public bool IncludeCurrentDay { get; private set; }

        /// <summary>Executed when export progress is updated</summary>
        public event EventHandler<GenericEventArgs<string>> OnProgressUpdate;


        /// <summary>
        /// ApiEventExportManager constructor
        /// </summary>
        /// <param name="symbol">Security symbol</param>
        /// <param name="exchange">Security exchange</param>
        /// <param name="days">Number of trading days to export</param>
        /// <param name="dataFolder">Primary data output folder</param>
        /// <param name="includeCurrentDay">Include current day even if incomplete</param>
        public ApiSecurityEventExportManager(string symbol, string exchange, int days, string dataFolder, bool includeCurrentDay)
        {
            Symbol = symbol;
            Exchange = exchange;
            Days = days;
            DataFolder = dataFolder;
            IncludeCurrentDay = includeCurrentDay;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Export()
        {

            //Determine output folder
            string folderName = DataFolder + @"\" + Symbol;
            if (Exchange != "ASX") folderName += "_" + Exchange;

            //Determine start date
            DateTime date =  new DateTime(2013,01,22); //DateTime.Today.AddDays(0);

            //Avoid creating a file for a day that contains incomplete data unless specified
            if (!IncludeCurrentDay)
            {
                if (DateTime.Now.Hour < 19) date = date.AddDays(-1);        
            }

            //Loop backwards through days until no more data available from Spark or max day count is reached
            int tradingDayCount = 0;
            int emptyDayCount = 0;
            do
            {

                //Check if date is potential trading day
                if ((date.DayOfWeek != DayOfWeek.Saturday) && (date.DayOfWeek != DayOfWeek.Sunday))
                {

                    //Determine output event file name
                    tradingDayCount++;
                    string eventFileName = folderName + @"\" + Symbol + "_" + Exchange + "_Event_" + date.ToString("yyyyMMdd") + ".txt";

                    //Request file if not already downloaded
                    string progressText = Symbol + "." + Exchange + "\t" + date.ToString("yyyyMMdd") + "\t";
                    if (!System.IO.File.Exists(eventFileName))
                    {

                        //Request data
                        Benchmark executeBench = new Benchmark("");
                        List<Spark.Event> events = null;
                        try
                        {

                            //Execute standard query
                            ApiSecurityEventQuery query = new ApiSecurityEventQuery(Symbol, Exchange, date);
                            events = query.Execute();

                            //Write to file
                            if (events != null)
                            {
                                if (events.Count > 0)
                                {
                                    ApiEventReaderWriter.WriteToFile(eventFileName, events);
                                }
                                else
                                {
                                    emptyDayCount++;
                                }
                                progressText += "Events:\t" + events.Count + "\tRequest Time:\t" + executeBench.Elapsed;
                            }
                            else
                            {
                                progressText += "Data is not currently available from Spark";
                            }

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
                    Debug.WriteLine(progressText);

                }

                //Move back one day
                date = date.AddDays(-1);

            } while ((tradingDayCount < Days) && (emptyDayCount < 4));


        }

    }

}
