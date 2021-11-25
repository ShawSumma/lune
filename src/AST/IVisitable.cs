using static System.Console;

using System;
using Lune;
using Lune.Lexer;

namespace Lune.AST
{
    /// <summary>
    /// Nodes that can be visited implement IVisitable.
    /// </summary>   
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}