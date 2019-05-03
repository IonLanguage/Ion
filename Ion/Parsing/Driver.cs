using Ion.Abstraction;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;
using Ion.CognitiveServices;
using System;

namespace Ion.Parsing
{
    public class Driver
    {
        public Module Module { get; protected set; }

        protected readonly TokenStream stream;

        public Driver(TokenStream stream)
        {
            this.stream = stream;
            this.Module = new Module();
        }

        public Driver(Token[] tokens) : this(new TokenStream(tokens))
        {
            //
        }

        // TODO: What if EOF token has not been processed itself?
        public bool HasNext
        {
            get
            {
                TokenType currentType = this.stream.Get().Type;
                TokenType nextType = this.stream.Peek().Type;

                return currentType != TokenType.ProgramEnd && nextType != TokenType.ProgramEnd;
            }
        }

        /// <summary>
        /// Process the next sequence. Returns true
        /// if the sequence was successfully processed.
        /// </summary>
        public bool Next()
        {
            // TODO: What if EOF token has not been processed itself?
            // End reached.
            if (this.stream.LastItem)
            {
                return false;
            }

            TokenType type = this.stream.Get().Type;

            // Skip program start token.
            if (type == TokenType.ProgramStart)
            {
                this.stream.Skip();

                // Assign type as next token type, continue execution.
                type = this.stream.Get().Type;
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
                    function.Emit(this.Module.Source);
                }
                // Otherwise, global variable declaration.
                else
                {
                    // Invoke the global variable parser.
                    GlobalVar globalVariable = new GlobalVarParser().Parse(stream);

                    // Emit the global variable.
                    globalVariable.Emit(this.Module.Source);
                }
            }
            // External definition.
            else if (type == TokenType.KeywordExternal)
            {
                // Invoke the external definition parser.
                Extern external = new ExternParser().Parse(stream);

                // Emit the external definition.
                external.Emit(this.Module.Source);
            }
            // Otherwise, top-level expression.
            else
            {
                // Invoke the top-level expression parser.
                Function exprDelegate = new TopLevelExprParser().Parse(stream);

                // Emit the top-level expression.
                exprDelegate.Emit(this.Module.Source);
            }

            // At this point, an entity was processed.
            return true;
        }
    }
}
