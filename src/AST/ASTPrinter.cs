using System;
using System.Text;
using Lune.AST.Nodes;

namespace Lune.AST
{
    /// <summary>
    /// Walks the AST and pretty prints it out
    /// </summary>
    public class ASTPrinter : ITreeVisitor<string>
    {
        private List<string> astStrings = new List<string>();

        public string VisitBinOp(Binary expr)
        {
            System.Console.WriteLine("Op: " + expr.Op.Lexeme);
            return parenthesize(expr.Op.Lexeme, expr.Left, expr.Right);
        }

        public string VisitGrouping(Grouping expr)
        {
            return parenthesize("group", expr.Expr);
        }

        public string VisitLiteral(Literal expr)
        {
            return expr.Value switch
            {
                null   => "null",
                string => $"\"{expr.Value}\"",
                char   => $"'{expr.Value}'",
                _ => $"{expr.Value}"
            };
        }

        public string VisitUnary(Unary expr)
        {
            return parenthesize(expr.Op.Lexeme, expr.Right);
        }

        public string Print(Expr expr)
        {
            return expr.Accept(this);
        }

        private string parenthesize(string name, params Expr[] exprs)
        {
            var builder = new StringBuilder();
            builder.Append($"({name}");
            
            foreach (var expr in exprs)
            {
                builder.Append(" ");
                builder.Append(expr.Accept(this));
            }
            builder.Append(")");

            return builder.ToString();
        }
    }
}