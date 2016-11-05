using System;
using System.Reflection;

namespace SparkAPI.Common.Design
{

    /// <summary>
    /// Generic singleton class instanced using the Instance property 
    /// </summary>
    /// <typeparam name="T">Class type</typeparam>
    /// <remarks>
    /// The Singleton is a design pattern used when you want to ensure that only one instance of an object
    /// is created in an application, even if it is to be accessed by many things. This is a generic singleton
    /// implementation that can be used for any class that requires a single instance.
    /// </remarks>
    public static class Singleton<T> where T : class
    {

        static private volatile T _instance;
        static private readonly object Lock = new object();

        /// <summary>
        /// Singleton constructor (inaccessible)
        /// </summary>
        static Singleton()
        {
        }

        /// <summary>
        /// Get singleton instance
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            ConstructorInfo constructor = null;

                            try
                            {
                                // Binding flags exclude public constructors
                                constructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null);
                            }
                            catch (Exception exception)
                            {
                                throw new Exception(exception.Message, exception);
                            }

                            if (constructor == null || constructor.IsAssembly)
                            {
                                // Also exclude internal constructors.
                                throw new Exception(string.Format("A private or protected constructor is missing for '{0}'.", typeof(T).Name));
                            }
                            _instance = (T)constructor.Invoke(null);
                        }
                    }

                return _instance;
            }
        }
    }

}
