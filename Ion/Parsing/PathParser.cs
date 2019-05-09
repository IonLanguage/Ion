using Ion.SyntaxAnalysis;
using System.Collections.Generic;

namespace Ion.Parsing
{
    public class PathParser : IParser<List<string>>
    {
        public List<string> Parse(ParserContext context)
        {
            // Create the resulting path.
            List<string> path = new List<string>();

            // Capture identifier to serve as path node.
            string node = context.Stream.Get(TokenType.Identifier).Value;

            // Append node to path.
            path.Add(node);

            // Skip identifier token.
            context.Stream.Skip();

            // Use recursion if symbol dot is present.
            if (context.Stream.Get().Type == TokenType.SymbolDot)
            {
                // Create the parser instance.
                PathParser childParser = new PathParser();

                // Invoke the parser.
                List<string> childParserPath = childParser.Parse(context);

                // Append resulting child parser's path(s) to the resulting path.
                path.AddRange(childParserPath);
            }

            // Return the resulting path.
            return path;
        }
    }
}
