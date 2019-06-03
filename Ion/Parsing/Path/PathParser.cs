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

            // Invoke identifier parser to capture the node.
            string node = new IdentifierParser().Parse(context);

            // Append node to path.
            nodes.Add(node);

            // Use recursion if symbol dot is present.
            if (context.Stream.Current.Type == TokenType.SymbolDot)
            {
                // Skip symbol dot token.
                context.Stream.Skip();

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
