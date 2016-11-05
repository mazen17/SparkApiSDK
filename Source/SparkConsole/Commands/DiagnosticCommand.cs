using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparkAPI.Common.Logging;

namespace SparkConsole.Commands
{
    /// <summary>
    /// Execute a set of Spark diagnostics
    /// </summary>
    public class DiagnosticCommand : IConsoleCommand
    {

        /// <summary>Log reference</summary>
        private readonly ILogDefinition _log;

        /// <summary>
        /// Diagnostic command constructor
        /// </summary>
        /// <param name="log"></param>
        public DiagnosticCommand(ILogDefinition log)
        {
            _log = log;
        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public void Execute(string[] args)
        {
            //Validate connection and communication with Spark server
            try
            {
                DateTime sparkDate = SparkAPI.Data.ApiFunctions.GetCurrentSparkDate();
                _log.WriteLine("Spark server connection validated");
            }
            catch (Exception ex)
            {
                _log.WriteLine("ERROR: " + ex.Message + "\r\n" + ex.StackTrace);
            }

        }

    }
}
