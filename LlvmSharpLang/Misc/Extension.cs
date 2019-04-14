using System;
using System.Globalization;
using System.Linq;
using LLVMSharp;

namespace LlvmSharpLang.Misc
{
    public static class Extension
    {
        public static string Capitalize(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));

                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));

                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public static LLVMBuilderRef CreateBuilder(this LLVMBasicBlockRef block)
        {
            LLVMBuilderRef builder = LLVM.CreateBuilder();

            LLVM.PositionBuilderAtEnd(builder, block);

            return builder;
        }
    }
}
