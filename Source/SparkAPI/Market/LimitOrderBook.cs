using System;
using System.Text;
using SparkAPI.Data;

namespace SparkAPI.Market
{

    /// <summary>
    /// Contains details of all orders currently in the limit order book for a specific security and exchange
    /// </summary>
    public class LimitOrderBook
    {

        /// <summary>Security symbol</summary>
        public string Symbol { get; private set; }

        /// <summary>Security exchange</summary>
        public string Exchange { get; private set; }

        /// <summary>Bid limit order list</summary>
        public LimitOrderList Bid { get; private set; }

        /// <summary>Ask limit order list</summary>
        public LimitOrderList Ask { get; private set; }

        /// <summary>Internal synchronisation lock object to protect against simultaneous access from multiple threads</summary>
        private object _lock = new object();

        /// <summary>
        /// LimitOrderBook constructor
        /// </summary>
        /// <param name="symbol">Security symbol</param>
        /// <param name="exchange">Security exchange</param>        
        public LimitOrderBook(string symbol, string exchange)
        {
            Symbol = symbol;
            Exchange = exchange;
            Bid = new LimitOrderList(symbol, MarketSide.Bid);
            Ask = new LimitOrderList(symbol, MarketSide.Ask);
        }

        /// <summary>
        /// Submit Spark.Event depth update event (thread-safe)
        /// </summary>
        /// <param name="eventItem">Spark event</param>
        public void SubmitEvent(Spark.Event eventItem)
        {
            LimitOrderList list = (ApiFunctions.GetMarketSide(eventItem.Flags) == MarketSide.Bid) ? Bid : Ask;
            lock (_lock)
            {
                list.SubmitEvent(eventItem);
            }
        }

        /// <summary>
        /// Current distance in price ticks between the best bid and ask prices
        /// </summary>
        public int TickSpread
        {
            get
            {
                return (AskPrice - BidPrice).ToPriceTicks(BidPrice);
            }
        }

        /// <summary>
        /// Average of current market bid and ask prices
        /// </summary>
        public int MidQuote
        {
            get
            {
                if ((BidPrice.IsBlankPrice()) && (AskPrice.IsBlankPrice()))
                {
                    return MarketPrice.BlankPrice;
                }
                else
                {
                    int bid = BidPrice;
                    int ask = AskPrice;
                    if (bid.IsBlankPrice()) { bid = ask; }
                    if (ask.IsBlankPrice()) { ask = bid; }
                    return (int)((double)(bid + ask) / 2);
                }
            }
        }

        /// <summary>Highest bid price</summary>
        public int BidPrice
        {
            get
            {
                int price = MarketPrice.BlankPrice;
                if (Bid.Count > 0) price = Bid[0].Price;
                return price;
            }
        }

        /// <summary>Lowest ask price</summary>
        public int AskPrice
        {
            get
            {
                int price = MarketPrice.BlankPrice;
                if (Ask.Count > 0) price = Ask[0].Price;
                return price;
            }
        }

        /// <summary>
        /// Returns TRUE if the market ask price is less than or equal to market bid price
        /// </summary>
        public bool HasCrossedQuotes
        {
            get
            {
                return (AskPrice <= BidPrice);
            }
        }

        /// <summary>
        /// Return discrete clone of limit order book (thread-safe)
        /// </summary>   
        public LimitOrderBook Clone()
        {
            LimitOrderBook result = new LimitOrderBook(Symbol, Exchange);
            lock (_lock)
            {
                result.Bid = Bid.Clone();
                result.Ask = Ask.Clone();
            }
            return result;
        }

        /// <summary>
        /// Returns tab-separated string representation of the limit order book (side, price, time, volume) (thread-safe)
        /// </summary>
        public new string ToString()
        {
            StringBuilder output = new StringBuilder();
            lock (_lock)
            {
                output.Append(Bid.ToString());
                output.Append(Ask.ToString());
            }
            string result = output.ToString();
            return result;
        }

    }
}
