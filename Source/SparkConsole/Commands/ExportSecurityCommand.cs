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
    /// exportsecurity  {securityListFile}  {destinationFolder}     [-d:{n}]  [-t]
    ///     securityListFile    Text file containing list of securities to export
    ///     destinationFolder   Destination folder for data files
    ///     -d                  Days to export where n equals number of days
    ///     -t                  Include current day
    /// </remarks>
    public class ExportSecurityCommand : IConsoleCommand
    {
        /// <summary>Log reference</summary>
        private readonly ILogDefinition _log;

        /// <summary>
        /// ExportSecurityCommand constructor
        /// </summary>
        /// <param name="log"></param>
        public ExportSecurityCommand(ILogDefinition log)
        {
            _log = log;
        }

        /// <summary>
        /// Execute command 
        /// </summary>
        /// <param name="args"></param>
        public void Execute(string[] args)
        {

            //Validate minimum argument count
            if (args.Count() < 3) throw new ArgumentException("'exportsecurity' command requires at least two arguments");

            //Get arguments
            string securityFileList = args[1];
            string destinationFolder = args[2];

            //Get optional arguments
            string daysToExportArg = (args.Count() >= 4) ? args[3] : "-d:20";
            string includeCurrentDayArg = (args.Count() >= 5) ? args[4] : "";
            int daysToExport = int.Parse(daysToExportArg.Split(':')[1]);
            bool includeCurrentDay = (includeCurrentDayArg == "-t");

            //Validate security list
            if (!System.IO.File.Exists(securityFileList)) throw new ArgumentException("Security list file '" + securityFileList + "' does not exist");
            string[] symbols = File.ReadAllLines(securityFileList);

            //Export specified security list to file
            foreach (string symbol in symbols)
            {
                string[] symbolItems = symbol.Split('.');
                if (symbolItems.Count() == 1)
                {

                    //Export both exchanges to file (if available) if no exchange specified
                    ExportSecurityToFile(symbol, "ASX", destinationFolder, daysToExport, includeCurrentDay);
                    ExportSecurityToFile(symbol, "CXA", destinationFolder, daysToExport, includeCurrentDay);

                }
                else
                {

                    //Export security to file for specified exchange
                    ExportSecurityToFile(symbolItems[0], symbolItems[1], destinationFolder, daysToExport, includeCurrentDay);

                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="exchange"></param>
        /// <param name="dataFolder"></param>
        /// <param name="daysToExport"></param>
        /// <param name="includeCurrentDay"></param>
        private void ExportSecurityToFile(string symbol, string exchange, string dataFolder, int daysToExport, bool includeCurrentDay)
        {
            var exportManager = new ApiSecurityEventExportManager(symbol, exchange, daysToExport, dataFolder, includeCurrentDay);
            exportManager.OnProgressUpdate += exportManager_OnProgressUpdate;
            exportManager.Export();
        }

        /// <summary>
        /// Executed when a progress update is sent from the export manager
        /// </summary>
        private void exportManager_OnProgressUpdate(object sender, SparkAPI.Common.Events.GenericEventArgs<string> e)
        {
            _log.WriteLine(e.Value);
        }

    }

}
