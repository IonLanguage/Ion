using System;
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

        public ParserException CreateException(string message, string name = InternalErrorNames.Generic)
        {
            // Create the error.
            Error error = new Error(message, this.sourceFileName, name);

            // Append the error onto the stack.
            this.stack.Append(error);

            // Create a parser exception for the error.
            ParserException exception = new ParserException(error.ToString());

            // Return the parser exception.
            return exception;
        }

        public ReadOnlyNoticeStack GetStack()
        {
            return this.stack.AsReadOnly();
        }

        public void UnexpectedToken(TokenType expected, TokenType actual)
        {
            this.CreateException($"Unexpected token '{actual}'; Expected '{expected}'", InternalErrorNames.Syntax);
        }

        public void ArgumentMismatch(string functionName, int expected, int actual)
        {
            this.CreateException($"Argument mismatch for function '{functionName}'; Expected '{expected}' arguments but got '{actual}'");
        }
    }
}
