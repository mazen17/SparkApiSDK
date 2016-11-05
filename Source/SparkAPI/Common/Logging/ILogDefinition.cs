using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparkAPI.Common.Logging
{

    /// <summary>
    /// Log definition interface
    /// </summary>
    public interface ILogDefinition
    {

        /// <summary>
        /// Write text to log file
        /// </summary>
        void Write(string text);

        /// <summary>
        /// Write text with a trailing CRLF to log file
        /// </summary>
        void WriteLine(string text);

    }
}
