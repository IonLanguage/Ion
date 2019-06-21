using System;
using System.Collections.Generic;
using Ion.Generation.Helpers;
using Ion.Misc;
using Ion.Tracking.Symbols;
using LLVMSharp;

namespace Ion.Generation
{
    public class StructDef : Named, IContextPipe<Module, LLVMTypeRef>
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
            LLVMTypeRef @struct = LLVM.StructCreateNamed(context.Target.GetContext(), this.Identifier);

            // Create the body buffer list.
            List<LLVMTypeRef> body = new List<LLVMTypeRef>();

            // Create a buffer dictionary for the symbol.
            Dictionary<string, LLVMTypeRef> symbolProperties = new Dictionary<string, LLVMTypeRef>();

            // Map the body's properties onto the body.
            foreach (StructDefProperty property in this.Body.Properties)
            {
                // Emit the property's type.
                LLVMTypeRef type = property.Type.Emit();

                // Append it to the body.
                body.Add(type);

                // Append it to the symbol's properties dictionary.
                symbolProperties.Add(property.Identifier, type);
            }

            // Set the struct's body.
            LLVM.StructSetBody(@struct, body.ToArray(), true);

            // Create the struct symbol.
            StructSymbol symbol = new StructSymbol(this.Identifier, @struct, symbolProperties);

            // TODO: Ensure it does not already exist on the symbol table? Or automatically does it?
            // Register struct as a symbol in the symbol table.
            context.SymbolTable.structs.Add(symbol);

            // Return the resulting struct.
            return @struct;
        }
    }
}
