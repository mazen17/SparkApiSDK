using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparkAPI.Market
{

    /// <summary>
    /// Contains details of a trade record
    /// </summary>
    public class Trade
    {

        /// <summary>Exchange timestamp</summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>Security symbol</summary>
        public string Symbol { get; private set; }

        /// <summary>Security exchange</summary>
        public string Exchange { get; private set; }

        /// <summary>Transaction price</summary>
        public int Price { get; private set; }

        /// <summary>Transaction volume</summary>
        public int Volume { get; private set; }

        /// <summary>Condition codes</summary>
        public string ConditionCodes { get; private set; }

        /// <summary>Observation time</summary>
        /// <remarks>Set to exchange time if replaying from file otherwise set to time that event was received</remarks>
        public DateTime ObservationTime { get; internal set; }

        /// <summary>
        /// Trade constructor
        /// </summary>
        /// <param name="timeStamp">Exchange trade timestamp</param>
        /// <param name="symbol">Security symbol</param>
        /// <param name="exchange">Security exchange</param>
        /// <param name="price">Trade price</param>
        /// <param name="volume">Trade volume</param>
        /// <param name="conditionCodes">Trade condition codes</param>
        /// <param name="observationTime">Trade observation time</param>
        public Trade(DateTime timeStamp, string symbol, string exchange, int price, int volume, string conditionCodes, DateTime observationTime)
        {
            TimeStamp = timeStamp;
            Symbol = symbol;
            Exchange = exchange;
            Price = price;
            Volume = volume;
            ConditionCodes = conditionCodes;
            ObservationTime = observationTime;
        }

        /// <summary>
        /// Write trade record to tab-delimited string
        /// </summary>
        public new string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.Append(TimeStamp.ToMarketTime());
            output.Append("\t");
            output.Append(Symbol);
            output.Append("\t");
            output.Append(Exchange);
            output.Append("\t");
            output.Append(Price.ToDecimalPrice().ToShortPriceString());
            output.Append("\t");
            output.Append(Volume);
            output.Append("\t");
            output.Append(ConditionCodes);
            return output.ToString();
        }

    }
}
