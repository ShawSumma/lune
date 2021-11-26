using static System.Console;

using System;
using Lune.Lexer;

namespace Lune.AST.Nodes
{
    /// <summary>
    /// Base expression structure.
    /// </summary>
    public abstract record Expr : Node
    {

    }

    // 5 or "hello"
    public record Literal(object value) : Expr
    {
        public override T Accept<T>(ITreeVisitor<T> visitor)
        {
            return visitor.VisitLiteral(this);
        }
    }

    // 1 + 2
    public record BinOp(Expr left, Token op, Expr right) : Expr
    {
        public override T Accept<T>(ITreeVisitor<T> visitor)
        {
            return visitor.VisitBinOp(this);
        }
    }

    // (1 + 2)
    public record Grouping(Expr expr) : Expr
    {
        public override T Accept<T>(ITreeVisitor<T> visitor)
        {
            return visitor.VisitGrouping(this);
        }
    }
}