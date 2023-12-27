
using System.Diagnostics;

namespace Utility
{
    public static class KLog
    {
        // Todo: Divide instrumantation and logging into channels
        
        private static int _verbosity = 0;
        private static char[] _profileBuffer = new char[8192];
        private static int _profileBufferIndex = 0;
        private static char[] _debugBuffer = new char[8192];
        private static int _debugBufferIndex = 0;
        private static char[] _wardingBuffer = new char[8192];
        private static int _warningBufferIndex = 0;
        private static char[] _errorBuffer = new char[8192];
        private static int _errorBufferIndex = 0;

        public static void LogProfile(string msg, Stopwatch time)
        {
            string timeString = time.ElapsedMilliseconds + "ms";
            KLog.LogDebug(msg + " " + timeString);
        }


        public static void LogDebug(string msg)
        {

            Console.WriteLine(msg);
        }

        public static void LogWarning(string msg)
        {
            Console.WriteLine(msg);
        }

        public static void LogError(string msg)
        {
            Console.WriteLine(msg);
        }   
    }
}
