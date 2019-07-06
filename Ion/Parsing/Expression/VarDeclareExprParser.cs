#nullable enable

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

            // Invoke identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // Capture current token.
            Token token = context.Stream.Current;

            // Create the value buffer.
            Construct? value = null;

            // A value is being assigned.
            if (token.Type == TokenType.OperatorAssignment)
            {
                // Skip the assignment operator.
                context.Stream.Skip();

                // Parse and assign the value to the buffer.
                value = new ExprParser().Parse(context);
            }

            // Create the variable declaration & link the type.
            VarDeclare declaration = new VarDeclare(identifier, type, null);

            // Return the resulting declaration construct.
            return declaration;
        }
    }
}
