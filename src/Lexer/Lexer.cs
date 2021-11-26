using static System.Console;

using System;
using System.Collections.Generic;

using static Lune.Lexer.TokenType;
using static Lune.ErrorReporting;

namespace Lune.Lexer
{
    /// <summary>
    /// Lexical analyzer for Rune.
    /// </summary>
    public class Lexer
    {
        private int start = 0, current = 0;
        private int line = 1;
        private bool inCommentBlock;

        private readonly string source;
        private List<Token> tokens = new List<Token>();

        // Mapping of reserved words and their tokens
        private Dictionary<string, TokenType> reserved = new Dictionary<string, TokenType>();

        // Initializer
        public Lexer(string source)
        {
            this.source = source;

            this.reserved = new Dictionary<string, TokenType>
            {
                ["and"] = And,
                ["or"] = Or,
                ["if"] = If,
                ["else"] = Else,
                ["for"] = For,
                ["in"] = TokenType.In,
                ["while"] = While,
                ["case"] = Case,
                ["of"] = Of,
                ["var"] = Var,
                ["proc"] = Proc,
            };
        }

        private bool isAtEnd() => current >= source.Length;

        #region Helper functions
        private char advance()
        {
            //Console.WriteLine($"start = {start}, current = {current}");

            if (!isAtEnd())
                return source[current++];
            return '\0';
        }

        private char peek()
        {
            if (isAtEnd()) return '\0';
            return source[current];
        }

        private char peekNext(int ahead = 1)
        {
            if (current + ahead >= source.Length)
                return '\0';
            return source[current + ahead];
        }

        private bool matches(char expected)
        {
            if (isAtEnd())
                return false;

            if (source[current] != expected)
                return false;

            current++;
            return true;
        }
        #endregion

        private void addToken(TokenType type, object? literal = null)
        {
            //Console.WriteLine($"start = {start}, current = {current}");

            var text = source.Substring(start, current - start);
            tokens.Add(new Token(type, "", literal, line));
        }

        /// <summary>
        /// Scan through until we reach the end of the string, and emit tokens
        /// </summary>
        /// <returns>List of all produced tokens</returns>
        public List<Token> Scan()
        {
            while (!isAtEnd())
            {
                // We are at the beginning of the next lexeme
                start = current;
                this.scanToken();
            }

            tokens.Add(new Token(TokenType.EOF, "", null, line));

            // As a kind of workaround to 01, check if the first token is a newline and remove it
            if (tokens[0].type == NewLine)
            {
                tokens.Remove(tokens[0]);
            }

            return tokens;
        }

        private void scanIdentifier()
        {
            while (Char.IsLetterOrDigit(peek()) || matches('_'))
                advance();

            var identifier = source.Substring(start, current - start);


            // Check if reserved keyword exists
            if (reserved.ContainsKey(identifier))
            {
                var tokenType = reserved[identifier];
                addToken(tokenType);
            }
            else
            {
                addToken(Identifier, identifier);
            }
        }

        private void scanNumber()
        {
            while (Char.IsDigit(peek()))
                advance();

            if (peek() == '.' && Char.IsDigit(peekNext()))
            {
                advance();

                while (Char.IsDigit(peek()))
                    advance();
            }

            var value = source.Substring(start, current - start);
            addToken(NumberLit, Double.Parse(value));
        }

        private void scanString()
        {
            while (peek() != '"' && !isAtEnd())
            {
                if (peek() == '\n')
                    line++;

                advance();
            }

            if (isAtEnd())
            {
                Error(line, "Unterminated string literal");
                return;
            }

            // Closing quote
            advance();

            // Remove the surrounding quotes
            string value = source.Substring(start + 1, current - start - 2);
            addToken(StringLit, value);
        }

        private void scanToken()
        {
            char c = advance();
            switch (c)
            {
                case '(': addToken(LeftParen); break;
                case ')': addToken(RightParen); break;
                case '{': addToken(LeftBrace); break;
                case '}': addToken(RightBrace); break;
                case ',': addToken(Comma); break;
                case '.': addToken(Dot); break;
                case '+': addToken(Plus); break;
                case '-': addToken(Minus); break;
                case '*': addToken(Star); break;
                case '/': addToken(Slash); break;
                case ':': addToken(Colon); break;
                case '!': addToken(matches('=') ? BangEqual : Bang); break;
                case '=': addToken(matches('=') ? EqualEqual : Equal); break;
                case '<': addToken(matches('=') ? LessEqual : Less); break;
                case '>': addToken(matches('=') ? GreaterEqual : Greater); break;
                case '"': scanString(); break;
                case '\\': break; // line continuation
                case ' ':

                case '\r':
                case '\t':
                    // Ignore whitespace
                    break;
                case '\n':
                    if (peekNext(ahead: -2) != '\\') {
                        // FIXME: 01: Kinda works but it still appends NewLine even if the line is actually empty
                        if (peek() != ' ' && tokens.Count == 0 || tokens[tokens.Count - 1].type != NewLine)
                        {
                            addToken(NewLine);
                        }
                        line++;
                    }
                    break;
                case '|':
                    if (matches('#'))
                    {
                        inCommentBlock = false;
                    }
                    break;
                case '#':
                    if (matches('|'))
                    {
                        // Multiline comment block
                        inCommentBlock = true;
                    }

                    if (!inCommentBlock)
                    {
                        while (peek() != '\n' && !isAtEnd()) advance();
                        if (peek() == '\n') advance();
                    }

                    break;
                default:
                    if (!inCommentBlock)
                    {
                        if (Char.IsDigit(c))
                        {
                            scanNumber();
                        }
                        else if (Char.IsLetter(c) || c == '_')
                        {
                            scanIdentifier();
                        }
                        else
                        {
                            Error(line, $"Unexpected character '{c}'");
                        }
                    }
                    else
                    {
                        // Inside a comment block, we ignore everything
                        while (peek() != '|' && peekNext() != '#' && !isAtEnd())
                            advance();
                    }
                    break;
            }
        }
    }
}