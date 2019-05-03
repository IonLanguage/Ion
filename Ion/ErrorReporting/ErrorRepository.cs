using Ion.SyntaxAnalysis;

namespace Ion.ErrorReporting
{
    public class ErrorRepository
    {
        protected readonly TraceableErrorFactory factory;
        protected readonly TokenStream stream;

        public ErrorRepository(TokenStream stream)
        {
            this.stream = stream;
            this.factory = new TraceableErrorFactory(this.stream);
        }

        public TraceableError UnexpectedToken(TokenType expected, TokenType actual)
        {
            return this.factory.Create("", InternalErrorNames.Syntax);
        }
    }
}