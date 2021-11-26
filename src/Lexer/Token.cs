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
        And, Or, If, Else, For, In, While, Case, Of, Var, Proc,

        NewLine, EOF
    }

    public record Token(TokenType type, string lexeme, object? literal, int line)
    {
        public override string ToString() 
        {
            var items = new List<object>();
            if (this.lexeme != "") {
                items.Add(this.lexeme.Trim());
            }
            else if (this.literal != null) {
                items.Add(this.literal);
            }

            return $"{this.type}({string.Join(',', items)})";
        }
    }
}