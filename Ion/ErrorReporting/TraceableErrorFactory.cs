using Ion.Core;
using Ion.SyntaxAnalysis;

namespace Ion.ErrorReporting
{
    public class TraceableErrorFactory
    {
        protected readonly TokenStream stream;

        public TraceableErrorFactory(TokenStream stream)
        {
            this.stream = stream;
        }

        public TraceableError Create(string message, string name = InternalErrorNames.Generic)
        {
            return new TraceableError(this.stream, message, name);
        }
    }
}
