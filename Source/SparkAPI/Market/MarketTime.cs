using System;

namespace SparkAPI.Market
{

    /// <summary>
    /// Static extension methods providing pre-formatted timestamp strings
    /// </summary>
    public static class MarketTime
    {

        /// <summary>
        /// Returns time specified to three decimal places in format HH:mm:ss
        /// </summary>
        public static string ToMarketTime(this DateTime dateStamp)
        {
            return dateStamp.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Returns date and time specified to three decimal places in format dd-MM-yyyy HH:mm:ss
        /// </summary>
        public static string ToMarketDateTime(this DateTime dateStamp)
        {
            return dateStamp.ToString("dd-MM-yyyy HH:mm:ss");
        }

    }
}
