using System;
using System.IO;
using System.Collections.Generic;

using Lune.Lexer;
using Lune.AST;
using Lune.AST.Nodes;

namespace Lune
{
    public class Program
    {
        private static Lexer.Lexer? lexer;
        private static Parser parser;

        /// <summary>
        /// Callback to let us know we have an error somewhere
        /// </summary>
        public static void ErrorCallback(Token token, string message) 
        {
            ErrorReporting.Error(token, message);
        }

        public static void Main(string[] args)
        {
            lexer = new Lexer.Lexer(File.ReadAllText("examples/expr.ln"));
            var tokens = lexer.Scan();

            foreach (var t in tokens)
            {
                Console.WriteLine(t);
            }

            parser = new Parser(tokens, ErrorCallback);
            ASTPrinter printer = new ASTPrinter();
            Expr? expr = parser.Parse();

            Console.WriteLine(printer.Print(expr));
        }
    }
}