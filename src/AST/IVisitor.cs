namespace Lune.AST
{
    public interface IVisitor
    {
        abstract void VisitLiteral(LiteralNode expr);
        abstract void VisitBinOp(BinOpNode expr);
        abstract void VisitGrouping(GroupingNode expr);
    }
}