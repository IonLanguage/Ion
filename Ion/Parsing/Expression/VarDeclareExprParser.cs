using Ion.Generation;
using Ion.Syntax;
using Type = Ion.Generation.Type;

namespace Ion.Parsing
{
    public class VarDeclareExprParser : IParser<VarDeclare>
    {
        public VarDeclare Parse(ParserContext context)
        {
            // Parse the type.
            Type type = new TypeParser().Parse(context);

            // Create the variable declaration & link the type.
            VarDeclare declaration = new VarDeclare(type, null);

            // Invoke identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // Assign the name.
            declaration.SetName(identifier);

            // Capture current token.
            Token token = context.Stream.Current;

            // A value is being assigned.
            if (token.Type == TokenType.OperatorAssignment)
            {
                // Skip the assignment operator.
                context.Stream.Skip();

                // Parse value.
                Construct value = new ExprParser().Parse(context);

                // Assign value.
                declaration.Value = value;
            }

            // Return the resulting declaration construct.
            return declaration;
        }
    }
}
