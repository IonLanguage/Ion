using System;
using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;
using LlvmSharpLang.Core;
using LlvmSharpLang.Misc;

namespace LlvmSharpLang.CodeGeneration
{
    public class Function : Named, IEntity<LLVMValueRef, LLVMModuleRef>
    {
        public Prototype Prototype { get; set; }

        public Block Body { get; set; }

        public Type ReturnType { get; set; }

        public Function()
        {
            this.ReturnType = TypeFactory.Void();
        }

        public LLVMValueRef Emit(LLVMModuleRef context)
        {
            // Ensure body was provided or created.
            if (this.Body == null)
            {
                throw new Exception("Body is not defined");
            }

            // Emit the argument types.
            LLVMTypeRef[] args = this.Prototype.Args.Emit();

            // Emit the return type.
            LLVMTypeRef returnType = LLVM.FunctionType(this.ReturnType.Emit(), args, this.Prototype.Args.Continuous);

            // Create the function.
            LLVMValueRef function = LLVM.AddFunction(context, this.Name, returnType);

            // Apply the body.
            this.Body.Emit(function);

            // TODO: Ensure function does not already exist.
            // Register the function in the symbol table.
            SymbolTable.functions.Add(this.Name, function);

            return function;
        }

        /// <summary>
        /// Creates, assigns and returns a body block,
        /// replacing existing body.
        /// </summary>
        public Block CreateBody()
        {
            // Create a block as the body.
            Block body = new Block();

            // Assign the created block as the body.
            this.Body = body;

            // Return the newly created body.
            return body;
        }

        public FormalArgs CreateArgs()
        {
            // Create the args entity.
            FormalArgs args = new FormalArgs();

            // Assign the created args.
            this.Prototype.Args = args;

            // Return the newly created args.
            return args;
        }
    }
}
