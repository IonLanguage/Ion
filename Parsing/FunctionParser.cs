using System;
using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang
{
    public class FunctionParser : Parser<Function>
    {
        public override Function Parse(TokenStream stream)
        {
            IEnumerator<Token> enumerator = stream.GetEnumerator();

            // Consume 'fn' keyword.
            enumerator.MoveNext();

            // Consume function identifier.
            enumerator.MoveNext();

            string name = enumerator.Current.Value;
            Function fn = new Function();

            // Set the function name.
            fn.SetName(name);

            // TODO: Continue implementing.

            return fn;
        }
    }
}
