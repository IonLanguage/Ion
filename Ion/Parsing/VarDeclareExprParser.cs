using System;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;
using Type = Ion.CodeGeneration.Type;

namespace Ion.Parsing
{
    public class VarDeclareExprParser : IParser<VarDeclareExpr>
    {
        public VarDeclareExpr Parse(ParserContext context)
        {
            // Parse the type.
            Type type = new TypeParser().Parse(context);

            // Create the variable declaration & link the type.
            VarDeclareExpr declaration = new VarDeclareExpr(type, null);

            // Consume the variable name.
            string name = context.Stream.Get().Value;

            // Ensure captured name is not null nor empty.
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Unexpected variable declaration identifier to be null or empty");
            }

            // Assign the name.
            declaration.SetName(name);

            // Peek next token for value.
            Token nextToken = context.Stream.Peek();

            // Value is being assigned.
            if (nextToken.Type == TokenType.OperatorAssignment)
            {
                // Skip onto the assignment operator
                context.Stream.Skip();

                // Skip the assignment operator.
                context.Stream.Skip();

                // Parse value.
                Expr value = new ExprParser().Parse(context);

                // Assign value.
                declaration.Value = value;
            }

            return declaration;
        }
    }
}
