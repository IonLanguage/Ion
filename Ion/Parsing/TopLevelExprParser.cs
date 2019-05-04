using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    /// <summary>
    /// Parses top-level expressions using an anonymous
    /// function.
    /// </summary>
    public class TopLevelExprParser : IParser<Function>
    {
        public Function Parse(TokenStream stream)
        {
            // TODO: Use expression? Maybe add it to the body block?
            // Parse the expression.
            Expr expr = new ExprParser().Parse(stream);

            // Create the anonymous function.
            var function = new Function();

            // Set the function name to anonymous.
            function.SetNameAnonymous();

            // Create default prototype.
            function.CreatePrototype();

            // TODO: Finish implementing (continue Kaleidoscope tutorial).
            Block body = function.CreateBody();

            return function;
        }
    }
}
