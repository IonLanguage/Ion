using LLVMSharp;
using LlvmSharpLang.CodeGen;
using LlvmSharpLang.CodeGen.Structure;

namespace LlvmSharpLang
{
    // TODO: This should be somehow boxed around "statement" so it emits within a body.
    public class VarDeclare : Named, IEntity<LLVMValueRef, LLVMBuilderRef>
    {
        public Type Type { get; protected set; }

        public VarDeclare(Type type)
        {
            this.Type = type;
        }

        public LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // Create the variable.
            LLVMValueRef variable = LLVM.BuildAlloca(context, this.Type.Emit(), this.Name);

            // TODO: What else?

            return variable;
        }
    }
}
