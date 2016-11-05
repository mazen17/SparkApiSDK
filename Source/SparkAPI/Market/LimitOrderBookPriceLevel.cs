using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparkAPI.Market
{

    /// <summary>
    /// Aggregate summary of a limit order book price level
    /// </summary>
    public class LimitOrderBookPriceLevel
    {

        /// <summary>Order price level</summary>
        public int Price { get; set; }

        /// <summary>Total volume available at price level</summary>
        public int Volume { get; set; }

        /// <summary>Number of orders at price level</summary>
        public int Count { get; set; }

    }

}
