using System;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class VarDeclareExprParser : IParser<VarDeclareExpr>
    {
        public VarDeclareExpr Parse(TokenStream stream)
        {
            // Consume the type string.
            string typeValue = stream.Next().Value;

            // Create the type.
            CodeGeneration.Type type = new CodeGeneration.Type(typeValue);

            // Create the variable declaration & link the type.
            VarDeclareExpr declaration = new VarDeclareExpr(type, null);

            // Consume the variable name.
            string name = stream.Next().Value;

            // Assign the name.
            declaration.SetName(name);

            // Peek next token for value.
            Token nextToken = stream.Peek();

            // Value is being assigned.
            if (nextToken.Type == TokenType.OperatorAssignment)
            {
                // Skip over the Assignment operator
                stream.Skip();

                // Parse value.
                Expr value = new ExprParser().Parse(stream);

                // Assign value.
                declaration.Value = value;
            }
            // Otherwise, it must be a semi-colon.
            else if (nextToken.Type != TokenType.SymbolSemiColon)
            {
                throw new Exception($"Unexpected token after variable declaration; Expected semi-colon but got '{nextToken.Type}'");
            }

            return declaration;
        }
    }
}
