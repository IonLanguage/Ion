using System;
using Ion.CodeGeneration;
using Ion.Core;
using Ion.SyntaxAnalysis;

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
            if (begin.Type == SyntaxAnalysis.TokenType.SymbolBlockL)
            {
                block.Type = BlockType.Default;
            }
            // Mark the block as short.
            else if (begin.Type == SyntaxAnalysis.TokenType.SymbolArrow)
            {
                block.Type = BlockType.Short;
            }
            // Otherwise, the block type could not be identified.
            else
            {
                throw new Exception("Unexpected block type");
            }

            // Capture the current token.
            Token token = context.Stream.Current;

            // While next token is not a block-closing token.
            while (token.Type != SyntaxAnalysis.TokenType.SymbolBlockR && block.Type != BlockType.Short)
            {
                // Returning a value.
                if (token.Type == SyntaxAnalysis.TokenType.KeywordReturn)
                {
                    // Invoke the return parser. It's okay if it returns null, as it will be emitted as void.
                    Expr returnExpr = new FunctionReturnParser().Parse(context);

                    // Assign the return expression to the block.
                    block.ReturnExpr = returnExpr;

                    // Exit the loop and return
                    break;
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

                // Ensure current token is a semi-colon.
                context.Stream.EnsureCurrent(SyntaxAnalysis.TokenType.SymbolSemiColon);

                // Skip over the semi-colon.
                context.Stream.Skip();

                // Get the new token for next parse.
                token = context.Stream.Current;
            }

            // Skip onto default block end or short block end.
            context.Stream.Skip();

            // Return the resulting block.
            return block;
        }
    }
}
