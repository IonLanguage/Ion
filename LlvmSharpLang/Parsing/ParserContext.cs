using LlvmSharpLang.CognitiveServices;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    // TODO: Finish implementing.
    public class ParserContext
    {
        public TokenStream Stream { get; }

        /// <summary>
        /// Retrieve the precedence of the
        /// current token.
        /// </summary>
        public int GetCurrentPrecedence()
        {
            return Precedence.Get(this.Stream.Get().Type);
        }
    }
}
