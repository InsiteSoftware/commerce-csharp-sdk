using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace CommerceApiSDK.Utils
{
    public static class Profiler
    {
        static readonly ConcurrentDictionary<string, Stopwatch> watches = new ConcurrentDictionary<string, Stopwatch>();

        public static void Start(object view)
        {
#if DEBUG
            Start(view.GetType().Name);
#endif
        }

        public static void Start(string tag)
        {
#if DEBUG
            Console.WriteLine("Starting Stopwatch {0}", tag);

            Stopwatch watch = watches[tag] = new Stopwatch();
            watch.Start();
#endif
        }

        public static void Stop(string tag)
        {
#if DEBUG
            Stopwatch watch;
            if (watches.TryGetValue(tag, out watch))
            {
                Console.WriteLine("Stop of Stopwatch {0} . It took {1}", tag, watch.Elapsed);
            }
#endif
        }
    }
}
