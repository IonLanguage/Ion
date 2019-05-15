using System;
using Ion.CodeGeneration.Structure;
using Ion.Core;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Function : Named, IPipe<CodeGeneration.Module, LLVMValueRef>
    {
        public Prototype Prototype { get; set; }

        public Block Body { get; set; }

        public LLVMValueRef Emit(PipeContext<CodeGeneration.Module> context)
        {
            // Ensure body was provided or created.
            if (this.Body == null)
            {
                throw new Exception("Unexpected function body to be null");
            }
            // Ensure prototype is set.
            else if (this.Prototype == null)
            {
                throw new Exception("Unexpected function prototype to be null");
            }

            // Emit the argument types.
            LLVMTypeRef[] args = this.Prototype.Args.Emit();

            // Emit the return type
            LLVMTypeRef returnType = this.Prototype.ReturnType.Emit();

            // Emit the function type.
            LLVMTypeRef type = LLVM.FunctionType(returnType, args, this.Prototype.Args.Continuous);

            // Create the function.
            LLVMValueRef function = LLVM.AddFunction(context.Target.Target, this.Prototype.Name, type);

            // Create the body context.
            PipeContext<LLVMValueRef> bodyContext = context.Derive<LLVMValueRef>(function);

            // Emit the body to its corresponding context.
            this.Body.Emit(bodyContext);

            // Ensures the function does not already exist
            if (context.SymbolTable.functions.ContainsKey(this.Prototype.Name))
            {
                throw new Exception($"Function with that name \"{this.Prototype.Name}\" already exists.");
            }

            // Register the function on the symbol table.
            context.SymbolTable.functions.Add(this.Prototype.Name, function);

            // Return the function entity.
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
            // Default the return type to void.
            Type returnType = TypeFactory.Void();

            // Create a new prototype instance.
            this.Prototype = new Prototype(this.Name, null, returnType);

            // Create formal arguments after assigning prototype to avoid infinite loop.
            FormalArgs args = this.CreateArgs();

            // Assign the formal arguments.
            this.Prototype.Args = args;

            // Return the prototype.
            return this.Prototype;
        }
    }
}
