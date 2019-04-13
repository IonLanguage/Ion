using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.CodeGen;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class FormalArgParser : IParser<FormalArg[]>
    {
        public FormalArg[] Parse(TokenStream stream)
        {
            List<FormalArg> args = new List<FormalArg>();

            return args.ToArray();
        }
    }
}
