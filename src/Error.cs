using static System.Console;

namespace Lune
{
    class ErrorReporting
    {
        /// Have we gotten an error while scanning?
        public static bool HadError;

        /// <summary>
        /// Display a friendly message saying where we encountered an error
        /// </summary>
        /// <param name="line"></param>
        /// <param name="message"></param>
        public static void Error(int line, string message) => Report(line, "", message);

        private static void Report(int line, string where, string message)
        {
            WriteLine($"error at L:{line}{(where != "" ? " [" + where + "]": "")}: {message}");
            ErrorReporting.HadError = true;
        }
    }
}