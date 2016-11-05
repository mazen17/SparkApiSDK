using System;
using System.Diagnostics;
using SparkAPI.Common.Design;

namespace SparkAPI.Data
{

    /// <summary>
    /// Spark connection status
    /// </summary>
    internal enum SparkConnectionStatus
    {
        Disconnected = 0,
        Connecting = Spark.SPARK_CONNECTING,
        Connected = Spark.SPARK_CONNECTED,
        LoggedIn = Spark.SPARK_LOGGED_IN
    }

    /// <summary>
    /// Controls the initialisation and connection management to the underlying Spark API
    /// </summary>
    internal class ApiControl
    {

        private readonly object _syncLock = new Object();
        private bool _isInitiated = false;

        /// <summary>
        /// APIControl constructor (can only be instanced via Common.Singleton.Instance())
        /// </summary>
        private ApiControl()
        {
        }

        /// <summary>
        /// Get instance of singleton APIControl
        /// </summary>
        internal static ApiControl Instance
        {
            get
            {
                return Singleton<ApiControl>.Instance;
            }
        }

        /// <summary>
        /// Returns Spark connection status
        /// </summary>
        internal SparkConnectionStatus ConnectionStatus
        {
            get
            {
                return (SparkConnectionStatus)Spark.GetConnectionStatus();
            }
        }

        /// <summary>
        /// Initialise Spark component
        /// </summary>
        /// <remarks>
        /// The Spark API must be initalised before use otherwise many functions will not work correctly, but it won't throw an exception 
        /// and will just return an invalid result. 
        /// </remarks>
        internal void Initialise()
        {
            lock (_syncLock)
            {
                if (!_isInitiated)
                {
                    Spark.Init();
                    Spark.SetOptions(Spark.OPTION_ALL_EXCHANGE_EVENTS);
                    Spark.SetCacheDir(@"D:\Temp", 4000);
                    _isInitiated = true;
                }
            }
        }

        /// <summary>
        /// Open connection to Spark API
        /// </summary>
        /// <returns></returns>
        internal bool Connect()
        {
            bool result = true;
            lock (_syncLock)
            {
                try
                {

                    if (ConnectionStatus == SparkConnectionStatus.Disconnected)
                    {

                        //Instance Spark object
                        Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "\t" + "Initiating Spark connection...");
                        Initialise();

                        //Connect to Spark server
                        ApiCredentials credentials = ApiCredentials.LoadFromFile();
                        if (credentials == null) throw new ApplicationException("Spark API security credentials not available");
                        Spark.SetLogin(credentials.Username, credentials.Password);
                        if (!Spark.Connect())
                        {
                            Debug.WriteLine("Can't connect: {0}", Spark.DescribeError(Spark.GetLastError()));
                            result = false;
                        }
                        Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "\t" + "Spark connection initiated");

                        //Wait to allow Spark API to data access
                        System.Threading.Thread.Sleep(1000);

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ERROR: APIControl - ", ex.Message);
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Close connection to Spark API
        /// </summary>
        internal void Disconnect()
        {
            lock (_syncLock)
            {
                if (ConnectionStatus != SparkConnectionStatus.Disconnected)
                {
                    Spark.Disconnect();
                }
                Spark.Exit();
                _isInitiated = false;
            }
        }

    }
}
