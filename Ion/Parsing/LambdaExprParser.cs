using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class LambdaExprParser : IParser<Expr>
    {
        public Expr Parse(ParserContext context)
        {
            // Create a lambda expression.
            LambdaExpr lambda = new LambdaExpr();

            // Parse the formal arguments.
            FormalArgs args = new FormalArgsParser().Parse(context);

            // Assign the parsed arguments to the lambda.
            lambda.Args = args;

            // Ensure current type is symbol colon.
            context.Stream.EnsureCurrent(TokenType.SymbolColon);

            // Skip symbol colon token.
            context.Stream.Skip();

            // Parse the return type.
            Type type = new TypeParser().Parse(context);

            // Assign the parsed type to the return type.
            lambda.ReturnType = type;

            // Parse the body block.
            Block body = new BlockParser().Parse(context);

            // Assign the block to the lambda.
            lambda.Body = body;

            // Return the resulting expression.
            return lambda;
        }
    }
}
