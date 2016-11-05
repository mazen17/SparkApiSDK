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
using SparkConsole.Commands;

namespace SparkConsole
{

    class Program
    {

        private const string LOG_ID = "SparkConsole";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            //Initialise log output
            LogDefinition log = new LogDefinition(LOG_ID, Environment.CurrentDirectory + @"\Logs\SparkConsole.log");
            LogDefinitionList.Instance.Add(LOG_ID, log);
            
            //Log arguments
            StringBuilder argText = new StringBuilder();
            for (int i = 0; i < args.Count(); i++)
            {
                argText.Append(string.Format("[{0}]={1} ", i, args[i]));
            }
            log.WriteLine("Intiated");
            log.WriteLine("ARGUMENTS: " + argText.ToString());

            //Execute command
            try
            {
                
                //Get command identifier
                if (args.Count() == 0) throw new ArgumentException("Command not specified");
                string commandId = args[0].ToLower();
                IConsoleCommand command = null;
                switch (commandId)
                {
                    case "exportmarket":
                        command = new ExportMarketCommand(log);
                        break;

                    case "exportsecurity":
                        command = new ExportSecurityCommand(log);
                        break;

                    case "diagnostic":
                        command = new DiagnosticCommand(log);
                        break;

                    case "test":
                        log.WriteLine("Test log entry");
                        break;

                    default:
                        throw new ArgumentException("Command '" + commandId + "' not recognised");
                }

                //Execute command
                if (command != null) command.Execute(args);

            }
            catch (ArgumentException ex)
            {
                log.WriteLine("ERROR: " + ex.Message);
            }
            catch (Exception ex)
            {
                log.WriteLine("ERROR: " + ex.Message + "\r\n" + ex.StackTrace);
            }

            //Flush log buffer
            log.WriteLine("Completed\r\n");
            LogDefinitionList.Instance.Flush();            

        }

    }
}
