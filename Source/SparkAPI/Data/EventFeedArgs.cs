using System;

namespace SparkAPI.Data
{

    /// <summary>
    /// Event arguments supplied when an event feed provides a Spark event update
    /// </summary>
    internal class EventFeedArgs : EventArgs
    {

        /// <summary>Security symbol</summary>
        internal string Symbol { get; private set; }

        /// <summary>Security exchange</summary>
        internal string Exchange { get; private set; }

        /// <summary>Spark event</summary>
        internal Spark.Event Event { get; private set; }

        /// <summary>Observation time</summary>
        internal DateTime TimeStamp { get; private set; }

        /// <summary>
        /// EventFeedArgs constructor
        /// </summary>
        /// <param name="eventItem">Spark event</param>
        /// <param name="timestamp">Observation timestamp</param>
        internal EventFeedArgs(Spark.Event eventItem, DateTime timestamp)
        {
            Event = eventItem;
            TimeStamp = timestamp;
            var adjustedValues = ApiFunctions.GetSymbolExchange(eventItem.Code, eventItem.Exchange);
            Symbol = adjustedValues.Item1;
            Exchange = adjustedValues.Item2;
        }

    }
}
