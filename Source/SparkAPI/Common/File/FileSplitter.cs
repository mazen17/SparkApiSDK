using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SparkAPI.Common.File
{

    /// <summary>
    /// 
    /// </summary>
    public class FileSplitter
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputfileName"></param>
        /// <param name="outputFileName"></param>
        /// <param name="lines"></param>
        public static void SplitFile(string inputfileName, string outputFileName, int lines)
        {
            StringBuilder output = new StringBuilder();
            int lineCount = 0;
            StreamReader file = null;
            try
            {
                file = new StreamReader(inputfileName);
                string line;
                do
                {
                    line = file.ReadLine();
                    if (line != null)
                    {
                        lineCount++;
                        output.AppendLine(line);
                    }
                } while ((line != null) && (lineCount < lines));
            }
            finally
            {
                if (file != null) file.Close();     //Close file stream even if there is an error
            }
            if (output.Length > 0)
            {
                System.IO.File.WriteAllText(outputFileName, output.ToString());
            }
        }

    }
}
