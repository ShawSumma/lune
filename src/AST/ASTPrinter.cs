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

        public string VisitBinOp(BinOp expr)
        {
            return parenthesize(expr.op.lexeme, expr.left, expr.right);
        }

        public string VisitGrouping(Grouping expr)
        {
            return parenthesize("group", expr.expr);
        }

        public string VisitLiteral(Literal expr)
        {
            return expr.value switch
            {
                null   => "null",
                string => $"\"{expr.value}\"",
                char   => $"'{expr.value}'",
                _ => $"{expr.value}"
            };
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