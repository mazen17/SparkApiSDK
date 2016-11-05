using System.Diagnostics;

namespace SparkAPI.Common.Diagnostics
{

    /// <summary>
    /// Provides a mechanism to benchmark the amount of time something takes. The benchmark will automatically
    /// start when instanced.
    /// </summary>
    public class Benchmark
    {

        /// <summary>Internal stopwatch for tracking benchmark</summary>
        private readonly Stopwatch _stopWatch = new Stopwatch();

        /// <summary>Benchmark name</summary>
        public string Name { get; private set; }

        /// <summary>
        /// Benchmark constructor
        /// </summary>
        /// <param name="name">Benchmark name</param>
        public Benchmark(string name)
        {
            Name = name;
            _stopWatch.Start();
        }

        /// <summary>
        /// Seconds elapsed since benchmark initiated
        /// </summary>
        public double Elapsed
        {
            get { return _stopWatch.Elapsed.TotalSeconds; }
        }

        /// <summary>
        /// Writes elapsed time to Debug
        /// </summary>
        public void WriteToConsole()
        {
            Debug.WriteLine(Name + ":\t" + Elapsed);
        }

    }
}
