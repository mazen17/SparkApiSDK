using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SparkAPI.Common.Logging;

namespace SparkAPI.UnitTests.Common.Logging
{

    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    class LogDefinitionTest
    {

        /// <summary>
        /// Write to log test with default configuration
        /// </summary>
        [Test]
        public void WriteTest_Default()
        {
            LogDefinition log = new LogDefinition("Test", @"C:\Temp\TestLog.txt");
            log.WriteLine("Test log entry");
            LogDefinitionList.Instance.Flush();
        }

    }

}
