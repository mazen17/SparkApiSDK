using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using SparkAPI.Common.Logging;
using SparkAPI.Data.Markets;
using SparkAPI.Data.Securities;

namespace SparkConsole.Commands
{

    /// <summary>
    /// Export specified security data to destination folder
    /// </summary>
    /// <remarks>
    /// exportmarket  {destinationFolder}   {exchange}    
    ///     destinationFolder   Destination folder for data files
    ///     exchange            Exchange code to export (i.e. "ASX"}
    /// </remarks>
    public class ExportMarketCommand : IConsoleCommand
    {

        /// <summary>Log reference</summary>
        private readonly ILogDefinition _log;

        /// <summary>
        /// Export command constructor
        /// </summary>
        /// <param name="log"></param>
        public ExportMarketCommand(ILogDefinition log)
        {
            _log = log;
        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public void Execute(string[] args)
        {

            //Validate minimum argument count
            if (args.Count() < 3) throw new ArgumentException("'exportmarket' command requires at least two arguments");

            //Get required arguments
            string destinationFolder = args[1];
            string exchange = args[2];

            //Export market data to file
            ApiMarketEventExportManager exportManager = new ApiMarketEventExportManager(exchange, destinationFolder);
            exportManager.OnProgressUpdate += exportManager_OnProgressUpdate;
            exportManager.Export();

        }

        /// <summary>
        /// Executed when export manager sends a progress update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportManager_OnProgressUpdate(object sender, SparkAPI.Common.Events.GenericEventArgs<string> e)
        {
            _log.WriteLine(e.Value);
        }

    }

}
