using System;
using Ion.CodeGeneration;
using Ion.Core;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class BlockParser : IParser<Block>
    {
        public Block Parse(ParserContext context)
        {
            // Capture current token. Either block start or arrow for anonymous functions.
            Token begin = context.Stream.Current;

            // Skip begin token.
            context.Stream.Skip();

            // Create the block.
            Block block = new Block();

            // Set the block as active in the symbol table.
            context.SymbolTable.activeBlock = block;

            // Mark the block as default.
            if (begin.Type == TokenType.SymbolBlockL)
            {
                block.Type = BlockType.Default;
            }
            // Mark the block as short.
            else if (begin.Type == TokenType.SymbolArrow)
            {
                block.Type = BlockType.Short;
            }
            // Otherwise, the block type could not be identified.
            else
            {
                throw new Exception("Unexpected block type");
            }

            // Begin the iteration.
            context.Stream.NextUntil(TokenType.SymbolBlockR, (Token token) =>
            {
                // Returning a value.
                if (token.Type == TokenType.KeywordReturn)
                {
                    // Invoke the return parser. It's okay if it returns null, as it will be emitted as void.
                    Expr returnExpr = new FunctionReturnParser().Parse(context);

                    // Assign the return expression to the block.
                    block.ReturnExpr = returnExpr;

                    // Return immediatly, signal to update the token buffer to the current token.
                    return true;
                }

                // Token must be a statement.
                Expr statement = new StatementParser().Parse(context);

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
