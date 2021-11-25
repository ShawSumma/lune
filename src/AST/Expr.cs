using static System.Console;

using System;
using Lune.Lexer;

namespace Lune.AST
{
    /// <summary>
    /// Base expression structure.
    /// Internally its a data structure, but C# structs cant be inherited from.
    /// </summary>
    public class Expr
    {

    }

    public class LiteralNode : Expr, IVisitable
    {
        public readonly object value;

        public LiteralNode(object value)
        {
            this.value = value;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitLiteral(this);
        }
    }

    public class BinOpNode : Expr, IVisitable
    {
        public readonly Expr left;
        public readonly Token op;
        public readonly Expr right;

        public BinOpNode(Expr left, Token op, Expr right)
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitBinOp(this);
        }
    }

    public class GroupingNode : Expr, IVisitable
    {
        public readonly Expr expr;

        public GroupingNode(Expr expr)
        {
            this.expr = expr;
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitGrouping(this);
        }
    }
}