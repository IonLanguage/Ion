using System;
using System.Collections.Generic;
using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using Ion.Tracking.Symbols;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class StructDef : Named, IPipe<Module, LLVMTypeRef>
    {
        public StructDefBody Body { get; }

        public StructDef(string name, StructDefBody body)
        {
            this.SetName(name);
            this.Body = body;
        }

        public LLVMTypeRef Emit(PipeContext<Module> context)
        {
            // Create the struct construct.
            LLVMTypeRef @struct = LLVM.StructCreateNamed(context.Target.GetContext(), this.Name);

            // Create the struct symbol.
            StructSymbol symbol = new StructSymbol(this.Name, @struct);

            // TODO: Ensure it does not already exist on the symbol table? Or automatically does it?
            // Register struct as a symbol in the symbol table.
            context.SymbolTable.structs.Add(symbol);

            // Create the body buffer list.
            List<LLVMTypeRef> body = new List<LLVMTypeRef>();

            // Map the body's properties onto the body.
            foreach (StructDefProperty property in this.Body.Properties)
            {
                // Emit the property's type.
                body.Add(property.Type.Emit());
            }

            // TODO: Finish implementing (heavy hard-coded debugging code below, a function must be registered beforehand this point or will hang).
            // Set the struct's body.
            LLVM.StructSetBody(@struct, body.ToArray(), true);

            // Return the resulting struct.
            return @struct;
        }
    }
}
