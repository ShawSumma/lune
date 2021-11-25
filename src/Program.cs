using System;
using Lune.Lexer;

namespace Lune
{
    public class Program
    {
        private static Lexer.Lexer lexer;

        public static void Main(string[] args)
        {
            lexer = new Lexer.Lexer("# a comment\nfor (x in y) { 5 * 4 }");
            var tokens = lexer.Scan();

            foreach (var t in tokens)
            {
                Console.WriteLine(t);
            }
        }
    }
}