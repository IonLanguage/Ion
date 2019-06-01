using Ion.CodeGeneration;

namespace Ion.Parsing
{
    public class LambdaExprParser : IParser<Expr>
    {
        public Expr Parse(ParserContext context)
        {
            // Create a lambda expression.
            LambdaExpr lambda = new LambdaExpr();

            // Parse the return type.
            Type type = new TypeParser().Parse(context);

            // Assign the parsed type to the return type.
            lambda.ReturnType = type;

            // Parse the formal arguments.
            FormalArgs args = new FormalArgsParser().Parse(context);

            // Assign the parsed arguments to the lambda.
            lambda.Args = args;

            // Parse the block.
            Block block = new BlockParser().Parse(context);

            // Assign the block to the lambda.
            lambda.Block = block;

            // Return the resulting expression.
            return lambda;
        }
    }
}
