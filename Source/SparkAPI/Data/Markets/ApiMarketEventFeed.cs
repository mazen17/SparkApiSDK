using System;
using SparkAPI.Common.Logging;

namespace SparkAPI.Data.Markets
{

    /// <summary>
    /// Provides a live event feed via the Spark API for the entire market
    /// </summary>
    /// <remarks>
    /// This feed calls the Spark.GetNextExchangeEvent() which is a synchronous method. This means when you call the method, it will 
    /// not return until it recieves an event or the timeout period is reached. By specifying a -1 timeout, we are telling it
    /// to wait until the end of the day. This structure means that RaiseEvent() method is only called when an event is
    /// received, but the logic must be run on a separate thread if we want other parts of the application to keep responding.
    /// </remarks>
    public class ApiMarketEventFeed : ApiEventFeedBase
    {

        /// <summary>Exchange identifier</summary>
        public string Exchange { get; private set; }

        /// <summary>
        /// ApiMarketEventFeed constructor
        /// </summary>
        /// <param name="exchange">Exchange sybmol</param>
        public ApiMarketEventFeed(string exchange)
        {
            Exchange = exchange;
        }

        /// <summary>
        /// Initiate data feed
        /// </summary>
        public override void Execute()
        {

            //Connect to Spark API if required
            ApiControl.Instance.Connect();

            //Get instance to exchange
            Spark.Exchange exchangeRef;
            if (ApiFunctions.GetSparkExchange(Exchange, out exchangeRef))
            {

                //Request all events for current day
                Spark.Event sparkEvent = new Spark.Event();
                if (Spark.GetAllExchangeEvents(ref exchangeRef, ref sparkEvent))
                {
                    while (Spark.GetNextExchangeEvent(ref exchangeRef, ref sparkEvent, -1))     //Specifying -1 timeout will keep it waiting until end of day
                    {
                        RaiseEvent(new EventFeedArgs(sparkEvent, Spark.TimeToDateTime(sparkEvent.Time)));
                    }
                }

                //Release memory at end of day
                Spark.ReleaseCurrentEvents();

            }

        }

    }
}
