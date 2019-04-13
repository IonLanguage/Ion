using System;
using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang.Misc
{
    public class Resolver
    {
        public delegate LLVMTypeRef LlvmTypeDelegate();

        protected static readonly Dictionary<string, LlvmTypeDelegate> typeMap = new Dictionary<string, LlvmTypeDelegate> {
            { "int", LLVMTypeRef.Int32Type }
        };

        public static LLVMTypeRef Type(string type)
        {
            if (Resolver.typeMap.ContainsKey(type))
            {
                return Resolver.typeMap[type]();
            }

            throw new Exception("Non-registered type");
        }
    }
}
