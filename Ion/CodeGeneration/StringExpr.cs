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
            // Retrieve a string name.
            string name = NameCounter.GetString();

            // Create the global string pointer.
            LLVMValueRef stringPtr = LLVM.BuildGlobalStringPtr(context, this.value, name);

            // Register the value on the symbol table.
            SymbolTable.strings.Add(name, stringPtr);

            // Return the string pointer value.
            return stringPtr;
        }
    }
}
