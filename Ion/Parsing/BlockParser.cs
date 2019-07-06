#nullable enable

using System;
using System.Collections.Generic;
using Ion.Generation;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class BlockParser : IParser<Block>
    {
        public Block Parse(ParserContext context)
        {
            // Ensure current token is block start.
            context.Stream.EnsureCurrent(TokenType.SymbolBraceL);

            // Skip begin token.
            context.Stream.Skip();

            // Create a construct buffer for the reutrn construct.
            Construct? returnConstruct = null;

            // Create the statement buffer list.
            List<Construct> statements = new List<Construct>();

            // Begin the iteration.
            context.Stream.NextUntil(TokenType.SymbolBraceR, (Token token) =>
            {
                // Returning a value.
                if (token.Type == TokenType.KeywordReturn)
                {
                    // Invoke the return parser. It's okay if it returns null, as it will be emitted as void.
                    Construct returnExpr = new FunctionReturnParser().Parse(context);

                    // Assign the return expression to the block.
                    returnConstruct = returnExpr;

                    // Return immediatly, signal to update the token buffer to the current token.
                    return true;
                }

                // Token must be a statement.
                Construct statement = new StatementParser().Parse(context);

                // Ensure statement was successfully parsed.
                if (statement == null)
                {
                    throw new Exception("Unexpected statement to be null");
                }

                // Append the parsed statement to the block's statement list.
                statements.Add(statement);

                // Ensure current token is a semi-colon, if previous statement did not parse a block.
                if (statement.ConstructType != ConstructType.Block)
                {
                    // Ensure semi-colon token.
                    context.Stream.EnsureCurrent(TokenType.SymbolSemiColon);

                    // Skip over the semi-colon.
                    context.Stream.Skip();
                }

                // Signal to update the token buffer with the current token.
                return true;
            });

            // Skip onto default block end or short block end.
            context.Stream.Skip();

            // TODO: Identifier?
            // Create the block.
            Block block = new Block("entry", statements.ToArray(), returnConstruct);

            // Return the resulting block.
            return block;
        }
    }
}
