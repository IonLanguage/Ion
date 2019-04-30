using System;
using System.Collections.Generic;
using LLVMSharp;
using Ion.CodeGeneration.Structure;
using Ion.Core;
using Ion.Misc;

namespace Ion.CodeGeneration
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

            // Emit the function type.
            LLVMTypeRef type = LLVM.FunctionType(this.ReturnType.Emit(), args, this.Prototype.Args.Continuous);

            // Create the function.
            LLVMValueRef function = LLVM.AddFunction(context, this.Name, type);

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
            // Create the prototype if applicable.
            if (this.Prototype == null)
            {
                // Create the prototype.
                this.CreatePrototype();

                // Return formal arguments created by the previous invocation.
                return this.Prototype.Args;
            }

            // Create the args entity.
            this.Prototype.Args = new FormalArgs();

            // Return the newly created args.
            return this.Prototype.Args;
        }

        /// <summary>
        /// Creates a prototype for this function, overriding
        /// any existing prototype property value. Creates arguments.
        /// </summary>
        public Prototype CreatePrototype()
        {
            this.Prototype = new Prototype(this.Name, null);

            // Create formal arguments.
            FormalArgs args = this.CreateArgs();

            // Assign the formal arguments.
            this.Prototype.Args = args;

            // Return the prototype.
            return this.Prototype;
        }
    }
}
