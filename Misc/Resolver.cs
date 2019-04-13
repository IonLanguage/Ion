using System;
using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang.Misc
{
    public class Resolver
    {
        public delegate LLVMTypeRef TypeResolverDelegate();

        protected static readonly Dictionary<string, TypeResolverDelegate> typeMap = new Dictionary<string, TypeResolverDelegate> {
            { "int", LLVMTypeRef.Int32Type }
        };

        public static TypeResolverDelegate Type(string type)
        {
            if (Resolver.typeMap.ContainsKey(type))
            {
                return Resolver.typeMap[type];
            }

            throw new Exception("Non-registered type");
        }
    }
}
