using System;
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

            // Create the block.
            Block block = new Block();

            // Set the block as active in the symbol table.
            context.SymbolTable.activeBlock = block;

            // Begin the iteration.
            context.Stream.NextUntil(TokenType.SymbolBlockR, (Token token) =>
            {
                // Returning a value.
                if (token.Type == TokenType.KeywordReturn)
                {
                    // Invoke the return parser. It's okay if it returns null, as it will be emitted as void.
                    Construct returnExpr = new FunctionReturnParser().Parse(context);

                    // Assign the return expression to the block.
                    block.ReturnConstruct = returnExpr;

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

                // Append the parsed statement to the block's expression list.
                block.Expressions.Add(statement);

                // Ensure current token is a semi-colon, if previous statement did not parse a block.
                if (statement.ExprType != ExprType.If)
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

            // Return the resulting block.
            return block;
        }
    }
}
