using System;
using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Struct : Named, IPipe<Module, LLVMTypeRef>
    {
        public StructPrototype Prototype { get; }

        public Struct(string name, StructPrototype prototype)
        {
            this.SetName(name);
            this.Prototype = prototype;
        }

        public LLVMTypeRef Emit(PipeContext<Module> context)
        {
            // Create the struct construct.
            LLVMTypeRef @struct = LLVM.StructCreateNamed(context.Target.GetContext(), this.Name);

            // TODO: Ensure it does not already exist on the symbol table.
            // Register struct in the symbol table.
            context.SymbolTable.structs.Add(this.Name, @struct);

            // TODO: Finish implementing (heavy hard-coded debugging code below, a function must be registered beforehand this point or will hang).
            LLVM.StructSetBody(@struct, new LLVMTypeRef[] { TypeFactory.Boolean().Emit() }, true);

            var b = LLVM.CreateBuilder();

            var f = LLVM.GetLastFunction(context.Target.Target);

            var bb = LLVM.GetEntryBasicBlock(f);

            LLVM.PositionBuilderAtEnd(b, bb);

            LLVM.BuildAlloca(b, @struct, "teststruct_a");

            return @struct;
        }
    }
}
