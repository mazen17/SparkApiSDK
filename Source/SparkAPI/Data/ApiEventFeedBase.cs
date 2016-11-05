using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparkAPI.Market;

namespace SparkAPI.Data
{
    
    /// <summary>
    /// Base class used by all derived event feed classes
    /// </summary>
    /// <remarks>
    /// A base class is implemented to avoid duplication of common code across the two types of event feed (live and replay). 
    /// The derived classes call the RaiseEvent method in the base class because the OnEvent event can only be called from 
    /// the class that specifies it (in this case the base class). We can't call OnEvent from the classes that inherit ApiEventFeedBase
    /// even though they inherit all it's members.
    /// </remarks>
    public abstract class ApiEventFeedBase
    {

        /// <summary>Dictionary of securities to be linked to the event feed</summary>
        public Dictionary<string, Security> Securities { get; private set; }

        /// <summary>Executed when event feed recieves a Spark event</summary>
        internal event EventHandler<EventFeedArgs> OnEvent;

        /// <summary>Specifies whether event feed should should skip quote events</summary>
        /// <remarks>Quote events are not required by the Security class and can be skipped to improve performance</remarks>
        public bool IgnoreQuoteEvents { get; set; }

        /// <summary>Specifies whether event feed should automatically create securities in the dictionary if not found</summary>
        /// <remarks>This setting facilitates a full-market replay without having to specifiy the entire universe of security symbols</remarks>
        public bool AutoGenerateSecurities { get; set; }

        /// <summary>Total number of events processed by the event feed</summary>
        public int TotalEventsProcessed { get; private set; }

        /// <summary>
        /// ApiEventFeedBase constructor
        /// </summary>
        protected ApiEventFeedBase()
        {
            Securities = new Dictionary<string, Security>();
        }

        /// <summary>
        /// Initiate the event feed
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Raise the specified event
        /// </summary>
        /// <param name="eventFeedArgs">Event arguments</param>
        internal void RaiseEvent(EventFeedArgs eventFeedArgs)
        {

            //Skip if quote event and quote updates are being ignored
            if ((IgnoreQuoteEvents) && (eventFeedArgs.Event.Type == Spark.EVENT_QUOTE)) return;
            
            //Raise direct event if feed handler is assigned
            TotalEventsProcessed++;
            if (OnEvent != null) OnEvent(this, eventFeedArgs);

            //Raise event for security if in the dictionary
            Security security;
            if (!Securities.TryGetValue(eventFeedArgs.Symbol, out security))
            {
                if (AutoGenerateSecurities) security = AddSecurity(eventFeedArgs.Symbol);
            }
            if (security != null) security.EventReceived(this, eventFeedArgs); 

        }

        /// <summary>
        /// Add a security to the feed so it receives events
        /// </summary>
        /// <param name="security">Security</param>
        public void AddSecurity(Security security)
        {
            Securities.Add(security.Symbol, security);
        }

        /// <summary>
        /// Create a new security and add it to the feed so it receives events
        /// </summary>
        /// <param name="securitySymbol">Security symbol</param>
        public Security AddSecurity(string securitySymbol)
        {
            Security result = new Security(securitySymbol);
            Securities.Add(securitySymbol, result);
            return result;
        }

        /// <summary>
        /// Remove security to the feed so it no longer receives events
        /// </summary>
        /// <param name="security">Security</param>
        public void RemoveSecurity(Security security)
        {
            Securities.Remove(security.Symbol);
        }

        /// <summary>
        /// Remove security to the feed so it no longer receives events
        /// </summary>
        /// <param name="securitySymbol">Security symbol</param>
        public void RemoveSecurity(string securitySymbol)
        {
            if (Securities.ContainsKey(securitySymbol)) Securities.Remove(securitySymbol);
        }

    }

}
