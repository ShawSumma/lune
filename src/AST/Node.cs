using System;

namespace Lune.AST
{
    public abstract record Node
    {
        public abstract T Accept<T>(ITreeVisitor<T> visitor);
    }
}