using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.CodeGen;
using LlvmSharpLang.Misc;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class FormalArgParser : IParser<Arg[]>
    {
        public Arg[] Parse(TokenStream stream)
        {
            // Skip '('.
            stream.Skip();

            List<Arg> args = new List<Arg>();

            // TODO
            var testArg = new Arg();

            testArg.SetName("testArg");
            testArg.Type = TypeFactory.Int32;

            args.Add(testArg);

            // Skip ')'.
            stream.Skip();

            return args.ToArray();
        }
    }
}
