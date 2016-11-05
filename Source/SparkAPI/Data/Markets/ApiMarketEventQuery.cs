using System.Collections.Generic;

namespace SparkAPI.Data.Markets
{

    /// <summary>
    /// Executes an event query against the Spark API and returns a list of Spark.Event objects for the specified exchange
    /// </summary>
    public class ApiMarketEventQuery
    {

        /// <summary>Security exchange</summary>
        public string Exchange { get; private set; }

        /// <summary>
        /// ApiMarketEventQuery constructor
        /// </summary>
        /// <param name="exchange">Exchange identifier</param>
        public ApiMarketEventQuery(string exchange)
        {
            Exchange = exchange;
        }

        /// <summary>
        /// Execute query
        /// </summary>
        /// <returns></returns>
        public List<Spark.Event> Execute()
        {
            return Execute(Exchange);
        }

        /// <summary>
        /// Execute query
        /// </summary>
        /// <param name="exchange">Security exchange</param>
        public static List<Spark.Event> Execute(string exchange)
        {

            //Connect to Spark API if required
            ApiControl.Instance.Connect();

            //Get exchange reference
            Spark.Exchange exchangeRef;
            List<Spark.Event> result = null;
            if (ApiFunctions.GetSparkExchange(exchange, out exchangeRef))
            {

                //Wait to allow Spark API to prepare for large download (it will only return a small number of events without this)
                System.Threading.Thread.Sleep(1000);

                //Request stock events
                result = new List<Spark.Event>();
                Spark.Event sparkEvent = new Spark.Event();

                //Execute query using current day request method
                if (Spark.GetAllExchangeEvents(ref exchangeRef, ref sparkEvent))
                {
                    while (Spark.GetNextExchangeEvent(ref exchangeRef, ref sparkEvent, 1000))
                    {
                        result.Add(sparkEvent);
                    }
                }
                Spark.ReleaseCurrentEvents();
            }
            return result;

        }

    }
}
