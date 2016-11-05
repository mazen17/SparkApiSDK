using System.Collections.Generic;

namespace SparkAPI.Common.Logging
{
    /// <summary>
    /// Threaded file write management system that buffers file write operations
    /// </summary>
    public class FileBufferList : Dictionary<string, FileBuffer>
    {

        //Private variables
        readonly System.Timers.Timer _writeTimer = null;

        /// <summary>Specifies whether a flush operation is currently being performed</summary>
        public bool IsFlushing { get; private set; }

        /// <summary>
        /// FileBufferList constructor  (can only be instanced via Common.Singleton.Instance())
        /// </summary>
        private FileBufferList()
        {
            _writeTimer = new System.Timers.Timer();
            _writeTimer.Elapsed += new System.Timers.ElapsedEventHandler(writeTimer_Elapsed);
            _writeTimer.Interval = 10000;
            _writeTimer.Start();
            _writeTimer.AutoReset = false;
        }

        /// <summary>
        /// Get instance of singleton FileBufferList
        /// </summary>
        public static FileBufferList Instance
        {
            get
            {
                return Design.Singleton<FileBufferList>.Instance;
            }
        }

        /// <summary>
        /// Add buffer to list
        /// </summary>
        /// <param name="outputFileName"></param>
        /// <param name="outputStyle"></param>
        /// <returns></returns>
        public FileBuffer Add(string outputFileName, FileBuffer.WriteStyle outputStyle)
        {
            FileBuffer buffer = this[outputFileName];
            buffer.OutputStyle = outputStyle;
            return buffer;
        }

        /// <summary>
        /// Triggered by writeTimer tick event
        /// </summary>
        private void writeTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Debug.WriteLine("writeTimer_Elapsed: " + DateTime.Now.ToString("HH:mm:ss.fff"));
            Flush();
            _writeTimer.Start();
        }

        /// <summary>
        /// Write specified text to file buffer identified by file name 
        /// </summary>
        /// <param name="fileName">Destination file name</param>
        /// <param name="text">Output text</param>
        public void Write(string fileName, string text)
        {
            this[fileName].Add(text);
        }

        /// <summary>
        /// Write specified text with terminating CRLF to file buffer identified by file name 
        /// </summary>
        /// <param name="fileName">Destination file name</param>
        /// <param name="text">Output text</param>
        public void WriteLine(string fileName, string text)
        {
            this[fileName].AddLine(text);
        }

        /// <summary>
        /// Creates or returns specified file buffer based on key
        /// </summary>
        public new FileBuffer this[string key]
        {
            get
            {
                FileBuffer bufferItem = null;
                if (!ContainsKey(key))
                {
                    bufferItem = new FileBuffer(key);
                    System.Collections.ICollection collection = (System.Collections.ICollection)this.Values;
                    lock (collection.SyncRoot)
                    {
                        if (!ContainsKey(key)) { Add(key, bufferItem); }
                    }
                }
                else
                {
                    bufferItem = base[key];
                }
                return bufferItem;
            }
            set
            {
                base[key] = value;
            }
        }

        /// <summary>
        /// Write the contents of each file buffer to file
        /// </summary>
        public void Flush()
        {
            Flush(false, 0);
        }

        /// <summary>
        /// Write the contents of each file buffer to file
        /// </summary>
        public void Flush(bool waitUntilComplete, int secondsToTimeout)
        {
            IsFlushing = true;
            System.Collections.ICollection collection = (System.Collections.ICollection)this.Values;
            lock (collection.SyncRoot)
            {
                foreach (FileBuffer fileBuffer in this.Values)
                {
                    if (fileBuffer.HasData) fileBuffer.WriteToFile();
                }
            }
            IsFlushing = false;
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            _writeTimer.Stop();
            do
            {
                System.Threading.Thread.Sleep(100);
            } while (IsFlushing);
            Flush();
        }

    }
}
