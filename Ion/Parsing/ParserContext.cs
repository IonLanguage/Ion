using System.Collections.Generic;
using Ion.CognitiveServices;
using Ion.Core;
using Ion.ErrorReporting;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    // TODO: Finish implementing.
    public class ParserContext
    {
        public Stack<Error> ErrorStack { get; }

        public TokenStream Stream { get; }

        public Driver Driver { get; }

        public SymbolTable SymbolTable { get; }

        public ParserContext(Driver driver, TokenStream stream)
        {
            this.Driver = driver;
            this.Stream = stream;

            // Create a new error stack.
            this.ErrorStack = new Stack<Error>();

            // Create a new symbol table instance.
            this.SymbolTable = new SymbolTable();
        }

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
