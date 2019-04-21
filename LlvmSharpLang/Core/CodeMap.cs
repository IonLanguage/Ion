using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang.Core
{
    /// <summary>
    /// Keeps track of emitted entities.
    /// </summary>
    public static class CodeMap
    {
        public static Dictionary<string, LLVMValueRef> functions = new Dictionary<string, LLVMValueRef>();
    }
}
