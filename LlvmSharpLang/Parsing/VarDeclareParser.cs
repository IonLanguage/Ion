using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class VarDeclareParser : IParser<VarDeclare>
    {
        public VarDeclare Parse(TokenStream stream)
        {
            // Consume the type string.
            string typeValue = stream.Next().Value;

            // Create the type.
            Type type = new Type(typeValue);

            // Create the variable declaration & link the type.
            VarDeclare declaration = new VarDeclare(type, null);

            // Consume the variable name.
            string name = stream.Next().Value;

            // Assign the name.
            declaration.SetName(name);

            // Peek next token for value.
            Token nextToken = stream.Peek();

            // Value is being assigned.
            if (nextToken.Type == TokenType.OperatorAssignment)
            {
                // Parse value.
                Expr value = new ExprParser().Parse(stream);

                // Assign value.
                declaration.Value = value;
            }

            return declaration;
        }
    }
}
