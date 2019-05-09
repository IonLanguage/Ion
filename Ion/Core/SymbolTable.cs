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
            this.strings.Clear();
        }
    }
}
