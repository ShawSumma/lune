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

        public static void Main(string[] args)
        {
            lexer = new Lexer.Lexer(File.ReadAllText("examples/vars.ln"));
            var tokens = lexer.Scan();

            foreach (var t in tokens)
            {
                Console.WriteLine(t);
            }

            ASTPrinter printer = new ASTPrinter();
            Console.WriteLine(
                printer.VisitBinOp(
                    new BinOp(
                        new Literal("hello"),
                        new Token(TokenType.Plus, "+", null, 0),
                        new Literal(4)
                )
            ));


        }
    }
}