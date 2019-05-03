using System;
using Ion.Abstraction;
using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class Driver
    {
        protected readonly TokenStream stream;

        public Driver(TokenStream stream)
        {
            this.stream = stream;
            Module = new Module();
        }

        public Driver(Token[] tokens) : this(new TokenStream(tokens))
        {
            //
        }

        public Module Module { get; }

        // TODO: What if EOF token has not been processed itself?
        public bool HasNext
        {
            get
            {
                TokenType currentType = stream.Get().Type;
                TokenType nextType = stream.Peek().Type;

                return currentType != TokenType.ProgramEnd && nextType != TokenType.ProgramEnd;
            }
        }

        /// <summary>
        ///     Process the next sequence. Returns true
        ///     if the sequence was successfully processed.
        /// </summary>
        public bool Next()
        {
            // TODO: What if EOF token has not been processed itself?
            // End reached.
            if (stream.LastItem) return false;

            // TODO: Finish fixing this, parsers overflowing (+1) because of this issue with the Program start (05/02/2019).
            TokenType type = stream.Get().Type;

            // Skip program start token.
            if (type == TokenType.ProgramStart)
            {
                stream.Skip();

                // Assign type as next token type, continue execution.
                type = stream.Get().Type;
            }

            // Skip unknown tokens for error recovery.
            if (type == TokenType.Unknown)
            {
                // TODO: Use error reporting.
                Console.WriteLine("Warning: Skipping unknown token");

                return false;
            }
            // Function definition or global variable.
            else if (TokenIdentifier.IsType(type))
            {
                // Peek the token after identifier.
                Token afterIdentifier = stream.Peek(2);

                // Function definition.
                if (afterIdentifier.Type == TokenType.SymbolParenthesesL)
                {
                    // Invoke the function parser.
                    Function function = new FunctionParser().Parse(stream);

                    // Emit the function.
                    function.Emit(Module.Source);
                }
                // Otherwise, global variable declaration.
                else
                {
                    // Invoke the global variable parser.
                    GlobalVar globalVariable = new GlobalVarParser().Parse(stream);

                    // Emit the global variable.
                    globalVariable.Emit(Module.Source);
                }
            }
            // External definition.
            else if (type == TokenType.KeywordExternal)
            {
                // Invoke the external definition parser.
                Extern external = new ExternParser().Parse(stream);

                // Emit the external definition.
                external.Emit(Module.Source);
            }
            // Otherwise, top-level expression.
            else
            {
                // Invoke the top-level expression parser.
                Function exprDelegate = new TopLevelExprParser().Parse(stream);

                // Emit the top-level expression.
                exprDelegate.Emit(Module.Source);
            }

            // At this point, an entity was processed.
            return true;
        }
    }
}