using Ion.Syntax;

namespace Ion.Parsing
{
    public class TokenStreamFactory
    {
        /// <summary>
        /// Create token stream from an input string.
        /// </summary>
        public static TokenStream CreateFromInput(string input)
        {
            // Create the lexer.
            Lexer lexer = new Lexer(input);

            // Tokenize the input.
            Token[] tokens = lexer.Tokenize();

            // Create the resulting stream.
            TokenStream stream = new TokenStream(tokens);

            // Return the stream.
            return stream;
        }
    }
}
