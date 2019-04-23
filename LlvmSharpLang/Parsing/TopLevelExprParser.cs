using LlvmSharpLang.SynaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    /// <summary>
    /// Parses top-level expressions using an anonymous
    /// function.
    /// </summary>
    public class TopLevelExprParser : IParser<Function>
    {
        public Function Parse(TokenStream stream)
        {
            // Parse the expression.
            Expr expr = new ExprParser().Parse(stream);

            // Create the anonymous function.
            Function function = new Function();

            // Set the function name to anonymous.
            function.SetNameAnonymous();

            // TODO: Finish implementing.
            function.Body = new Body();

            return function;
        }
    }
}
