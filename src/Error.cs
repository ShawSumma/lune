using static System.Console;

using Lune.Lexer;

namespace Lune
{
    class ParseException : Exception
    {
        public ParseException() {}
        public ParseException(string message) : base(message) {}
    }

    class ErrorReporting
    {
        /// Have we gotten an error while scanning?
        public static bool HadError;

        /// <summary>
        /// Display a friendly message saying where we encountered an error
        /// </summary>
        /// <param name="line"></param>
        /// <param name="message"></param>
        public static void Error(int line, string message) => report(line, "", message);

        public static void Error(Token token, string message)
        {
            if (token.Type == TokenType.EOF)
            {
                report(token.Line, " at end", message);
            }
            else
            {
                report(token.Line, $"at '{token.Lexeme}'", message);
            }
        }

        private static void report(int line, string where, string message)
        {
            WriteLine($"error at L:{line} {(where != "" ? where : "")}: {message}");
            ErrorReporting.HadError = true;
        }
    }
}