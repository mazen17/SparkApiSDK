using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SparkAPI.Data.Securities
{

    /// <summary>
    /// Executes an event query against the Spark API and returns a list of Spark.Event objects
    /// </summary>
    public class ApiSecurityEventQuery
    {

        /// <summary>Security symbol</summary>
        public string Symbol { get; private set; }

        /// <summary>Security exchange</summary>
        public string Exchange { get; private set; }

        /// <summary>Request date</summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// ApiSecurityEventQuery constructor
        /// </summary>
        /// <param name="symbol">Security symbol</param>
        /// <param name="exchange">Security exchange</param>
        public ApiSecurityEventQuery(string symbol, string exchange)
        {
            Symbol = symbol;
            Exchange = exchange;
            Date = DateTime.Today;
        }

        /// <summary>
        /// ApiSecurityEventQuery constructor
        /// </summary>
        /// <param name="symbol">Security symbol</param>
        /// <param name="exchange">Security exchange</param>
        /// <param name="date">Request date</param>
        public ApiSecurityEventQuery(string symbol, string exchange, DateTime date)
        {
            Symbol = symbol;
            Exchange = exchange;
            Date = date;
        }

        /// <summary>
        /// Execute query
        /// </summary>
        /// <returns></returns>
        public List<Spark.Event> Execute()
        {
            return Execute(Symbol, Exchange, Date);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol">Security symbol</param>
        /// <param name="exchange">Security exchange</param>
        /// <param name="date">Request date</param>
        public static List<Spark.Event> Execute(string symbol, string exchange, DateTime date)
        {

            //Connect to Spark API if required
            ApiControl.Instance.Connect();

            //Determine what Spark considers to be the current date
            DateTime sparkDate = ApiFunctions.GetCurrentSparkDate();
            Debug.WriteLine("\tCurrent Spark Date: " + date.ToShortDateString());

            //Get instance to stock
            Spark.Stock stock;
            List<Spark.Event> result = null;
            if (ApiFunctions.GetSparkStock(symbol, exchange, out stock))
            {

                //Request stock events
                Spark.Event sparkEvent = new Spark.Event();                    
                result = new List<Spark.Event>();
                if (date == sparkDate)
                {

                    //Wait to allow Spark API to prepare for download (it may only return a small number of events without this)
                    System.Threading.Thread.Sleep(1000);
                    //NOTE: This is only returning 1 event when requested over a mobile wireless connection. It may require more delay or some other sort of test.

                    //Execute query using current day request method
                    Debug.WriteLine("\tSpark.GetAllStockEvents(" + symbol + ")");
                    if (Spark.GetAllStockEvents(ref stock, ref sparkEvent))
                    {
                        while (Spark.GetNextStockEvent(ref stock, ref sparkEvent, 1000))  //Specifying 0 timeout will return events until there are no more in the queue
                        {
                            result.Add(sparkEvent);
                        }
                    }
                    Spark.ReleaseStockEvents(ref stock);

                }
                else
                {

                    //Execute query using historical request method
                    Debug.WriteLine("\tSpark.GetPastStockEvents(" + symbol + ", " + date.ToShortDateString() + ")");
                    if (Spark.GetPastStockEvents(ref stock, ref sparkEvent, date))
                    {
                        while (Spark.GetNextPastStockEvent(ref stock, ref sparkEvent))
                        {
                            result.Add(sparkEvent);
                        }
                    }
                    Spark.ReleasePastStockEvents(ref stock, (DateTime)date);

                }

            }
            return result;

        }

    }
}
