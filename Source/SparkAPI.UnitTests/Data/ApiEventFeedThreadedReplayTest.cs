using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SparkAPI.Common.Diagnostics;

namespace SparkAPI.UnitTests.Data
{

    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class ApiEventFeedThreadedReplayTest
    {

        /// <summary>
        /// Test replay for single security
        /// </summary>
        [Test]
        public void ExecuteTest_SingleSecurity()
        {
            
            //Execute replay
            var security = new SparkAPI.Market.Security("AHD");
            var replayManager = new SparkAPI.Data.ApiEventFeedThreadedReplay(@"Data\TestData\AHD_Event_20120426.txt");
            replayManager.AddSecurity(security);
            replayManager.Execute();

            //Validate replay
            Assert.AreEqual(40, security.Trades.Count);

        }

        /// <summary>
        /// Test replay for multiple securities
        /// </summary>
        [Test]
        public void ExecuteTest_MultipleSecurites()
        {

            //Create replay manager
            Benchmark benchmark = new Benchmark("ExecuteTest_MultipleSecurites");
            var replayManager = new SparkAPI.Data.ApiEventFeedThreadedReplay(@"Data\TestData\ASX_Event_20121231.txt");

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
