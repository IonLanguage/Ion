using System;
using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.CodeGen;
using LlvmSharpLang.Misc;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class ArgsParser : IParser<Args>
    {
        public Args Parse(TokenStream stream)
        {
            // Skip '('.
            stream.Skip(SyntaxAnalysis.TokenType.SymbolParenthesesL);

            Args args = new Args();
            Token buffer = stream.Next();

            // Loop until parentheses end.
            while (buffer.Type != SyntaxAnalysis.TokenType.SymbolParenthesesR)
            {
                // Get argument type value.
                string typeValue = buffer.Value;

                // Continuous arguments.
                if (!args.Continuous && buffer.Type == SyntaxAnalysis.TokenType.SymbolContinuous)
                {
                    // Set the continuous flag.
                    args.Continuous = true;

                    // Advance stream immediatly.
                    buffer = stream.Next();

                    continue;
                }
                // Continuous arguments must be final.
                else if (args.Continuous)
                {
                    throw new Exception("Unexpected token after continuous arguments");
                }

                // Create the arg's type.
                CodeGen.Type type = new CodeGen.Type(typeValue);

                // Create the arg.
                Arg arg = new Arg(type);

                // Capture the arg's name.
                string name = stream.Next(SyntaxAnalysis.TokenType.Id).Value;

                // Assign the arg's name.
                arg.SetName(name);

                // Append the newly created arg.
                args.Values.Add(arg);

                // Prepare buffer for next iteration.
                buffer = stream.Next();
            }

            // Finish process. No need to skip parentheses end.
            return args;
        }
    }
}
