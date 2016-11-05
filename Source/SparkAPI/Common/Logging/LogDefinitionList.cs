using System.Collections.Generic;

namespace SparkAPI.Common.Logging
{

    /// <summary>
    /// 
    /// </summary>
    public class LogDefinitionList : Dictionary<string, LogDefinition>
    {

        /// <summary>
        /// LogDefinitionList constructor  (can only be instanced via Common.Singleton.Instance())
        /// </summary>
        private LogDefinitionList()
        {
        }

        /// <summary>
        /// Get instance of singleton LogDefinitionList
        /// </summary>
        public static LogDefinitionList Instance
        {
            get
            {
                return Design.Singleton<LogDefinitionList>.Instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        public static void WriteLine(string name, string text)
        {
            LogDefinitionList.Instance[name].WriteLine(text);
        }

        /// <summary>
        /// Flush log buffers
        /// </summary>
        public void Flush()
        {
            FileBufferList.Instance.Flush();
        }

    }
}
