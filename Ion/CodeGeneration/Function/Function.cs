using System;
using Ion.CodeGeneration.Helpers;
using Ion.Core;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Function : Named, ITopLevelPipe
    {
        public Attribute[] Attributes { get; set; }

        public Prototype Prototype { get; set; }

        public Block Body { get; set; }

        public Function()
        {
            this.Attributes = new Attribute[] { };
        }

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
            // Ensure that body returns a value if applicable.
            else if (!this.Prototype.ReturnsVoid && !this.Body.HasReturnExpr)
            {
                throw new Exception("Functions that do not return void must return a value");
            }

            // Emit the argument types.
            LLVMTypeRef[] args = this.Prototype.Args.Emit(context);

            // Emit the return type
            LLVMTypeRef returnType = this.Prototype.ReturnType.Emit();

            // Emit the function type.
            LLVMTypeRef type = LLVM.FunctionType(returnType, args, this.Prototype.Args.Continuous);

            // Create the function.
            LLVMValueRef function = LLVM.AddFunction(context.Target.Target, this.Prototype.Identifier, type);

            // Create the function context.
            PipeContext<LLVMValueRef> functionContext = context.Derive<LLVMValueRef>(function);

            // Emit the body to its corresponding context.
            LLVMBasicBlockRef body = this.Body.Emit(functionContext);

            // Create a new builder reference for the body.
            LLVMBuilderRef bodyBuilder = body.CreateBuilder(false);

            // Derive a context for the body's builder.
            PipeContext<LLVMBuilderRef> bodyContext = context.Derive<LLVMBuilderRef>(bodyBuilder);

            // TODO: Missing support for native attribute emission.
            // Emit attributes as first-class instructions if applicable.
            foreach (Attribute attribute in this.Attributes)
            {
                // Emit the attribute onto the body's builder context.
                attribute.Emit(bodyContext);
            }

            // Ensures the function does not already exist
            if (context.SymbolTable.functions.ContainsKey(this.Prototype.Identifier))
            {
                throw new Exception($"A function with the identifier '{this.Prototype.Identifier}' already exists");
            }

            // Register the function on the symbol table.
            context.SymbolTable.functions.Add(this.Prototype.Identifier, function);

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

            // Set the body's name to entry.
            body.SetNameEntry();

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
            ITypeEmitter returnType = PrimitiveTypeFactory.Void();

            // Create a new prototype instance.
            this.Prototype = new Prototype(this.Identifier, null, returnType, true);

            // Create formal arguments after assigning prototype to avoid infinite loop.
            FormalArgs args = this.CreateArgs();

            // Assign the formal arguments.
            this.Prototype.Args = args;

            // Return the prototype.
            return this.Prototype;
        }
    }
}
