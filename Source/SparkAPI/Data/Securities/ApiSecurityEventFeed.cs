using System;
using SparkAPI.Common.Logging;

namespace SparkAPI.Data.Securities
{

    /// <summary>
    /// Provides a live event feed via the Spark API for a specified stock and exchange
    /// </summary>
    /// <remarks>
    /// This feed calls the Spark.GetNextStockEvent() which is a synchronous method. This means when you call the method, it will 
    /// not return until it recieves an event or the timeout period is reached. By specifying a -1 timeout, we are telling it
    /// to wait until the end of the day. This structure means that RaiseEvent() method is only called when an event is
    /// received, but the logic must be run on a separate thread if we want other parts of the application to keep responding.
    /// </remarks>
    public class ApiSecurityEventFeed : ApiEventFeedBase
    {

        /// <summary>Security code</summary>
        internal string Symbol { get; private set; }

        /// <summary>Security exchange</summary>
        internal string Exchange { get; private set; }

        /// <summary>
        /// ApiStockEventFeed constructor
        /// </summary>
        /// <param name="symbol">Security code</param>
        /// <param name="exchange">Security exchange</param>
        public ApiSecurityEventFeed(string symbol, string exchange)
        {
            Symbol = symbol;
            Exchange = exchange;
        }

        /// <summary>
        /// Initiate data feed
        /// </summary>
        public override void Execute()
        {

            //Connect to Spark API if required
            ApiControl.Instance.Connect();

            //Get instance to stock
            Spark.Stock stock;
            if (ApiFunctions.GetSparkStock(Symbol, Exchange, out stock))
            {

                //Request all events for current day
                Spark.Event sparkEvent = new Spark.Event();
                if (Spark.GetAllStockEvents(ref stock, ref sparkEvent))
                {
                    while (Spark.GetNextStockEvent(ref stock, ref sparkEvent, -1))  //Specifying -1 timeout will keep it waiting until end of day
                    {
                        RaiseEvent(new EventFeedArgs(sparkEvent, Spark.TimeToDateTime(sparkEvent.Time)));
                    }
                }

                //Release memory at end of day
                Spark.ReleaseStockEvents(ref stock);

            }

        }

    }
}
