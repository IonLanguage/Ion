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
            // Capture current token. Either '{' or '=>' for anonymous functions.
            Token begin = context.Stream.Get();

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

            // Capture the current token.
            Token token = context.Stream.Get();

            // While next token is not a block-closing token.
            while (token.Type != TokenType.SymbolBlockR && block.Type != BlockType.Short)
            {
                // Returning a value.
                if (token.Type == TokenType.KeywordReturn)
                {
                    // Invoke the return parser. It's okay if it returns null, as it will be emitted as void.
                    Expr returnExpr = new FunctionReturnParser().Parse(context);

                    // Assign the return expression to the block.
                    block.ReturnExpr = returnExpr;

                    // Exit the loop and return
                    break;
                }

                // Token must be an expression.
                Expr expr = new PrimaryExprParser().Parse(context);

                // Ensure expression was successfully parsed.
                if (expr == null)
                {
                    throw new Exception("Unexpected expression to be null");
                }

                // Append the parsed expression to the block's expression list.
                block.Expressions.Add(expr);

                // Ensure current token is a semi-colon.
                context.Stream.EnsureCurrent(TokenType.SymbolSemiColon);

                // Skip over the semi-colon.
                context.Stream.Skip();

                // Get the new token for next parse.
                token = context.Stream.Get();
            }

            // Skip onto default block end '}' or short block end ';'.
            context.Stream.Skip();

            // Return the resulting block.
            return block;
        }
    }
}
