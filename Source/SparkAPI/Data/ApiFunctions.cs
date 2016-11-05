using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SparkAPI.Market;


namespace SparkAPI.Data
{

    /// <summary>
    /// Provides a set of static functions that are used when interfacing with the Spark API
    /// </summary>
    public static class ApiFunctions
    {

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Force disconnection from the Spark API 
        /// </summary>
        public static void Disconnect()
        {
            ApiControl.Instance.Disconnect();
        }

        /// <summary>
        /// Extension method for the Spark.Event struct that converts a Spark event into LimitOrder that can be submitted 
        /// into the LimitOrderBook
        /// </summary>
        /// <param name="eventItem">Spark event</param>
        internal static LimitOrder ToLimitOrder(this Spark.Event eventItem)
        {
            MarketSide side = ApiFunctions.GetMarketSide(eventItem.Flags);
            int price = eventItem.Price;
            int volume = (int) eventItem.Volume;
            DateTime timeStamp = DateTimeFromUnixTimestampSeconds(eventItem.Time);
            return new LimitOrder(side, price, volume, timeStamp);
        }

        /// <summary>
        /// Extension method for the Spark.Event struct that generates new trade from order event if there is an 
        /// event type match, otherwise returns NULL.
        /// </summary>
        internal static Trade ToTrade(this Spark.Event eventItem, DateTime observationTime)
        {
            var values = GetSymbolExchange(eventItem.Code, eventItem.Exchange);
            return eventItem.ToTrade(values.Item1, values.Item2, observationTime);
        }

        /// <summary>
        /// Extension method for the Spark.Event struct that generates new trade from order event if there is an 
        /// event type match, otherwise returns NULL.
        /// </summary>
        internal static Trade ToTrade(this Spark.Event eventItem, string symbol, string exchange, DateTime observationTime)
        {
            int price = eventItem.Price;
            int volume = (int)eventItem.Volume;
            DateTime timeStamp = DateTimeFromUnixTimestampSeconds(eventItem.Time);
            string conditionCodes = Spark.ConditionCodesToStr(eventItem.ConditionCodes, eventItem.Exchange);
            return new Trade(timeStamp, symbol, exchange, price, volume, conditionCodes, observationTime);
        }

        /// <summary>
        /// Converts Spark.Event depth flag into market side
        /// </summary>
        /// <param name="sparkFlags">Spark flags parameter</param>
        internal static MarketSide GetMarketSide(int sparkFlags)
        {
            return ((sparkFlags & Spark.DEPTH_FLAG_BID) == Spark.DEPTH_FLAG_BID) ? MarketSide.Bid : MarketSide.Ask;
        }

        /// <summary>
        /// Return that date that Spark considers the current day using current day request method and getting first event
        /// </summary>
		public static DateTime GetCurrentSparkDate()
		{
            DateTime result = DateTime.Today;
            ApiControl.Instance.Connect();
            Spark.Exchange exchange;
            if (ApiFunctions.GetSparkExchange("ASX", out exchange))
            {
                result = DateTimeFromUnixTimestampSeconds(exchange.Date);
            }
            return result;
		}

        /// <summary>
        /// Return Spark Exhange time
        /// </summary>
        public static DateTime GetExchangeTime()
        {
            ApiControl.Instance.Connect();
            return Spark.GetExchangeTime("ASX");
        }

        /// <summary>
        /// Convert Spark UNIX time to .NET DateTime
        /// </summary>
        /// <param name="seconds">Seconds since the epoc (1-Jan-1970)</param>
        public static DateTime DateTimeFromUnixTimestampSeconds(long seconds)
        {
            return UnixEpoch.AddSeconds(seconds).ToLocalTime();            
        }

        /// <summary>
        /// Converts Spark State type code into MarketState enum
        /// </summary>
        /// <param name="sparkStateType">Spark market state value</param>
        internal static MarketState ConvertToMarketState(int sparkStateType)
        {
            switch (sparkStateType)
            {
                case Spark.STATE_ABB_AUCTION:
                    return MarketState.ABBAuction;

                case Spark.STATE_ADJUST:
                    return MarketState.Adjust;

                case Spark.STATE_ADJUST_ON:
                    return MarketState.AdjustOn;

                case Spark.STATE_CLOSE:
                    return MarketState.Close;

                case Spark.STATE_CSPA:
                    return MarketState.CSPA;

                case Spark.STATE_ENQUIRE:
                    return MarketState.Enquire;

                case Spark.STATE_LATE_TRADING:
                    return MarketState.LateTrading;

                case Spark.STATE_NONE:
                    return MarketState.None;

                case Spark.STATE_OPEN:
                    return MarketState.Open;

                case Spark.STATE_OPEN_NIGHT_TRADING:
                    return MarketState.OpenNightTrading;

                case Spark.STATE_OPEN_VMB:
                    return MarketState.OpenVMB;

                case Spark.STATE_PRE_CSPA:
                    return MarketState.PreCSPA;

                case Spark.STATE_PRE_NIGHT_TRADING:
                    return MarketState.PreNightTrading;

                case Spark.STATE_PREOPEN:
                    return MarketState.PreOpen;

                case Spark.STATE_PURGE_ORDERS:
                    return MarketState.PurgeOrders;

                case Spark.STATE_SUSPEND:
                    return MarketState.Suspend;

                case Spark.STATE_SYSTEM_MAINTENANCE:
                    return MarketState.SystemMaintenance;

                case Spark.STATE_TRADING_HALT:
                    return MarketState.TradingHalt;

                case Spark.STATE_WAIT_VMB:
                    return MarketState.WaitVMB;

                default:
                    return MarketState.Unknown;
            }
        }

        /// <summary>
        /// Convert Spark.Event struct into tab-separted human readable string
        /// </summary>
        /// <param name="eventItem">Spark event item</param>
        internal static string ToOutputString(this Spark.Event eventItem)
        {
            System.Text.StringBuilder result = new StringBuilder();
            result.Append(DateTimeFromUnixTimestampSeconds(eventItem.Time).ToMarketDateTime() + "\t");
            result.Append(Spark.EventTypeToStr(eventItem.Type) + "\t");
            result.Append(eventItem.Code + "\t");
            result.Append(eventItem.Exchange + "\t");
            result.Append(ApiFunctions.GetMarketSide(eventItem.Flags).ToString() + "\t");
            result.Append(eventItem.Price.ToPriceString() + "\t");
            result.Append(eventItem.Volume + "\t");
            result.Append(Spark.ConditionCodesToStr(eventItem.ConditionCodes, eventItem.Exchange) + "\t");
            result.Append(eventItem.Flags + "\t");
            result.Append(Spark.StateToStr(eventItem.State) + "\t");
            result.Append(eventItem.Position + "\t");
            result.Append(Spark.QuoteBasesToStr(eventItem.QuoteBases, eventItem.Exchange) + "\t");
            result.Append(eventItem.Reference + "\t");
            result.Append(eventItem.Count + "\t");
            return result.ToString();
        }

        /// <summary>
        /// Convert condition codes to text format
        /// </summary>
        /// <param name="conditionCodes">Condition code long value</param>
        /// <param name="exchange">Exchange</param>
        internal static string ConvertToConditionCodeText(ulong conditionCodes, string exchange)
        {
            return Spark.ConditionCodesToStr(conditionCodes, exchange);
        }


        /// <summary>
        /// Creates a Spark.Stock reference for the specified security and exchange.
        /// </summary>
        /// <param name="exchangeId">Security exchange</param>
        /// <param name="exchange">Exchange reference generated by the method</param>
        /// <returns>TRUE if Spark was able to create the specified exchange reference, else FALSE</returns>
        internal static bool GetSparkExchange(string exchangeId, out Spark.Exchange exchange)
        {
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "\t" + "Creating rreference to Spark exchange '" + exchangeId + "'...");
            exchange = new Spark.Exchange();
            if (!Spark.GetExchange(ref exchange, exchangeId))
            {
                string error = Spark.DescribeError(Spark.GetLastError());
                Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "\t" + "Creating rreference to Spark exchange '" + exchangeId + "' failed: " + error);
                return false;
            }
            else
            {
                Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "\t" + "Creating rreference to Spark exchange '" + exchangeId + "' complete");
                return true;
            }
        }

        /// <summary>
        /// Creates a Spark.Stock reference for the specified security and exchange.
        /// </summary>
        /// <param name="symbol">Security symbol</param>
        /// <param name="exchange">Security exchange</param>
        /// <param name="stock">Stock reference generated by the method</param>
        /// <returns>TRUE if Spark was able to create the specified stock, else FALSE</returns>
        internal static bool GetSparkStock(string symbol, string exchange, out Spark.Stock stock)
        {
            
            //Get internal spark security symbol
            Tuple<string, string> sparkSymbolExchange = ApiFunctions.GetSparkSymbolExchange(symbol, exchange);

            //Get reference to stock
            string identifier = sparkSymbolExchange.Item1 + "." + sparkSymbolExchange.Item2;
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "\t" + "Initiating event feed for " + identifier);
            stock = new Spark.Stock();
            if (!Spark.GetStock(ref stock, sparkSymbolExchange.Item1, sparkSymbolExchange.Item2))
            {
                Console.WriteLine("Can't get {0}: {1}", identifier, Spark.DescribeError(Spark.GetLastError()));
                return false;
            }

            //Wait to allow Spark API to data access
            System.Threading.Thread.Sleep(1000);
            return true;

        }

        /// <summary>
        /// Returns correct Spark security symbol for alternative exchanges
        /// </summary>
        /// <param name="symbol">Security symbol</param>
        /// <param name="exchange">Security exchange</param>
        /// <remarks>
        /// Spark does not implement Chi-X and ASX PureMatch as seperate exchanges. Instead, it assigns a suffix
        /// to the symbol to denote an alternative venue. 
        /// </remarks>
        internal static Tuple<string, string> GetSparkSymbolExchange(string symbol, string exchange)
        {
            switch (exchange.ToUpper())
            {
                case "CXA":                 //Chi-X Australia
                    return new Tuple<string, string>(symbol + "_CX", "ASX");

                case "PMX":                 //ASX PureMatch
                    return new Tuple<string, string>(symbol + "_PM", "ASX");

                default:
                    return new Tuple<string, string>(symbol, exchange);
            }
        }

        /// <summary>
        /// Returns correct security symbol given a Spark security code
        /// </summary>
        /// <param name="sparkSymbol">Security symbol</param>
        /// <param name="sparkExchange">Security exchange</param>
        /// <remarks>
        /// Spark does not implement Chi-X and ASX PureMatch as seperate exchanges. Instead, it assigns a suffix
        /// to the symbol to denote an alternative venue. 
        /// </remarks>
        internal static Tuple<string, string> GetSymbolExchange(string sparkSymbol, string sparkExchange)
        {
            int index = sparkSymbol.IndexOf('_');
            if (index > 0)
            {
                return new Tuple<string, string>(sparkSymbol.Substring(0, index - 1), "CXA");
            }
            else
            {
                return new Tuple<string, string>(sparkSymbol, sparkExchange);
            }
        }

    }
}
