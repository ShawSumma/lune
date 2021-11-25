using System;
using Lune.Lexer;

namespace Lune
{
    public class Program
    {
        private static Lexer.Lexer lexer;

        public static void Main(string[] args)
        {
            lexer = new Lexer.Lexer("(20 * 2) + 4");
            var tokens = lexer.Scan();

            foreach (var t in tokens)
            {
                Console.WriteLine(t);
            }
        }
    }
}