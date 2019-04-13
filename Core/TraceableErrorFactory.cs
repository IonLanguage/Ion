using LlvmSharpLang.Core;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang
{
    public class TraceableErrorFactory
    {
        protected readonly TokenStream stream;

        public TraceableErrorFactory(TokenStream stream)
        {
            this.stream = stream;
        }

        public TraceableError Create(string message, string name = Error.defaultName)
        {
            return new TraceableError(this.stream, message, name);
        }
    }
}
