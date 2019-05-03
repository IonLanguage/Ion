using System;
using Ion.CodeGeneration.Structure;
using Ion.Core;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Function : Named, IEntity<LLVMValueRef, LLVMModuleRef>
    {
        public Prototype Prototype { get; set; }

        public Block Body { get; set; }

        public LLVMValueRef Emit(LLVMModuleRef context)
        {
            // Ensure body was provided or created.
            if (Body == null)
                throw new Exception("Unexpected function body to be null");
            // Ensure prototype is set.
            else if (Prototype == null) throw new Exception("Unexpected function prototype to be null");

            // Emit the argument types.
            var args = Prototype.Args.Emit();

            // Emit the return type
            LLVMTypeRef returnType = Prototype.ReturnType.Emit();

            // Emit the function type.
            LLVMTypeRef type = LLVM.FunctionType(returnType, args, Prototype.Args.Continuous);

            // Create the function.
            LLVMValueRef function = LLVM.AddFunction(context, Prototype.Name, type);

            // Apply the body.
            Body.Emit(function);

            // Ensures the function does not already exist
            if (SymbolTable.functions.ContainsKey(Prototype.Name))
                throw new Exception($@"Function with that name ""{Prototype.Name}"" already exists.");
            else
                // Register the function in the symbol table.
                SymbolTable.functions.Add(Prototype.Name, function);

            return function;
        }

        /// <summary>
        ///     Attempt to retrieve the function LLVM value
        ///     reference from the symbol table. Returns null
        ///     if the function does not exist or was not
        ///     previously emitted.
        /// </summary>
        public LLVMValueRef Retrieve()
        {
            return SymbolTable.functions[Name];
        }

        /// <summary>
        ///     Creates, assigns and returns a body block,
        ///     replacing existing body.
        /// </summary>
        public Block CreateBody()
        {
            // Create a block as the body.
            var body = new Block();

            // Assign the created block as the body.
            Body = body;

            // Return the newly created body.
            return body;
        }

        public FormalArgs CreateArgs()
        {
            // Create the prototype if applicable.
            if (Prototype == null)
            {
                // Create the prototype.
                CreatePrototype();

                // Return formal arguments created by the previous invocation.
                return Prototype.Args;
            }

            // Create the args entity.
            Prototype.Args = new FormalArgs();

            // Return the newly created args.
            return Prototype.Args;
        }

        /// <summary>
        ///     Creates a prototype for this function, overriding
        ///     any existing prototype property value. Creates arguments.
        /// </summary>
        public Prototype CreatePrototype()
        {
            // Default the return type to void.
            Type returnType = TypeFactory.Void();

            // Create a new prototype instance.
            Prototype = new Prototype(Name, null, returnType);

            // Create formal arguments after assigning prototype to avoid infinite loop.
            FormalArgs args = CreateArgs();

            // Assign the formal arguments.
            Prototype.Args = args;

            // Return the prototype.
            return Prototype;
        }
    }
}