using System;
using System.Collections.Generic;
using Ion.CodeGeneration;
using Ion.Tracking.Symbols;
using LLVMSharp;

namespace Ion.Tracking
{
    /// <summary>
    /// Keeps track of emitted entities.
    /// </summary>
    public class ContextSymbolTable
    {
        public readonly SymbolTable<FunctionSymbol> functions = new SymbolTable<FunctionSymbol>();

        public readonly Dictionary<string, CodeGeneration.Module> modules = new Dictionary<string, CodeGeneration.Module>();

        public readonly Dictionary<string, string> directives = new Dictionary<string, string>();

        public readonly SymbolTable<StructSymbol> structs = new SymbolTable<StructSymbol>();

        /// <summary>
        /// Contains locally-scoped emitted values.
        /// All values are reset once the scope changes.
        /// </summary>
        public readonly Dictionary<string, LLVMValueRef> localScope = new Dictionary<string, LLVMValueRef>();

        /// <summary>
        /// The active block being parsed.
        /// </summary>
        public Block activeBlock;

        /// <summary>
        /// Contains emitted global string pointers.
        /// </summary>
        public Dictionary<string, LLVMValueRef> strings = new Dictionary<string, LLVMValueRef>();

        /// <summary>
        /// Retrieve a stored function symbol, otherwise throw an error
        /// if the requested function does not exist.
        /// </summary>
        public FunctionSymbol RetrieveFunctionOrThrow(string name)
        {
            // Ensure function with provided name exists.
            if (!this.functions.Contains(name))
            {
                throw new Exception($"Attempted to retrieve a non-existent function with name '{name}'");
            }

            // Return the function's LLVM value reference.
            return this.functions[name];
        }

        /// <summary>
        /// Reset all temporary stored values.
        /// </summary>
        public void Reset()
        {
            this.localScope.Clear();
            this.activeBlock = null;
        }

        public void HardReset()
        {
            this.Reset();
            this.functions.symbols.Clear();
            this.structs.symbols.Clear();
            this.modules.Clear();
            this.strings.Clear();
        }
    }
}
