using System.Text;
using Ion.Syntax;

namespace Ion.Optimization.Minification
{
    public interface IMinifier
    {
        string Minify(TokenStream stream);
    }

    public class Minifier
    {
        protected readonly MinifierOption options;

        public Minifier(MinifierOption options)
        {
            this.options = options;
        }

        public Minifier() : this(MinifierOption.None)
        {
            //
        }

        public string Minify(TokenStream stream)
        {
            // Create a new string builder instance.
            StringBuilder result = new StringBuilder();

            // Create a buffer for the previous token.
            Token previousBuffer = stream[0];

            // Loop through all the tokens.            
            foreach (Token token in stream)
            {
                // Abstract the token's value.
                string value = token.Value;

                // Prepend a space if both the token's and previous token's values start with a letter.
                if (char.IsLetter(value[0]) && char.IsLetter(previousBuffer.Value[0]))
                {
                    value = value.Insert(0, " ");
                }

                // Append the resulting value onto the result.
                result.Append(value);

                // Populate the previous token buffer.
                previousBuffer = token;
            }

            // Trim and return the resulting token stream.
            return result.ToString().Trim();
        }

        public string Minify(string input)
        {
            // Create a new lexer instance.
            Lexer lexer = new Lexer(input);

            // Invoke the lexer, create resulting token stream.
            TokenStream stream = new TokenStream(lexer.Tokenize());

            // Invoke the base method with the stream.
            return this.Minify(stream);
        }
    }
}
