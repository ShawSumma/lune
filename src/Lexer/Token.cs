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

        // One or two character tokens
        And, Or,

        Bang, BangEqual,
        Equal, EqualEqual,
        Greater, GreaterEqual,
        Less, LessEqual,

        // Literals
        Identifier, StringLit, NumberLit,

        // Keywords
        If, Else, For, In, While, Var, Proc,

        EOF
    }

    public class Token
    {
        public readonly TokenType type;
        public readonly string lexeme;
        public readonly object? literal;
        public readonly int line;

        public Token(TokenType type, string lexeme, object? literal, int line)
        {
            this.type = type;
            this.lexeme = lexeme;
            this.literal = literal;
            this.line = line;
        }

        public override string ToString() => $"{this.type} {this.lexeme} {this.literal}";
    }
}