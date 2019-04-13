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
            // Skip '('.
            stream.Skip();

            List<FormalArg> args = new List<FormalArg>();

            // TODO

            // Skip ')'.
            stream.Skip();

            return args.ToArray();
        }
    }
}
