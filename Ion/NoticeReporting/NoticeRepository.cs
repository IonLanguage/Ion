using Ion.SyntaxAnalysis;

namespace Ion.NoticeReporting
{
    public class NoticeRepository
    {
        protected readonly TokenStream stream;

        protected readonly NoticeStack stack;

        protected readonly string sourceFileName;

        public NoticeRepository(TokenStream stream, string sourceFileName)
        {
            this.stream = stream;
            this.stack = new NoticeStack(this.stream);
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
