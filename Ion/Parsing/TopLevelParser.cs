using System;
using Ion.CodeGeneration;
using Ion.CodeGeneration.Structure;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{

    public class TopLevelParser : IParser<bool>
    {

        public readonly Module Module;
        public readonly PipeContext<Module> ModulePipeContext;

        public TopLevelParser(Module module, PipeContext<Module> pipeContext)
        {
            this.Module = module;
            this.ModulePipeContext = pipeContext;
        }

        // TODO: Is returning bool fine?
        public bool Parse(ParserContext context)
        {
            // Retrieve the current token.
            TokenType type = context.Stream.Get().Type;

            // Skip unknown tokens for error recovery.
            if (type == TokenType.Unknown)
            {
                // TODO: Use error reporting.
                Console.WriteLine("Warning: Skipping unknown token");

                return false;
            }

            // Function definition or global variable.
            if (TokenIdentifier.IsType(type))
            {
                // Peek the token after identifier.
                Token afterIdentifier = context.Stream.Peek(2);

                // Function definition.
                if (afterIdentifier.Type == TokenType.SymbolParenthesesL)
                {
                    // Invoke the function parser.
                    Function function = new FunctionParser().Parse(context);

                    // Emit the function.
                    function.Emit(this.ModulePipeContext);
                }
                // Otherwise, global variable declaration.
                else
                {
                    // Invoke the global variable parser.
                    GlobalVar globalVariable = new GlobalVarParser().Parse(context);

                    // Emit the global variable.
                    globalVariable.Emit(this.ModulePipeContext);
                }
            }
            // External definition.
            else if (type == TokenType.KeywordExternal)
            {
                // Invoke the external definition parser.
                Extern external = new ExternParser().Parse(context);

                // Emit the external definition.
                external.Emit(this.ModulePipeContext);
            }
            // TODO: Enforce a single namespace definition per-file.
            // Namespace definition.
            else if (type == TokenType.KeywordNamespace)
            {
                // Invoke the namespace definition parser.
                Namespace namespaceEntity = new NamespaceParser().Parse(context);

                // Process the namespace definition reaction.
                namespaceEntity.Invoke(this.Module);
            }
            // Otherwise, throw an error.
            else
            {
                throw new Exception("Unexpected top-level entity");
            }

            // TODO: Should return something else?
            return true;
        }

    }

}