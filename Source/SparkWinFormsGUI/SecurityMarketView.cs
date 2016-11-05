using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SparkAPI.Common.Events;
using SparkAPI.Market;

namespace SparkWinFormsGUI
{

    /// <summary>
    /// User control that display depth and trade history for a security
    /// </summary>
    /// <remarks>
    /// MARKET SYNCHRONISATION
    /// Connecting this user control to a live event feed will result in it receiving all the events for the
    /// specified security-exchange from the beginning of the day. As it updates the UI after each event is
    /// received, it can take a long time for the UI to synchronise (catch up) to current market time.
    /// 
    /// To avoid this issue, the user control supports synchronisation logic that disables UI updating while
    /// it's receiving events that occured earlier in the day, then updates the controls just once after it
    /// has recieved all the historical events and starts to recieve events as they occur.
    /// 
    /// In order to determine if the user control has synchronised with the market, each time the synchronisation
    /// timer fires, it records the number of events it has recieved since the timer firing. By default the timer 
    /// fires every 250ms. If the control is synchronising with the market, there will be a big backlog of events
    /// to process, the control should have recieved 50+ new events in the 250ms interval. Once we have aligned
    /// with real-time, that number of events recieved a 250ms interval will become 0. Once this is detected, 
    /// synchronisation is turned off, and the form starts updating the controls everytime an event is recieved.
    /// 
    /// UPDATING CONTROLS VIA CROSS-THREAD CALLS
    /// This user control binds its event-handlers to security events that are raised in a separate thread. This
    /// means that when it receives an event, it's being sent from a different thread to the one the GUI is
    /// running under. It's not safe to update windows form controls (e.g. labels, listboxes, etc.) via a 
    /// cross-thread method call, so we use the Control.Invoke() method to pass the update from the event thread
    /// to the GUI thread. In order to determine if the Invoke() method should be used, the Control.InvokeRequired() 
    /// method is called. This returns TRUE if it is being called from another thread. If its TRUE, the Invoke() 
    /// method is executed, passing a delegate to the update method.
    /// </remarks>
    public partial class SecurityMarketView : UserControl
    {

        //Synchronisation fields
        private readonly Timer _synchronisationTimer = new Timer();             
        private int _lastQuoteUpdateCount = 0;
        private readonly Queue<string> _tradeEntries = new Queue<string>();
        private LimitOrderBook _depthReference = null;
        
        private string _lastUpdateTime = string.Empty;
        private int _quoteUpdateCount = 0;

        //Delegates used to update controls via a cross-thread call
        private delegate void AddListBoxItemDelegate(ListBox listBox, object item);
        private delegate void UpdateLabelTextDelegate(Label label, string text);

        /// <summary>Reference to security object</summary>
        private Security _security;

        /// <summary>Specifies whether live market data is currently being loaded</summary>
        public bool IsSynchronising { get; private set; }

        /// <summary>Specifies the number of levels to display in the depth view</summary>
        public int DepthLevels { get; set; }

        /// <summary>Enables pre-loading all existing events before updating the controls</summary>
        /// <remarks>Synchronisation avoids having to watch a full replay of all events before viewing the current market state.</remarks>
        public bool EnableSynchronisation { get; set; }

        /// <summary>Specifies if order book depth is aggregated by price level</summary>
        public bool DisplayAggregateDepth { get; set; }

        /// <summary>
        /// SecurityMarketView constructor
        /// </summary>
        public SecurityMarketView()
        {
            
            //Initialise component
            InitializeComponent();

            //Initialise synchronisation timer
            _synchronisationTimer.Interval = 250;
            _synchronisationTimer.Tick += _synchronisationTimer_Tick;
            EnableSynchronisation = false;
            DisplayAggregateDepth = true;

        }

        /// <summary>
        /// Executed every time the synchronisation timer fires
        /// </summary>
        void _synchronisationTimer_Tick(object sender, EventArgs e)
        {

            //Check if synchronisation is complete
            if ((_lastQuoteUpdateCount == _quoteUpdateCount) && (_lastUpdateTime != string.Empty))
            {
                Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "\t" + _lastQuoteUpdateCount + "\t" + _quoteUpdateCount + "\t" + _lastUpdateTime);
                LimitOrderBook depthClone = _depthReference.Clone();
                lblDepthView.Text = (DisplayAggregateDepth) ? CreateAggregateOrderDepthText(depthClone) : CreateOrderDepthText(depthClone);
                lblMarketTime.Text = _lastUpdateTime;
                lblMarketState.Text = _security.State.ToString();
                bool hasTrades = (_tradeEntries.Count > 0);
                if (hasTrades)
                {
                    lbTrades.BeginUpdate();
                    while (_tradeEntries.Count > 0)
                    {
                        lbTrades.Items.Insert(0, _tradeEntries.Dequeue());
                    }
                    lbTrades.EndUpdate();
                }

                //Disable the synchronisation timer so that it stops firing
                IsSynchronising = false;
                _synchronisationTimer.Stop();

            }
            else
            {

                //Update the last trade queue count
                //Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "\t" + _lastQuoteUpdateCount + "\t" + _quoteUpdateCount + "\t" + _lastUpdateTime);
                _lastQuoteUpdateCount = _quoteUpdateCount;

            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void Initialise()
        {

            //Clear controls
            lblDepthView.Text = string.Empty;
            lbTrades.Items.Clear();

            //Initialise synchronisation objects
            if (EnableSynchronisation)
            {
                _lastQuoteUpdateCount = 0;
                _tradeEntries.Clear();
                _depthReference = null;
                _lastUpdateTime = string.Empty;
                IsSynchronising = true;
                _synchronisationTimer.Start();
                lblMarketState.Text = "Synchronising...";
            }
            else
            {
                IsSynchronising = false;
            }

        }


        /// <summary>
        /// Get and sets the security assigned to the control
        /// </summary>
        public Security Security
        {
            get
            {
                return _security;
            }
            set
            {
            
                //Disconnect existing security if required
                if (_security != null)
                {
                    _security.OnTradeUpdate -= security_OnTradeUpdate;
                    _security.OnDepthUpdate -= _security_OnDepthUpdate;
                    _security.OnMarketStateUpdate -= _security_OnMarketStateUpdate;
                }
                
                //Connect security update events to user-control event handlers
                _security = value;
                if (_security != null)
                {
                    _security.OnTradeUpdate += new EventHandler<GenericEventArgs<Trade>>(security_OnTradeUpdate);
                    _security.OnDepthUpdate += new EventHandler<GenericEventArgs<LimitOrderBook>>(_security_OnDepthUpdate);
                    _security.OnMarketStateUpdate += new EventHandler<GenericEventArgs<MarketState>>(_security_OnMarketStateUpdate);
                    Initialise();

                }
                
            }
        }

        /// <summary>
        /// Executed in response to a trade update event for the security
        /// </summary>
        private void security_OnTradeUpdate(object sender, GenericEventArgs<Trade> e)
        {
            string tradeText = CreateTradeText(e.Value);
            if (IsSynchronising)
            {
                _tradeEntries.Enqueue(tradeText);
                _lastUpdateTime = e.TimeStamp.ToMarketTime();
            }
            else
            {
                AddListBoxItem(lbTrades, tradeText);
                UpdateLabelText(lblMarketTime, e.TimeStamp.ToMarketTime());
            }
        }

        /// <summary>
        /// Executed in response to a depth update for the security
        /// </summary>
        private void _security_OnDepthUpdate(object sender, GenericEventArgs<LimitOrderBook> e)
        {
            _quoteUpdateCount++;
            _depthReference = e.Value;
            if (IsSynchronising)
            {
                _lastUpdateTime = e.TimeStamp.ToMarketTime();                
            }
            else
            {
                string depthText = (DisplayAggregateDepth) ? CreateAggregateOrderDepthText(e.Value) : CreateOrderDepthText(e.Value);
                UpdateLabelText(lblDepthView, depthText);
                UpdateLabelText(lblMarketTime, e.TimeStamp.ToMarketTime());
            }
        }

        /// <summary>
        /// Executed in response to a market state update event for the security
        /// </summary>
        private void _security_OnMarketStateUpdate(object sender, GenericEventArgs<MarketState> e)
        {
            if (!IsSynchronising)
            {
                UpdateLabelText(lblMarketState, e.Value.ToString());
            }
        }

        /// <summary>
        /// Handles cross-thread adding of items to listboxes
        /// </summary>
        /// <param name="listBox">Listbox reference</param>
        /// <param name="item">Item to be added to listbox</param>
        private static void AddListBoxItem(ListBox listBox, object item)
        {
            if (listBox.InvokeRequired)
            {
                listBox.Invoke(new AddListBoxItemDelegate(AddListBoxItem), listBox, item);
            }
            else
            {
                listBox.Items.Insert(0, item);
            }
        }

        /// <summary>
        /// Handles cross-thread updating text in a label
        /// </summary>
        /// <param name="label"></param>
        /// <param name="text"></param>
        private static void UpdateLabelText(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new UpdateLabelTextDelegate(UpdateLabelText), label, text);
            }
            else
            {
                label.Text = text;
            }
        }

        /// <summary>
        /// Generate trade text line
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        private static string CreateTradeText(Trade trade)
        {
            StringBuilder output = new StringBuilder();
            output.Append(trade.TimeStamp.ToMarketTime().PadRight(10));
            output.Append(trade.Price.ToDecimalPrice().ToShortPriceString().PadRight(10));
            output.Append(trade.Volume.ToString().PadRight(9));
            output.Append(trade.ConditionCodes.PadRight(10));
            return output.ToString();
        }

        /// <summary>
        /// Generate multi-line string that displays the limit order book
        /// </summary>
        /// <param name="orderBook"></param>
        private string CreateOrderDepthText(LimitOrderBook orderBook)
        {
            //Build header
            StringBuilder output = new StringBuilder();
            output.Append("TIME  ".PadLeft(10));
            output.Append("VOLUME".PadLeft(9));
            output.Append("BID ".PadLeft(9));
            output.Append("".PadRight(4));
            output.Append(" ASK".PadRight(9));
            output.Append("VOLUME".PadRight(9));
            output.Append("TIME".ToString());
            output.AppendLine();
            output.AppendLine();

            //Build depth view
            for (int i = 0; i < DepthLevels; i++)
            {

                //Write bid text for level
                if (orderBook.Bid.Count > i)
                {
                    LimitOrder bidOrder = orderBook.Bid[i];
                    output.Append(bidOrder.TimeStamp.ToMarketTime().PadRight(10));
                    output.Append(bidOrder.Volume.ToString().PadLeft(9));
                    output.Append(bidOrder.Price.ToDecimalPrice().ToShortPriceString().PadLeft(9));
                    output.Append("".PadRight(4));
                }
                else
                {
                    output.Append("".PadRight(32));
                }

                //Write ask text for level
                if (orderBook.Ask.Count > i)
                {
                    LimitOrder askOrder = orderBook.Ask[i];
                    output.Append(askOrder.Price.ToDecimalPrice().ToShortPriceString().PadRight(9));
                    output.Append(askOrder.Volume.ToString().PadRight(9));
                    output.Append(askOrder.TimeStamp.ToMarketTime());
                }
                output.AppendLine();

            }
            return output.ToString();
        }

        /// <summary>
        /// Generate multi-line string that displays the limit order book
        /// </summary>
        /// <param name="orderBook"></param>
        private string CreateAggregateOrderDepthText(LimitOrderBook orderBook)
        {

            //Generate aggregate limit order book view
            AggregatedLimitOrderBook aggregateOrderBook = new AggregatedLimitOrderBook(orderBook, DepthLevels);

            //Build header
            StringBuilder output = new StringBuilder();
            output.Append("#".PadLeft(8));
            output.Append("VOLUME".PadLeft(11));
            output.Append("BID ".PadLeft(9));
            output.Append("".PadRight(4));
            output.Append(" ASK".PadRight(9));
            output.Append("VOLUME".PadRight(11));
            output.Append("#".ToString());
            output.AppendLine();
            output.AppendLine();

            //Build depth view
            for (int i = 0; i < DepthLevels; i++)
            {

                //Write bid text for level
                if (aggregateOrderBook.Bid.Count > i)
                {
                    LimitOrderBookPriceLevel bidOrder = aggregateOrderBook.Bid[i];
                    output.Append(bidOrder.Count.ToString().PadLeft(8));
                    output.Append(bidOrder.Volume.ToString().PadLeft(11));
                    output.Append(bidOrder.Price.ToDecimalPrice().ToShortPriceString().PadLeft(9));
                    output.Append("".PadRight(4));
                }
                else
                {
                    output.Append("".PadRight(32));
                }

                //Write ask text for level
                if (aggregateOrderBook.Ask.Count > i)
                {
                    LimitOrderBookPriceLevel askOrder = aggregateOrderBook.Ask[i];
                    output.Append(askOrder.Price.ToDecimalPrice().ToShortPriceString().PadRight(9));
                    output.Append(askOrder.Volume.ToString().PadRight(11));
                    output.Append(askOrder.Count.ToString());
                }
                output.AppendLine();

            }
            return output.ToString();

        }

        /// <summary>
        /// Fired when depth label is double-clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblDepthView_DoubleClick(object sender, System.EventArgs e)
        {

            //Swap between price level and order depth view
            DisplayAggregateDepth = !DisplayAggregateDepth;
            _security_OnDepthUpdate(this, new GenericEventArgs<LimitOrderBook>(DateTime.Now, this._depthReference));

        }


    }
}
