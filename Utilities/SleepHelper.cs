using System.Threading;
using System.Runtime.InteropServices;

namespace Snake.Utilities
{
    internal static class SleepHelper
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtQueryTimerResolution(out uint MinimumResolution, out uint MaximumResolution, out uint CurrentResolution);

        private static readonly double LowestSleepThreshold;

        static SleepHelper()
        {
            uint min, max, current;
            NtQueryTimerResolution(out min, out max, out current);
            LowestSleepThreshold = 1.0 + (max / 10000.0);
        }

        /// <summary>Returns the current timer resolution in milliseconds</summary>
        internal static double GetCurrentResolution()
        {
            uint min, max;
            NtQueryTimerResolution(out min, out max, out uint current);
            return current / 10000.0;
        }

        /// <summary>Sleeps as long as possible without exceeding the specified period</summary>
        internal static void SleepForNoMoreThan(double milliseconds)
        {
            // Assumption is that Thread.Sleep(t) will sleep for at least (t), and at most (t + timerResolution)
            if (milliseconds < LowestSleepThreshold) return;

            var sleepTime = (int)(milliseconds - GetCurrentResolution());
            if (sleepTime > 0) 
                Thread.Sleep(sleepTime);
        }
    }
}