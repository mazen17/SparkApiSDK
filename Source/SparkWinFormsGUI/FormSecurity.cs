using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SparkAPI.Data;
using SparkAPI.Data.Securities;
using SparkAPI.Market;
using ThreadState = System.Threading.ThreadState;

namespace SparkWinFormsGUI
{

    /// <summary>
    /// Connects to live market data feed and displays trade and depth information for a specified security
    /// </summary>
    public partial class FormSecurity : Form
    {

        private ApiEventFeedBase _eventFeed;
        private Thread _eventFeedThread;
        private Security _security;

        /// <summary>
        /// FormSecurity constructor
        /// </summary>
        public FormSecurity()
        {
            InitializeComponent();
            cbExchange.SelectedIndex = 0;
            txtSymbol.Select();
        }


        /// <summary>
        /// Updates market view to reflect data from specified symbol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSymbol_TextChanged(object sender, EventArgs e)
        {
            string text = txtSymbol.Text.Trim();
            if ((text.Length == 3) || (text.Length == 5))
            {
                Initialise(text, cbExchange.Text);    
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="exchange"></param>
        private void Initialise(string symbol, string exchange)
        {

            //Stop existing event feed if required
            if (_eventFeed != null) Stop();

            //Start new event feed using 
            _eventFeed = new ApiSecurityEventFeed(symbol, exchange);
            
            //Create stock to process events
            _security = new Security(symbol);
            _eventFeed.AddSecurity(_security);
            securityMarketView.Security = _security;


            //Initiate replay on separate thread
            _eventFeedThread = new Thread(_eventFeed.Execute);
            _eventFeedThread.Start();

        }

        /// <summary>
        /// Stops event feed updates and closes down feed and thread
        /// </summary>
        private void Stop()
        {
            if (_eventFeed != null)
            {
                _eventFeed.RemoveSecurity(_security);
                Application.DoEvents();
                _eventFeedThread.Abort();
                int retryCount = 0;
                while ((_eventFeedThread.ThreadState != ThreadState.Aborted) && (retryCount < 10))
                {
                    Thread.Sleep(100);
                    retryCount++;
                }
                _eventFeedThread = null;
                _eventFeed = null;
            }
        }

        /// <summary>
        /// Executed when the form is being closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormSecurity_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbExchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSymbol_TextChanged(this, null);
        }

    }
}
