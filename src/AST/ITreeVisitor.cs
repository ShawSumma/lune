using Lune.AST.Nodes;

namespace Lune.AST
{
    public interface ITreeVisitor<out T>
    {
        public T VisitLiteral(Literal expr);
        public T VisitBinOp(Binary expr);
        public T VisitUnary(Unary expr);
        public T VisitGrouping(Grouping expr);
    }
}