using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.Core;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class BlockParser : IParser<Block>
    {
        public Block Parse(TokenStream stream)
        {
            // Consume next token. Either '{' or '=>' for anonymous functions.
            Token begin = stream.Next();

            // Create the block.
            Block block = new Block();

            // Set the block as active in the symbol table.
            SymbolTable.activeBlock = block;

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

            // Look at the next token.
            Token nextToken = stream.Peek();

            // While next token is not a block-closing token.
            while (nextToken.Type != TokenType.SymbolBlockR && block.Type != BlockType.Short)
            {
                // Returning a value.
                if (nextToken.Type == TokenType.KeywordReturn)
                {
                    // Invoke the return parser. It's okay if it returns null, as it will be emitted as void.
                    Expr returnExpr = new FunctionReturnParser().Parse(stream);

                    // Assign the return expression to the block.
                    block.ReturnExpr = returnExpr;

                    // Exit the loop and return
                    break;
                }

                // Token must be an expression.
                Expr expr = new PrimaryExprParser().Parse(stream);

                block.Expressions.Add(expr);

                // Ensure expression was successfully parsed.
                if (expr == null)
                {
                    throw new Exception("Unexpected expression to be null");
                }

                // SKip over the semi colon.
                stream.Skip();

                // Peek the new token for next parse.
                nextToken = stream.Peek();
            }

            // Skip default block end '}' or short block end ';'.
            stream.Skip();

            return block;
        }
    }
}
