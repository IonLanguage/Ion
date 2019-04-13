using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.CodeGen;
using LlvmSharpLang.Misc;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class ArgParser : IParser<Arg[]>
    {
        public Arg[] Parse(TokenStream stream)
        {
            // Skip '('.
            stream.Skip();

            List<Arg> args = new List<Arg>();

            // TODO

            // --- START TESTING ---
            var testArg = new Arg();

            testArg.SetName("testArg");
            testArg.Type = TypeFactory.Int32;

            args.Add(testArg);

            var testArg2 = new Arg();

            testArg2.Type = TypeFactory.Int64;

            args.Add(testArg2);
            // --- END TESTING ---

            // Skip ')'.
            stream.Skip();

            return args.ToArray();
        }
    }
}
