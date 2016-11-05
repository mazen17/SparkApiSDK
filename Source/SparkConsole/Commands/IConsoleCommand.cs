using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparkConsole.Commands
{

    /// <summary>
    /// Console command interface
    /// </summary>
    internal interface IConsoleCommand
    {

        /// <summary>
        /// Execute command
        /// </summary>
        /// <param name="args">Command line arguments</param>
        void Execute(string[] args);

    }
}
