using LLVMSharp;
using LlvmSharpLang.CodeGen;
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
            // TODO: Otherwise, throw error about block type.

            // TODO

            // Skip block end '{' or statement end ';'.
            stream.Skip();

            return block;
        }
    }
}
