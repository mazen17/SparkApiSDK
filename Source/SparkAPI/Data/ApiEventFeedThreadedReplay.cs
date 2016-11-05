using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class ApiEventFeedThreadedReplay : ApiEventFeedBase
    {

        /// <summary>Event feed file name</summary>
        internal string FileName { get; private set; }

        /// <summary>Specifies whether the replay is currently paused</summary>
        public bool IsPaused { get; private set; }

        /// <summary>Maximum event queue size reached during processing</summary>
        public int MaxQueueDepthReached { get; private set; }

        /// <summary>Specifies whether the replay should step forward one event</summary>
        private bool _isSteppingForward;

        /// <summary>Stores latest observed market time</summary>
        private DateTime _latestMarketTime = DateTime.MinValue;

        /// <summary>Concurrent event queue for transferring events between producer and consumer</summary>
        private BlockingCollection<Spark.Event> _eventQueue;

        /// <summary>
        /// Spark event recieved handler
        /// </summary>
        /// <param name="eventItem"></param>
        internal delegate void EventReceivedHandler(Spark.Event eventItem);

        /// <summary>
        /// ApiEventFeedReplay constructor
        /// </summary>
        /// <param name="fileName">Event feed file name</param>
        public ApiEventFeedThreadedReplay(string fileName)
        {
            FileName = fileName;
            IgnoreQuoteEvents = false;
        }

        /// <summary>
        /// Initiate data feed
        /// </summary>
        public override void Execute()
        {
            
            //Initiate reader task
            _eventQueue = new BlockingCollection<Spark.Event>();
            var readerTask = new Task(ExecuteEventReader); 
            readerTask.Start();

            //Initiate processor task
            var processor = new Task(ExecuteEventProcessor);
            processor.Start();

            //Wait until reading events is complete and mark as finished
            readerTask.Wait();
            _eventQueue.CompleteAdding();

            //Wait until event processing is complete
            processor.Wait();

        }

        /// <summary>
        /// Initiate event reader
        /// </summary>
        private void ExecuteEventReader()
        {
            ApiEventReaderWriter reader = new ApiEventReaderWriter();
            reader.StreamFromFile(FileName, EventRecieved);
        }

        /// <summary>
        /// Initiate event processor
        /// </summary>
        private void ExecuteEventProcessor()
        {
            foreach (Spark.Event eventItem in _eventQueue.GetConsumingEnumerable())
            {
                ProcessEvent(eventItem);
                int currentQueueDepth = _eventQueue.Count;
                if (currentQueueDepth > MaxQueueDepthReached) MaxQueueDepthReached = currentQueueDepth;
            }
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
            _eventQueue.Add(eventItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventItem"></param>
        private void ProcessEvent(Spark.Event eventItem)
        {

            //Skip over waiting logic if quote event and ignoring quotes
            if ((IgnoreQuoteEvents) && (eventItem.Type == Spark.EVENT_QUOTE)) return;

            //Raise event
            DateTime sparkEventTime = ApiFunctions.DateTimeFromUnixTimestampSeconds(eventItem.Time);
            if (sparkEventTime > _latestMarketTime) _latestMarketTime = sparkEventTime;
            RaiseEvent(new EventFeedArgs(eventItem, _latestMarketTime));

            //Wait before playing next event while replay is pause
            while ((IsPaused) && (!_isSteppingForward))
            {
                System.Threading.Thread.Sleep(100);
            }
            if (_isSteppingForward) _isSteppingForward = false;

        }

    }

}
