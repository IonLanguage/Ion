using Ion.SyntaxAnalysis;
using System.Collections.Generic;

namespace Ion.Parsing
{
    public class PathParser : IParser<List<string>>
    {
        public List<string> Parse(TokenStream stream)
        {
            // Create the resulting path.
            List<string> path = new List<string>();

            // Capture identifier to serve as path node.
            string node = stream.Get(TokenType.Identifier);

            // Append node to path.
            path.Add(node);

            // Skip identifier token.
            stream.Skip();

            // Use recursion if symbol dot is present.
            if (stream.Get().Type == TokenType.SymbolDot)
            {
                // Create the parser instance.
                PathParser childParser = new PathParser().Parse(stream);

                // Invoke the parser.
                List<string> childParserPath = childParser.Parse(stream);

                // Append resulting child parser's path(s) to the resulting path.
                path.AddRange(childParserPath);
            }

            // Return the resulting path.
            return path;
        }
    }
}
