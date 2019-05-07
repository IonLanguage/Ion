using Ion.CognitiveServices;
using Ion.Core;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class StringExpr : Expr
    {
        public override ExprType Type => ExprType.StringLiteral;

        public readonly TokenType tokenType;

        public readonly Type type;

        public readonly string value;

        public StringExpr(TokenType tokenType, Type type, string value)
        {
            this.tokenType = tokenType;
            this.type = type;
            this.value = value;
        }

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // Emit the value.
            LLVMValueRef valueRef = Resolvers.Literal(this.tokenType, this.value, this.type);

            // Retrieve a string name.
            string name = NameCounter.GetString();

            // Register the value on the symbol table.
            SymbolTable.strings.Add(name, valueRef);

            // Return the emitted value.
            return valueRef;
        }
    }
}
