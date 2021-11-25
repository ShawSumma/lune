using System;
using System.IO;
using System.Collections.Generic;

using Lune.Lexer;

namespace Lune
{
    public class Program
    {
        private static Lexer.Lexer? lexer;

        public static void Main(string[] args)
        {
            lexer = new Lexer.Lexer(File.ReadAllText("examples/ex.ln"));
            var tokens = lexer.Scan();

            foreach (var t in tokens)
            {
                Console.WriteLine(t);
            }
        }
    }
}