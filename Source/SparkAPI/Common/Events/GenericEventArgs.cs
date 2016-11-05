using System;

namespace SparkAPI.Common.Events
{

    /// <summary>
    /// EventArgs implementation that supports a generic value and a time stamp
    /// </summary>
    /// <typeparam name="T">Generic argument type</typeparam>
    /// <remarks>
    /// When an event requires an object passed, we normally have to create class that inherits the EventArgs class
    /// and exposes the extra parameters. The GenericEventArgs class simplifies this process by exposing a generic type
    /// event.
    /// </remarks>
    public class GenericEventArgs<T> : EventArgs
    {

        /// <summary>Event time stamp</summary>
        public DateTime TimeStamp { get; private set; }

        /// <summary>Event argument value</summary>
        public T Value { get; private set; }

        /// <summary>
        /// EventArgs constructor
        /// </summary>
        /// <param name="timeStamp">Event time stamp</param>
        /// <param name="value">Event argument value</param>
        public GenericEventArgs(DateTime timeStamp, T value)
        {
            TimeStamp = timeStamp;
            Value = value;
        }

    }

}
