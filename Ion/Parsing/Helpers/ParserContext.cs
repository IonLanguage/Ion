using Ion.CognitiveServices;
using Ion.NoticeReporting;
using Ion.Syntax;

namespace Ion.Parsing
{
    // TODO: Finish implementing.
    public class ParserContext
    {
        public TokenStream Stream { get; }

        public Driver Driver { get; }

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
