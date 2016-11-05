using System.Collections.Generic;
using System.IO;
using System.Text;
using SparkAPI.Market;

namespace SparkAPI.Data
{

    /// <summary>
    /// Class handles reading and writing Spark events from and to file
    /// </summary>
    public class ApiEventReaderWriter
    {

        /// <summary>
        /// ApiEventReaderWriter constructor
        /// </summary>
        /// <remarks>
        /// The Spark API must be initalised before use otherwise many functions will not work correctly, but it won't throw an exception 
        /// and will just return an invalid result. 
        /// </remarks>
        public ApiEventReaderWriter()
        {
            ApiControl.Instance.Initialise();
        }

        /// <summary>
        /// Read all Spark events from file and return as list of Spark.Events
        /// </summary>
        /// <param name="fileName">Event file name</param>
        public IEnumerable<Spark.Event> ReadFromFile(string fileName)
        {
            List<Spark.Event> result = null;
            if (System.IO.File.Exists(fileName))
            {
                result = new List<Spark.Event>();
                string[] eventTextRows = File.ReadAllLines(fileName);
                foreach (string eventTextRow in eventTextRows)
                {
                    result.Add(Parse(eventTextRow));
                }
            }
            return result;
        }

        /// <summary>
        /// Open file for reading and then read one line at a time calling the event processing delegate (function pointer) to 
        /// process the event
        /// </summary>
        /// <remarks>
        /// Streaming events sequentially from a file is slightly faster than loading all events into an array before processing. 
        /// It also reduces the memory required when processing a large file.
        /// </remarks>
        /// <param name="fileName">Event file name</param>
        /// <param name="eventProcessingFunction">Delegate (function pointer) for the method that will process the events</param>
        internal void StreamFromFile(string fileName, ApiEventFeedReplay.EventReceivedHandler eventProcessingFunction)
        {
            StreamReader file = null;
            try
            {
                file = new StreamReader(fileName);
                string line;
                do
                {
                    line = file.ReadLine();
                    if (line != null)
                    {
                        Spark.Event eventItem = ApiEventReaderWriter.Parse(line);
                        eventProcessingFunction(eventItem);
                    }
                } while (line != null);
            }
            finally
            {
                if (file != null) file.Close();     //Close file stream even if there is an error
            }            
        }

        /// <summary>
        /// Parses Spark event file string into Spark.Event struct
        /// </summary>
        /// <param name="eventItemText">Spark event in tab-delimited text format</param>
        public static Spark.Event Parse(string eventItemText)
        {

            //Parse Row
            string[] entryItems = eventItemText.Split('\t');

            //Create new event
            Spark.Event eventItem = new Spark.Event
                {
                    Code = entryItems[0],
                    ConditionCodes = ulong.Parse(entryItems[1]),
                    Count = int.Parse(entryItems[2]),
                    Exchange = entryItems[3],
                    Flags = int.Parse(entryItems[4]),
                    Position = int.Parse(entryItems[5]),
                    Price = int.Parse(entryItems[6]),
                    QuoteBases = ulong.Parse(entryItems[7]),
                    Reference = int.Parse(entryItems[8]),
                    State = int.Parse(entryItems[9]),
                    Time = int.Parse(entryItems[10]),
                    TimeNsec = int.Parse(entryItems[11]),
                    Type = int.Parse(entryItems[12]),
                    Volume = int.Parse(entryItems[13])
                };
            return eventItem;

        }

        /// <summary>
        /// Write Spark event list to file
        /// </summary>
        /// <param name="fileName">Output file name</param>
        /// <param name="eventItems">Spark event list</param>
        public static void WriteToFile(string fileName, IEnumerable<Spark.Event> eventItems)
        {

            //Build output string
            StringBuilder output = new StringBuilder();
            foreach (Spark.Event eventItem in eventItems)
            {
                output.AppendLine(ToFileString(eventItem));
            }

            //Create folder if required
            string folderName = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);

            //Write to file
            System.IO.File.WriteAllText(fileName, output.ToString());

        }

        /// <summary>
        /// Convert Spark.Event struct into tab-separted string for file storage
        /// </summary>
        /// <param name="eventItem">Spark event item</param>
        public static string ToFileString(Spark.Event eventItem)
        {
            StringBuilder result = new StringBuilder();
            result.Append(eventItem.Code + "\t");
            result.Append(eventItem.ConditionCodes + "\t");
            result.Append(eventItem.Count + "\t");
            result.Append(eventItem.Exchange + "\t");
            result.Append(eventItem.Flags + "\t");
            result.Append(eventItem.Position + "\t");
            result.Append(eventItem.Price + "\t");
            result.Append(eventItem.QuoteBases + "\t");
            result.Append(eventItem.Reference + "\t");
            result.Append(eventItem.State + "\t");
            result.Append(eventItem.Time + "\t");
            result.Append(eventItem.TimeNsec + "\t");
            result.Append(eventItem.Type + "\t");
            result.Append(eventItem.Volume);
            return result.ToString();
        }

    }
}
