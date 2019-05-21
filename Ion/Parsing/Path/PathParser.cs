using Ion.SyntaxAnalysis;
using System.Collections.Generic;

namespace Ion.Parsing
{
    public class PathParser : IParser<PathResult>
    {
        public PathResult Parse(ParserContext context)
        {
            // Create the resulting path's nodes.
            List<string> nodes = new List<string>();

            // Capture identifier to serve as path node.
            string node = context.Stream.Get(TokenType.Identifier).Value;

            // Append node to path.
            nodes.Add(node);

            // Skip identifier token.
            context.Stream.Skip();

            // Use recursion if symbol dot is present.
            if (context.Stream.Get().Type == TokenType.SymbolDot)
            {
                // Create the parser instance.
                PathParser childParser = new PathParser();

                // Invoke the parser.
                PathResult childParserPath = childParser.Parse(context);

                // Append resulting child parser's path(s) to the resulting path.
                nodes.AddRange(childParserPath.nodes);
            }

            // Create the resulting path.
            PathResult result = new PathResult(nodes);

            // Return the resulting path.
            return result;
        }
    }
}
