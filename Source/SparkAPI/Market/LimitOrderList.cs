using System.Collections.Generic;
using System.Text;
using SparkAPI.Data;

namespace SparkAPI.Market
{

    /// <summary>
    /// Contains details of all orders currently on one side of the limit order book for a specific security and exchange
    /// </summary>
    public class LimitOrderList : List<LimitOrder>
    {

        /// <summary>Stock symbol</summary>
        public string Symbol { get; private set; }

        /// <summary>Market side</summary>
        public MarketSide Side { get; private set; }

        /// <summary>
        /// LimitOrderList constructor
        /// </summary>        
        public LimitOrderList(string symbol, MarketSide side)
        {
            Symbol = symbol;
            Side = side;
        }

        /// <summary>
        /// Update order list according to details in specified order change event
        /// </summary>
        /// <param name="eventItem">Spark event</param>
        public void SubmitEvent(Spark.Event eventItem)
        {
            switch (eventItem.Type)
            {
                case Spark.EVENT_NEW_DEPTH:

                    //ENTER
                    LimitOrder order = eventItem.ToLimitOrder();
                    if (Count == 0)
                    {
                        Add(order);
                    }
                    else
                    {
                        Insert(eventItem.Position - 1, order);
                    }
                    break;

                case Spark.EVENT_AMEND_DEPTH:
                    
                    //AMEND
                    this[eventItem.Position - 1].Volume = (int)eventItem.Volume;
                    break;

                case Spark.EVENT_DELETE_DEPTH:
                    
                    //DELETE
                    RemoveAt(eventItem.Position - 1);
                    break;

                default:
                    break;

            }


        }

        /// <summary>
        /// Returns tab-separated string representation of the limit order book side (side, price, time, volume)
        /// </summary>
        public new string ToString()
        {
            StringBuilder output = new StringBuilder();
            foreach (LimitOrder limitOrderItem in this)
            {
                output.AppendLine(Side.ToString() + "\t" + limitOrderItem.ToString());
            }
            return output.ToString();
        }

        /// <summary>
        /// Returns a discrete clone of limit order list
        /// </summary>
        public LimitOrderList Clone()
        {
            LimitOrderList result = new LimitOrderList(Symbol, Side);
            foreach (LimitOrder orderItem in this)
            {
                result.Add(orderItem.Clone());
            }
            return result;
        }

    }
}
