using System;
using Ion.CodeGeneration;
using Ion.CodeGeneration.Helpers;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class TopLevelHandler : IReaction<ParserContext>
    {
        public readonly PipeContext<Module> ModulePipeContext;

        public TopLevelHandler(PipeContext<Module> pipeContext)
        {
            this.ModulePipeContext = pipeContext;
        }

        public void Invoke(ParserContext context)
        {
            // Retrieve the current token.
            Token token = context.Stream.Current;

            // Abstract the token's type.
            TokenType type = token.Type;

            // Skip unknown tokens for error recovery.
            if (type == SyntaxAnalysis.TokenType.Unknown)
            {
                // Create the warning.
                context.NoticeRepository.UnknownToken(token.Value);

                // Skip token.
                context.Stream.Skip();

                // Return immediately.
                return;
            }
            // Function definition or global variable.
            else if (TokenIdentifier.IsPrimitiveType(type) || type == TokenType.SymbolBracketL)
            {
                // Peek the token after identifier.
                Token afterIdentifier = context.Stream.Peek(2);

                // Function definition.
                if (type == TokenType.SymbolBracketL || afterIdentifier.Type == SyntaxAnalysis.TokenType.SymbolParenthesesL)
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
            // Struct definition.
            else if (type == SyntaxAnalysis.TokenType.KeywordStruct)
            {
                // Invoke the struct parser.
                StructDef @struct = new StructDefParser().Parse(context);

                // Emit the struct construct.
                @struct.Emit(this.ModulePipeContext);
            }
            // External definition.
            else if (type == SyntaxAnalysis.TokenType.KeywordExternal)
            {
                // Invoke the external definition parser.
                Extern @extern = new ExternParser().Parse(context);

                // Emit the external definition.
                @extern.Emit(this.ModulePipeContext);
            }
            // TODO: Enforce a single namespace definition per-file.
            // Namespace definition.
            else if (type == SyntaxAnalysis.TokenType.KeywordNamespace)
            {
                // Invoke the namespace definition parser.
                Namespace @namespace = new NamespaceParser().Parse(context);

                // Process the namespace definition reaction.
                @namespace.Invoke(this.ModulePipeContext.Target);
            }
            // Directive.
            else if (type == SyntaxAnalysis.TokenType.SymbolHash)
            {
                // Invoke the directive parser.
                Directive directive = new DirectiveParser().Parse(context);

                // Invoke the directive onto the symbol table.
                directive.Invoke(context);
            }
            // Otherwise, throw an error.
            else
            {
                throw new Exception("Unexpected top-level entity");
            }
        }
    }
}
