using System;
using Ion.CodeGeneration.Helpers;
using Ion.Core;
using Ion.Syntax;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class LambdaExpr : Expr
    {
        public override ExprType ExprType => ExprType.Lambda;

        public FormalArgs Args { get; set; }

        public ITypeEmitter ReturnType { get; set; }

        public Block Body { get; set; }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Create a new function.
            Function function = new Function();

            // Assign the function's body.
            function.Body = this.Body;

            // Create the function's prototype.
            function.Prototype = new Prototype(NameRegister.GetLambda(), this.Args, this.ReturnType);

            // Emit the created function.
            LLVMValueRef functionRef = function.Emit(context.ModuleContext);

            // TODO: What about input arguments?
            // Create a function call.
            CallExpr call = new CallExpr(function.Prototype.Identifier);

            // Emit the function call.
            LLVMValueRef result = call.Emit(context);

            // Return the resulting function call.
            return result;
        }
    }
}
