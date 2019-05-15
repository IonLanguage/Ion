using System;
using System.Collections.Generic;
using Ion.CodeGeneration;
using LLVMSharp;

namespace Ion.Core
{
    /// <summary>
    /// Keeps track of emitted entities.
    /// </summary>
    public class SymbolTable
    {
        public readonly Dictionary<string, LLVMValueRef> functions = new Dictionary<string, LLVMValueRef>();

        public readonly Dictionary<string, CodeGeneration.Module> modules = new Dictionary<string, CodeGeneration.Module>();

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
        /// Retrieve a stored function, otherwise throw an error
        /// if the requested function does not exist.
        /// </summary>
        public LLVMValueRef RetrieveFunctionOrThrow(string name)
        {
            // Ensure function with provided name exists.
            if (!this.functions.ContainsKey(name))
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
            this.functions.Clear();
            this.modules.Clear();
            this.strings.Clear();
        }
    }
}
