using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparkAPI.Market
{

    /// <summary>
    /// Provides a view of an order book where orders are aggregated by price level
    /// </summary>
    public class AggregatedLimitOrderBook
    {

        /// <summary>Bid order price level list</summary>
        public List<LimitOrderBookPriceLevel> Bid { get; private set; }

        /// <summary>Ask order price level list</summary>
        public List<LimitOrderBookPriceLevel> Ask { get; private set; }

        /// <summary>
        /// AggregatedLimitOrderBook constructor
        /// </summary>
        /// <param name="orderBook">Limit order book</param>
        public AggregatedLimitOrderBook(LimitOrderBook orderBook) : this(orderBook, -1)
        {
        }

        /// <summary>
        /// AggregatedLimitOrderBook constructor
        /// </summary>
        /// <param name="orderBook">Limit order book</param>
        /// <param name="maximumPriceLevels">Number of aggregate price levels to calculate</param>
        public AggregatedLimitOrderBook(LimitOrderBook orderBook, int maximumPriceLevels)
        {
            Bid = GeneratePriceLevels(orderBook.Bid, maximumPriceLevels);
            Ask = GeneratePriceLevels(orderBook.Ask, maximumPriceLevels);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderList"></param>
        /// <param name="maximumPriceLevels"></param>
        /// <returns></returns>
        private static List<LimitOrderBookPriceLevel> GeneratePriceLevels(IEnumerable<LimitOrder> orderList, int maximumPriceLevels)
        {
            List<LimitOrderBookPriceLevel> result = new List<LimitOrderBookPriceLevel>();
            LimitOrderBookPriceLevel priceLevel = null;
            int currentPrice = 0;
            int levelIndex = 0;
            foreach (LimitOrder order in orderList)
            {

                //Update price level
                if (order.Price != currentPrice)
                {
                    levelIndex++;
                    if (priceLevel != null) result.Add(priceLevel);
                    priceLevel = new LimitOrderBookPriceLevel();
                    priceLevel.Price = order.Price;
                    currentPrice = order.Price;
                }
                priceLevel.Count++;
                priceLevel.Volume += order.Volume;

                //Exit if maximium price level is reached
                if (levelIndex > maximumPriceLevels) break;

            }
            return result;
        }

    }

}
