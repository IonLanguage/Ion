using System;
using System.Collections.Generic;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;
using Type = Ion.CodeGeneration.Type;

namespace Ion.Parsing
{
    public class ArrayExprParser : IExprParser<ArrayExpr>
    {
        protected readonly ITypeEmitter type;

        public ArrayExprParser(ITypeEmitter type)
        {
            this.type = type;
        }

        public ArrayExpr Parse(ParserContext context)
        {
            // Ensure current token is bracket start.
            context.Stream.EnsureCurrent(TokenType.SymbolBracketL);

            // Skip bracket start token.
            context.Stream.Skip();

            // Create the value buffer list.
            List<Expr> values = new List<Expr>();

            // Begin parsing values.
            context.Stream.NextUntil(TokenType.SymbolBracketR, (Token token) =>
            {
                // Invoke expression parser.
                Expr value = new ExprParser().Parse(context);

                // Add the captured expression to the list.
                values.Add(value);

                // Capture current token.
                Token current = context.Stream.Get();

                // Ensure current token is either semi-colon or bracket end.
                if (current.Type != TokenType.SymbolComma && current.Type != TokenType.SymbolBracketR)
                {
                    throw new Exception($"Expected token in array expression to be of type comma or bracket end, but got '{current.Type}'");
                }

                // Signal to update iterator to current token, since parsers where invoked.
                return true;
            });

            // Ensure current token is bracket end.
            context.Stream.EnsureCurrent(TokenType.SymbolBracketR);

            // Skip bracket end token.
            context.Stream.Skip();

            // Create the resulting array construct.
            ArrayExpr arrayExpr = new ArrayExpr(this.type, values.ToArray());

            // Return the resulting array construct.
            return arrayExpr;
        }
    }
}
