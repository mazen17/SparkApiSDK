using System;
using System.Diagnostics;
using System.Text;

namespace SparkAPI.Market
{

    /// <summary>
    /// Contains details of a single order currently in the limit order book for a specific security and exchange
    /// </summary>
    /// <remarks>
    /// This class is called 'LimitOrder' rather than just 'Order' to distinguish it from the other types of order
    /// classes that can be found in a trading system (e.g. parent/client order, market order, etc.). 
    /// </remarks>
    public class LimitOrder
    {

        /// <summary>Order submission side</summary>
        public MarketSide Side { get; private set; }

        /// <summary>Order price</summary>
        public int Price { get; private set; }

        /// <summary>Order submission time</summary>
        public DateTime TimeStamp { get; private set; }

        /// <summary>Order volume</summary>
        public int Volume { get; set; }

        /// <summary>
        /// LimitOrder constructor
        /// </summary>
        public LimitOrder(MarketSide side, int price, int volume, DateTime timeStamp)
        {
            Side = side;
            Price = price;
            Volume = volume;
            TimeStamp = timeStamp;
        }

        /// <summary>
        /// Create duplicate of limit order object
        /// </summary>
        public LimitOrder Clone()
        {
            return new LimitOrder(Side, Price, Volume, TimeStamp);
        }

        /// <summary>
        /// Returns tab-separated string representation of the limit order (price, time, volume, recorded)
        /// </summary>        
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.Append(Price.ToPriceString());
            output.Append('\t');
            output.Append(TimeStamp.ToMarketDateTime());
            output.Append('\t');
            output.Append(Volume);
            output.Append('\t');
            return output.ToString();
        }

    }
}
