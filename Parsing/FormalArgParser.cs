using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang
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
