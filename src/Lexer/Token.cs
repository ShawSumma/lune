using System.Collections.Generic;

namespace Lune.Lexer
{
    /// <summary>
    /// Enumeration of all token types
    /// </summary>
    public enum TokenType
    {
        // Single character tokens
        LeftParen, RightParen, LeftBrace, RightBrace,
        Comma, Dot, Plus, Minus, Star, Slash, Colon, Semicolon,

        Bang, BangEqual,
        Equal, EqualEqual,
        Greater, GreaterEqual,
        Less, LessEqual,

        // Literals
        Identifier, StringLit, CharLit, NumberLit,

        // Keywords
        And, Or, If, Else, For, _In, While, Case, Of, Var, Proc,

        NewLine, EOF
    }

    public record Token(TokenType Type, string Lexeme, object? Literal, int Line)
    {
        public override string ToString() => $"{Type}: {Lexeme} ('{Literal}')";
    }
}