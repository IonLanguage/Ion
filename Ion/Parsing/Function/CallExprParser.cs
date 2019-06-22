using System.Collections.Generic;
using Ion.Generation;

namespace Ion.Parsing
{
    public class CallExprParser : IParser<Call>
    {
        protected readonly string identifier;

        public CallExprParser(string identifier)
        {
            this.identifier = identifier;
        }

        public CallExprParser(PathResult path) : this(path.ToString())
        {
            //
        }

        public Call Parse(ParserContext context)
        {
            // Invoke the function call argument parser.
            List<Expr> args = new CallArgsParser().Parse(context);

            // Create the function call expression entity.
            Call functionCall = new Call(this.identifier, args);

            // Return the function call expression.
            return functionCall;
        }
    }
}
