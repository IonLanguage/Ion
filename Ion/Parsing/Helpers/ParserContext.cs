using System.Collections.Generic;
using Ion.CodeGeneration.Helpers;
using Ion.CognitiveServices;
using Ion.Core;
using Ion.NoticeReporting;
using Ion.SyntaxAnalysis;
using Ion.Tracking;

namespace Ion.Parsing
{
    // TODO: Finish implementing.
    public class ParserContext : IGenericPipeContext
    {
        public TokenStream Stream { get; }

        public Driver Driver { get; }

        public ContextSymbolTable SymbolTable => this.Driver.Module.SymbolTable;

        public NoticeRepository NoticeRepository;

        public ParserContext(Driver driver, TokenStream stream)
        {
            this.Driver = driver;
            this.Stream = stream;

            // Create a new notice repository instance.
            this.NoticeRepository = new NoticeRepository(this.Stream, driver.Module.FileName);
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
