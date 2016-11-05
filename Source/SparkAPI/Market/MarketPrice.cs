namespace SparkAPI.Market
{

    /// <summary>
    /// Static and extension methods use for converting and handling market prices (integer value 
    /// representations of floating point prices)
    /// </summary>
    /// <remarks>
    /// Stock market related applications often perform comparison operations on prices. For example,
    /// comparing an aggressive market order price against a limit price in the order book to
    /// determine if a trade has occurred. As prices are expressed in dollars and cents, prices are 
    /// normally represented as a floating point number (float or double). However, comparisons using
    /// floating point numbers are error prone (e.g. 36.0400000001 != 36.04) and yield unpredictable
    /// results.
    /// 
    /// To avoid this issue, stock market related applications internally convert prices into an
    /// integer format. Not only does this ensure accurate comparison operations, but reduces the 
    /// memory footprint and speeds up comparisons (integer operations are faster on a CPU than
    /// floating point operations).
    /// 
    /// The sub-dollar section of prices are retained in an integer format by multiplying the price
    /// by a scaling factor. For example, the price 34.25 would become 342500 with a scaling factor
    /// of 10,000. Four decimal places are sufficient to represent the valid range of tick prices
    /// in the Australian market, as the smallest price would be a mid-point trade on a sub $0.10 
    /// stock with a tick of 0.001 (e.g. Bid=0.081  Ask=0.082   Mid=0.0815).
    /// 
    /// An accuracy of four decimal places works for market prices, however calculation of values
    /// such as average execution price on an order are better stored in a double or decimal
    /// type as they do not require accurate comparisons.
    /// </remarks>
    public static class MarketPrice
    {

        /// <summary>Internal scaling factor for converting decimal to integer</summary>
        public const int ScalingFactor = Spark.PRICE_MULTIPLIER;

        /// <summary>
        /// Convert integer price to decimal price
        /// </summary>
        /// <param name="price"></param>
        public static decimal ToDecimalPrice(this int price)
        {
            return (decimal)price / ScalingFactor;
        }

        /// <summary>
        /// Convert integer price to double price
        /// </summary>
        /// <param name="price"></param>
        public static double ToDoublePrice(this int price)
        {
            return (double)price / ScalingFactor;
        }

        /// <summary>
        /// Convert decimal price to integer price
        /// </summary>
        /// <param name="price"></param>
        public static int ToIntegerPrice(this decimal price)
        {
            return (int)(price * ScalingFactor);
        }

        /// <summary>
        /// Convert double price to integer price
        /// </summary>
        /// <param name="price"></param>
        public static int ToIntegerPrice(this double price)
        {
            return (int)(price * ScalingFactor);
        }

        /// <summary>
        /// Returns market price represented in decimal format
        /// </summary>
        public static string ToPriceString(this int price)
        {
            return price.ToDecimalPrice().ToPriceString();
        }

        /// <summary>
        /// Returns market price represented in decimal format
        /// </summary>
        public static string ToPriceString(this decimal price)
        {
            return price.ToString("0.0000");
        }

        /// <summary>
        /// Returns market price represented in decimal format
        /// </summary>
        public static string ToShortPriceString(this decimal price)
        {
            string text = price.ToString("0.0000");
            for (int i = 0; i <= 1; i++)
            {
                if (text[text.Length - 1] == '0')
                {
                    text = text.Substring(0, text.Length - 1);
                }
            }
            return text;
        }

        /// <summary>
        /// Returns market price represented as price ticks
        /// </summary>
        /// <param name="price">Price to convert to ticks</param>
        /// <param name="currentPrice">Current stock price used to determine minimum price tick</param>
        /// <remarks>Changes to scaling factor must be manually updated in this method</remarks>
        public static int ToPriceTicks(this int price, int currentPrice)
        {
            return price / Tick(currentPrice);
        }

        /// <summary>
        /// Returns tick size as integer price for current market price
        /// </summary>
        /// <param name="price">Price to convert to ticks</param>
        /// <remarks>Changes to scaling factor must be manually updated in this method</remarks>
        public static int Tick(this int price)
        {
            if (price < 0.1 * ScalingFactor)        //Under $0.10
            {
                return (int)(0.001 * ScalingFactor);    
            }
            else if (price < 2 * ScalingFactor)     //Less than $2.00  
            {
                return (int)(0.005 * ScalingFactor);
            }
            else                                    //Over $2.00
            {
                return (int)(0.01 * ScalingFactor);
            }
        }

        /// <summary>Returns TRUE if price is blank (equal to 0)</summary>
        public static bool IsBlankPrice(this int price)
        {
            return (price == BlankPrice);
        }

        /// <summary>Returns blank market price</summary>
        public static int BlankPrice { get { return 0; } }


    }

}
