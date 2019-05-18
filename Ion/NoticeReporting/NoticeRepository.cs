using Ion.SyntaxAnalysis;

namespace Ion.NoticeReporting
{
    public class ErrorRepository
    {
        protected readonly TokenStream stream;

        protected readonly ErrorStack stack;

        protected readonly string sourceFileName;

        public ErrorRepository(TokenStream stream, string sourceFileName)
        {
            this.stream = stream;
            this.stack = new ErrorStack(this.stream);
            this.sourceFileName = sourceFileName;
        }

        public void Append(string message, string name = InternalErrorNames.Generic)
        {
            // Create the error.
            Error error = new Error(message, this.sourceFileName, name);

            // Append the error onto the stack.
            this.stack.Append(error);
        }

        public void UnexpectedToken(TokenType expected, TokenType actual)
        {
            this.Append($"Unexpected token '{actual}'; Expected '{expected}'", InternalErrorNames.Syntax);
        }

        public void ArgumentMismatch(string functionName, int expected, int actual)
        {
            this.Append($"Argument mismatch for function '{functionName}'; Expected '{expected}' arguments but got '{actual}'");
        }
    }
}
