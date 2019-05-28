using Ion.CodeGeneration.Helpers;
using Ion.CognitiveServices;
using Ion.Core;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class StringExpr : Expr
    {
        public override ExprType ExprType => ExprType.StringLiteral;

        public readonly TokenType tokenType;

        public readonly string value;

        public StringExpr(TokenType tokenType, string value)
        {
            this.tokenType = tokenType;
            this.value = value;
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Retrieve a string name.
            string name = NameCounter.GetString();

            // Create the global string pointer.
            LLVMValueRef stringPtr = LLVM.BuildGlobalStringPtr(context.Target, this.value, name);

            // Register the value on the symbol table.
            context.SymbolTable.strings.Add(name, stringPtr);

            // Return the string pointer value.
            return stringPtr;
        }
    }
}
