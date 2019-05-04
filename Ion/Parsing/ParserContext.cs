using System.Collections.Generic;
using Ion.CognitiveServices;
using Ion.ErrorReporting;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    // TODO: Finish implementing.
    public class ParserContext
    {
        public ParserContext(TokenStream stream)
        {
            this.ErrorStack = new Stack<Error>();
            this.Stream = stream;
        }

        public Stack<Error> ErrorStack { get; }

        public TokenStream Stream { get; }

        /// <summary>
        /// Retrieve the precedence of the current
        /// token.
        /// </summary>
        public int GetCurrentPrecedence()
        {
            return Precedence.Get(this.Stream.Get().Type);
        }

        /// <summary>
        /// Append an error to the error stack.
        /// </summary>
        public void AppendError(Error error)
        {
            this.ErrorStack.Push(error);
        }

        /// <summary>
        /// Clear and reset the error stack.
        /// </summary>
        public void BeginErrorStack()
        {
            this.ErrorStack.Clear();
        }
    }
}
