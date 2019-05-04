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

        public Module Module { get; }

        // TODO: What if EOF token has not been processed itself?
        public bool HasNext
        {
            get
            {
                return !this.stream.IsLastItem;
            }
        }

        public Driver(TokenStream stream)
        {
            this.stream = stream;
            this.Module = new Module();
        }

        public Driver(Token[] tokens) : this(new TokenStream(tokens))
        {
            //
        }

        /// <summary>
        /// Process the next sequence. Returns true
        /// if the sequence was successfully processed.
        /// </summary>
        public bool Next()
        {
            // TODO: What if EOF token has not been processed itself?
            // End reached.
            if (this.stream.IsLastItem)
            {
                return false;
            }

            // TODO: Finish fixing this, parsers overflowing (+1) because of this issue with the Program start (05/02/2019).
            TokenType type = this.stream.Get().Type;

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
                Token afterIdentifier = this.stream.Peek(2);

                // Function definition.
                if (afterIdentifier.Type == TokenType.SymbolParenthesesL)
                {
                    // Invoke the function parser.
                    Function function = new FunctionParser().Parse(this.stream);

                    // Emit the function.
                    function.Emit(this.Module.Source);
                }
                // Otherwise, global variable declaration.
                else
                {
                    // Invoke the global variable parser.
                    GlobalVar globalVariable = new GlobalVarParser().Parse(this.stream);

                    // Emit the global variable.
                    globalVariable.Emit(this.Module.Source);
                }
            }
            // External definition.
            else if (type == TokenType.KeywordExternal)
            {
                // Invoke the external definition parser.
                Extern external = new ExternParser().Parse(this.stream);

                // Emit the external definition.
                external.Emit(this.Module.Source);
            }
            // Otherwise, top-level expression.
            else
            {
                // Invoke the top-level expression parser.
                Function exprDelegate = new TopLevelExprParser().Parse(this.stream);

                // Emit the top-level expression.
                exprDelegate.Emit(this.Module.Source);
            }

            // At this point, an entity was processed.
            return true;
        }
    }
}
