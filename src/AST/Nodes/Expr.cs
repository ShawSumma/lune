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

    public record Unary(Token Op, Expr Right) : Expr
    {
        public override T Accept<T>(ITreeVisitor<T> visitor)
        {
            return visitor.VisitUnary(this);
        }
    }

    public record Literal(object Value) : Expr
    {
        public override T Accept<T>(ITreeVisitor<T> visitor)
        {
            return visitor.VisitLiteral(this);
        }
    }

    public record Binary(Expr Left, Token Op, Expr Right) : Expr
    {
        public override T Accept<T>(ITreeVisitor<T> visitor)
        {
            return visitor.VisitBinOp(this);
        }
    }

    public record Grouping(Expr Expr) : Expr
    {
        public override T Accept<T>(ITreeVisitor<T> visitor)
        {
            return visitor.VisitGrouping(this);
        }
    }
}