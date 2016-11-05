using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SparkAPI.Data
{

    /// <summary>
    /// Creates a file stream reader for the specified event file and then raises each event as it is receieved. Supports 
    /// pausing, resuming and stepping forward through an event replay.
    /// </summary>
    /// <remarks>
    /// The event file is streamed (read one line at a time) rather than read in entirity to reduce the memory footprint
    /// and increase the speed at which the replay begins.
    /// 
    /// This class inherits the ApiEventFeedBase class and raises the  events though its RaiseEvent() method.
    /// </remarks>
    public class ApiEventFeedReplay : ApiEventFeedBase
    {

        /// <summary>Event feed file name</summary>
        internal string FileName { get; private set; }

        /// <summary>Specifies whether the replay is currently paused</summary>
        public bool IsPaused { get; private set; }

        /// <summary>Specifies whether the replay should step forward one event</summary>
        private bool _isSteppingForward;

        /// <summary>Stores latest observed market time</summary>
        private DateTime _latestMarketTime = DateTime.MinValue;

        /// <summary>
        /// Spark event recieved handler
        /// </summary>
        /// <param name="eventItem"></param>
        internal delegate void EventReceivedHandler(Spark.Event eventItem);

        /// <summary>
        /// ApiEventFeedReplay constructor
        /// </summary>
        /// <param name="fileName">Event feed file name</param>
        public ApiEventFeedReplay(string fileName)
        {
            FileName = fileName;
            IgnoreQuoteEvents = false;
        }

        /// <summary>
        /// Initiate data feed
        /// </summary>
        public override void Execute()
        {
            ApiEventReaderWriter reader = new ApiEventReaderWriter();
            reader.StreamFromFile(FileName, EventRecieved);
        }

        /// <summary>
        /// Pause replay
        /// </summary>
        public void Pause()
        {
            IsPaused = true;
        }

        /// <summary>
        /// Resume replay
        /// </summary>
        public void Resume()
        {
            IsPaused = false;
        }

        /// <summary>
        /// Step forward one event
        /// </summary>
        public void StepForward()
        {
            _isSteppingForward = true;
        }

        /// <summary>
        /// Fired each time an event is read from file
        /// </summary>
        /// <param name="eventItem"></param>
        private void EventRecieved(Spark.Event eventItem)
        {

            //Raise event
            if (Spark.TimeToDateTime(eventItem.Time) > _latestMarketTime) _latestMarketTime = Spark.TimeToDateTime(eventItem.Time);
            RaiseEvent(new EventFeedArgs(eventItem, _latestMarketTime));

            //Skip over waiting logic if quote event and ignoring quotes
            if ((IgnoreQuoteEvents) && (eventItem.Type == Spark.EVENT_QUOTE)) return;

            //Wait before playing next event while replay is pause
            while ((IsPaused) && (!_isSteppingForward))
            {
                System.Threading.Thread.Sleep(100);
            }
            if (_isSteppingForward) _isSteppingForward = false;

        }

    }

}
