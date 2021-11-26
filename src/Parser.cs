using static System.Console;

using System;
using Lune;
using Lune.Lexer;
using Lune.AST.Nodes;

using static Lune.Lexer.TokenType;

namespace Lune
{
    class Parser
    {
        private readonly List<Token> tokens = new List<Token>();
        private int current = 0;

        private readonly Action<Token, string> errorCallback;

        public Parser(List<Token> tokens, Action<Token, string> errorCallback)
        {
            this.tokens = tokens;

            // taken from https://github.com/Jay-Madden/SharpLox
            this.errorCallback = errorCallback;
        }

        #region Helper functions
        private bool isAtEnd() => peek().Type == TokenType.EOF;

        private Token peek()
        {
            return tokens[current];
        }

        private Token previous()
        {
            return tokens[current - 1];
        }

        private Token? consume(TokenType type, string message)
        {
            if (check(type))
                return advance();

            this.errorCallback(peek(), message);
            return null;
        }

        private Token advance()
        {
            if (!isAtEnd())
                current++;
            return previous();
        }

        private bool check(TokenType type)
        {
            if (isAtEnd()) return false;
            return peek().Type == type;
        }

        private bool matches(params TokenType[] types)
        {
            foreach (var t in types)
            {
                if (check(t))
                {
                    advance();
                    return true;
                }
            }

            return false;
        }
        #endregion

        private void Sychronize()
        {
            advance();
            while (!isAtEnd())
            {
                if (previous().Type == NewLine)
                    return;
                
                switch (peek().Type)
                {
                    case If:
                    case Else:
                    case For:
                    case _In:
                    case Of:
                    case While:
                    case Var:
                    case Proc:
                        return;
                }

                advance();
            }
        }

        private Expr ParseExpression()
        {
            // expression = equality
            return ParseEquality();
        }

        private Expr ParseEquality()
        {
            // equality = comparison ( ("!=" | "==") comparison)*
            Expr expr = ParseComparison();
            while (matches(BangEqual, EqualEqual))
            {
                var op = previous();
                var right = ParseComparison();
                expr = new Binary(expr, op, right);
            }

            return expr;
        }

        private Expr ParseComparison() 
        {
            // comparison = term ( (">" | ">=" | "<" | "<=") term)*
            Expr expr = ParseTerm();
            while (matches(Greater, GreaterEqual, Less, LessEqual))
            {
                var op = previous();
                var right = ParseTerm();
                expr = new Binary(expr, op, right);
            }

            return expr;
        }

        private Expr ParseTerm()
        {
            // term = factor ( ("+" | "-") factor)*
            Expr expr = ParseFactor();
            while (matches(Minus, Plus))
            {
                var op = previous();
                var right = ParseFactor();
                expr = new Binary(expr, op, right);
            }

            return expr;
        }

        private Expr ParseFactor()
        {
            // factor = unary ( ("*" | "/") unary)*
            Expr expr = ParseUnary();
            while(matches(Star, Slash))
            {
                var op = previous();
                var right = ParseUnary();
                expr = new Binary(expr, op, right);
            }

            return expr;
        }

        private Expr ParseUnary()
        {
            // unary = ("!" | "-") unary
            //       | primary
            if (matches(Bang, Minus)) 
            {
                var op = previous();
                var right = ParseUnary();
                return new Unary(op, right);
            }

            return ParsePrimary();
        }

        private Expr ParsePrimary()
        {
            if (matches(NumberLit, StringLit))
                return new Literal(previous().Literal);

            if (matches(LeftParen))
            {
                var expr = ParseExpression();
                consume(RightParen, "Expected ')' after expression");
                return new Grouping(expr);
            }

            errorCallback(peek(), "Expected an expression");
            throw new ParseException();
        }

        public Expr? Parse()
        {
            try
            {
                return ParseExpression();
            }
            catch (ParseException ex)
            {
                return null;
            }
        }
    }
}