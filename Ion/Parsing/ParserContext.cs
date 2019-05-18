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
        public TokenStream Stream { get; }

        public Driver Driver { get; }

        public SymbolTable SymbolTable => this.Driver.Module.SymbolTable;

        public ErrorRepository ErrorRepository;

        public ParserContext(Driver driver, TokenStream stream)
        {
            this.Driver = driver;
            this.Stream = stream;

            // Create a new error repository.
            this.ErrorRepository = new ErrorRepository(this.Stream, driver.Module.FileName);
        }

        /// <summary>
        /// Retrieve the precedence of the current
        /// token.
        /// </summary>
        public int GetCurrentPrecedence()
        {
            return Precedence.Get(this.Stream.Get().Type);
        }
    }
}
