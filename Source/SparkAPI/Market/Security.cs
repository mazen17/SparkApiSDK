using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparkAPI.Common.Events;
using SparkAPI.Data;

namespace SparkAPI.Market
{
    
    /// <summary>
    /// Security class contains a trade record and a limit order book (depth) for each exchange the security is traded on 
    /// </summary>
    /// <remarks>
    /// The security receives Spark.Event events and processes them to generate a trade record and current view of the limit order 
    /// book (depth). As there are now two lit exchanges in Australia (ASX and Chi-X), the security contains a dictionary of order
    /// books; one for each exchange. If a security is provided with both an ASX and Chi-X feed, it will maintain two order books,
    /// but show a single trade record.
    /// </remarks>
    public class Security
    {

        /// <summary>Security code</summary>
        public string Symbol { get; private set; }

        /// <summary>Limit order books dictionary using exchange code as key</summary>
        public Dictionary<string, LimitOrderBook> OrderBooks { get; private set; }

        /// <summary>Security trade list</summary>
        public List<Trade> Trades { get; private set; }

        /// <summary>Market state of the security</summary>
        public MarketState State { get; private set; }

        /// <summary>Raised by securtiy when it receives a trade update</summary>
        public event EventHandler<GenericEventArgs<Trade>> OnTradeUpdate;

        /// <summary>Raised by securtiy when it receives a depth update</summary>
        public event EventHandler<GenericEventArgs<LimitOrderBook>> OnDepthUpdate;

        /// <summary>Raised by securtiy when it receives a market quote update</summary>
        public event EventHandler<GenericEventArgs<MarketQuote>> OnQuoteUpdate;

        /// <summary>Raised by securtiy when it receives a market state update</summary>
        public event EventHandler<GenericEventArgs<MarketState>> OnMarketStateUpdate;

        /// <summary>
        /// Security constructor
        /// </summary>
        /// <param name="symbol">Security code</param>
        public Security(string symbol)
        {
            Symbol = symbol;
            OrderBooks = new Dictionary<string, LimitOrderBook>();
            Trades = new List<Trade>();
        }

        /// <summary>
        /// Processes the Spark update event provided in the EventFeedArgs and updates security properties based on the event type
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="eventFeedArgs">Event feed args</param>
        internal void EventReceived(object sender, EventFeedArgs eventFeedArgs)
        {

            //Process event
            Spark.Event eventItem = eventFeedArgs.Event;
            switch (eventItem.Type)
            {

                //Depth update
                case Spark.EVENT_NEW_DEPTH:
                case Spark.EVENT_AMEND_DEPTH:
                case Spark.EVENT_DELETE_DEPTH:

                    //Check if exchange order book exists and create if it doesn't
                    LimitOrderBook orderBook;
                    if (!OrderBooks.TryGetValue(eventFeedArgs.Exchange, out orderBook))
                    {
                        orderBook = new LimitOrderBook(eventFeedArgs.Symbol, eventFeedArgs.Exchange);
                        OrderBooks.Add(eventFeedArgs.Exchange, orderBook);
                    }

                    //Submit update to appropriate exchange order book
                    orderBook.SubmitEvent(eventItem);
                    if (OnDepthUpdate != null) OnDepthUpdate(this, new GenericEventArgs<LimitOrderBook>(eventFeedArgs.TimeStamp, orderBook));
                    break;

                //Trade update
                case Spark.EVENT_TRADE:

                    //Create and store trade record
                    Trade trade = eventItem.ToTrade(eventFeedArgs.Symbol, eventFeedArgs.Exchange, eventFeedArgs.TimeStamp);
                    Trades.Add(trade);
                    if (OnTradeUpdate != null) OnTradeUpdate(this, new GenericEventArgs<Trade>(eventFeedArgs.TimeStamp, trade));
                    break;

                //Trade cancel
                case Spark.EVENT_CANCEL_TRADE:
                    
                    //Find original trade in trade record and delete
                    Trade cancelledTrade = eventItem.ToTrade(eventFeedArgs.TimeStamp);
                    Trade originalTrade = Trades.Find(x => (x.TimeStamp == cancelledTrade.TimeStamp && x.Price == cancelledTrade.Price && x.Volume == cancelledTrade.Volume));
                    if (originalTrade != null) Trades.Remove(originalTrade);
                    break;

                //Market state update
                case Spark.EVENT_STATE_CHANGE:
                    State = ApiFunctions.ConvertToMarketState(eventItem.State);
                    if (OnMarketStateUpdate != null) OnMarketStateUpdate(this, new GenericEventArgs<MarketState>(eventFeedArgs.TimeStamp, State));
                    break;

                //Market quote update (change to best market bid-ask prices)
                case Spark.EVENT_QUOTE:
                    if (OnQuoteUpdate != null)
                    {
                        LimitOrderBook depth = OrderBooks[eventFeedArgs.Exchange];
                        MarketQuote quote = new MarketQuote(eventFeedArgs.Symbol, eventFeedArgs.Exchange, depth.BidPrice, depth.AskPrice, eventFeedArgs.TimeStamp);
                        OnQuoteUpdate(this, new GenericEventArgs<MarketQuote>(eventFeedArgs.TimeStamp, quote));
                    }
                    break;

                //IAP (Indicative Auction Price) Update
                case Spark.EVENT_AUCTION_PRICE:
                    break;

                default:
                    //Console.WriteLine(eventItem.ToOutputString());
                    break;

            }
        }

    }

}
