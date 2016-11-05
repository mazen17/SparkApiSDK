using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparkAPI.Market
{

    /// <summary>
    /// Market order side enumeration
    /// </summary>
    public enum MarketSide
    {
        Bid = 0,
        Ask = 1
    }

    /// <summary>
    /// Security market state type
    /// </summary>
    public enum MarketState
    {
        Unknown = -1,
        None = 0,
        PreOpen = 1,
        OpenAuction = 2,        //Assigned when market state switches to Open but limit order book is still crossed
        Open = 3,
        PreCSPA = 4,
        CSPA = 5,
        Adjust = 6,
        AdjustOn = 7,
        Enquire = 8,
        PurgeOrders = 9,
        SystemMaintenance = 10,
        LateTrading = 11,
        PreNightTrading = 12,
        OpenNightTrading = 13,
        Close = 14,
        Suspend = 15,
        WaitVMB = 16,
        ABBAuction = 17,
        TradingHalt = 18,
        OpenVMB = 19
    }

}
