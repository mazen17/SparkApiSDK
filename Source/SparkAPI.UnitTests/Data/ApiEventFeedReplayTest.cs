using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SparkAPI.Common.Diagnostics;
using SparkAPI.Market;

namespace SparkAPI.UnitTests.Data
{

    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class ApiEventFeedReplayTest
    {

        /// <summary>
        /// Test replay for single security
        /// </summary>
        [Test]
        public void ExecuteTest_SingleSecurity()
        {
            
            //Execute replay
            var replayManager = new SparkAPI.Data.ApiEventFeedReplay(@"Data\TestData\AHD_Event_20120426.txt");
            var security = new SparkAPI.Market.Security("AHD");
            replayManager.AddSecurity(security);
            replayManager.Execute();

            //Validate replay
            Assert.AreEqual(40, security.Trades.Count);

        }

        [Test]
        public void ExecuteTest_SingleSecurity_Verbose()
        {

            //Create an event feed using replay from file
            var replayManager = new SparkAPI.Data.ApiEventFeedReplay(@"Data\TestData\AHD_Event_20120426.txt");

            //Create a security to receive the event feed
            var security = new SparkAPI.Market.Security("AHD");
            replayManager.AddSecurity(security);

            //Add event handlers to write each trade and quote update to console
            security.OnTradeUpdate += (sender, args) => Console.WriteLine("TRADE\t" + args.Value.ToString());
            security.OnQuoteUpdate += (sender, args) => Console.WriteLine("QUOTE\t" + args.Value.ToString());

            //Initiate the event replay
            replayManager.Execute();

        }

        /// <summary>
        /// Test replay for multiple securities
        /// </summary>
        [Test]
        public void ExecuteTest_MultipleSecurites()
        {

            //Create replay manager
            Benchmark benchmark = new Benchmark("ExecuteTest_MultipleSecurites");
            var replayManager = new SparkAPI.Data.ApiEventFeedReplay(@"Data\TestData\ASX_Event_20121231.txt");

            //Add securities
            replayManager.AddSecurity("QWN");
            replayManager.AddSecurity("QNA");
            replayManager.AddSecurity("QPN");

            //Execute replay
            replayManager.Execute();
            benchmark.WriteToConsole();

            //Validate replay
            Assert.AreEqual(0, replayManager.Securities["QWN"].Trades.Count);
            Assert.AreEqual(0, replayManager.Securities["QNA"].Trades.Count);
            Assert.AreEqual(0, replayManager.Securities["QPN"].Trades.Count);

        }

    }

}
