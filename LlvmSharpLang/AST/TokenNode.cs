using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.AST
{
    public class TokenNode : Node<Token>
    {
        public TokenType Operator { get; set; }

        public TokenNode(Token value) : base(value.StartPos, value)
        {
            //
        }
    }
}
