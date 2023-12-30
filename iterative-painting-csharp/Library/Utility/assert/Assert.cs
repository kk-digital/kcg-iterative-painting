#define DebugAssertLevel2


using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Utility
{
    public class Utils
    {
        public static void Assert(bool condition, 
        string message = "", [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null)
        {
#if ReleaseMode
            // empty for now
#elif DebugAssertLevel1
            // level 1 assertion does not crash the game
            // logs the error message and move on
            
            if (!condition)
            {
                // Combine all at the debug assert message
                string path = filePath.Remove(0, filePath.IndexOf("KCG"));
                KLog.LogError(message + " " + path + "  at " + caller + "()" + "  line: " + lineNumber);
                throw new Exception();
            }
            
#elif DebugAssertLevel2
            if (!condition)
            {
                // Combine all at the debug assert message
                string path = filePath.Remove(0, filePath.IndexOf("kcg"));
                Utility.KLog.LogError(message + " " + path + "  at " + caller + "()" + "  line: " + lineNumber);
                Debug.Assert(condition, message);
                Debug.Fail("Execution Failed");
                throw new Exception();
            }
#endif
        }

        public static void Assert(string message = "", [CallerLineNumber] int lineNumber = 0,
        [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null)
        {
#if ReleaseMode
            // empty for now
#elif DebugAssertLevel1
            // level 1 assertion does not crash the game
            // logs the error message and move on

            // Combine all at the debug assert message
            string path = filePath.Remove(0, filePath.IndexOf("kcg"));
            Utility.KLog.LogError(message + " " + path + "  at " + caller + "()" + "  line: " + lineNumber);
#elif DebugAssertLevel2
            // Combine all at the debug assert message
            string path = filePath.Remove(0, filePath.IndexOf("kcg"));
            Utility.KLog.LogError(message + " " + path + "  at " + caller + "()" + "  line: " + lineNumber);
            Debug.Assert(false);
            Debug.Fail("Execution Failed");
#endif
        }
    }
}
