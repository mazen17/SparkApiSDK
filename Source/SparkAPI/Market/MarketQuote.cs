using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparkAPI.Market
{

    /// <summary>
    /// Market quote observation
    /// </summary>
    public class MarketQuote
    {

        /// <summary>Security symbol</summary>
        internal string Symbol { get; private set; }

        /// <summary>Security exchange</summary>
        internal string Exchange { get; private set; }

        /// <summary>Highest bid price</summary>
        public int BidPrice { get; private set; }

        /// <summary>Lowest ask price</summary>
        public int AskPrice { get; private set; }

        /// <summary>Market quote observation time</summary>
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// MarketQuote constructor
        /// </summary>
        /// <param name="symbol">Security symbol</param>
        /// <param name="exchange">Security exchange</param>
        /// <param name="bidPrice">Bid price</param>
        /// <param name="askPrice">Ask price</param>
        /// <param name="timeStamp">Quote time stamp</param>
        public MarketQuote(string symbol, string exchange, int bidPrice, int askPrice, DateTime timeStamp)
        {
            Symbol = symbol;
            Exchange = exchange;
            BidPrice = bidPrice;
            AskPrice = askPrice;
            TimeStamp = timeStamp;
        }

        /// <summary>
        /// Write market quote to tab-delimited string
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
            output.Append(BidPrice.ToDecimalPrice().ToShortPriceString());
            output.Append("\t");
            output.Append(AskPrice.ToDecimalPrice().ToShortPriceString());
            return output.ToString();
        }



    }
}
