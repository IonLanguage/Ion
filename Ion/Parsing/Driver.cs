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
        public bool HasNext => !this.stream.IsLastItem;

        public ParserContext ParserContext { get; }

        public Driver(TokenStream stream, string name)
        {
            this.stream = stream;

            // Create a new module instance.
            this.Module = new Module(name);

            // Create a new parser context instance.
            this.ParserContext = new ParserContext(this, this.stream);
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
                    Function function = new FunctionParser().Parse(this.ParserContext);

                    // Emit the function.
                    function.Emit(this.Module.Source);
                }
                // Otherwise, global variable declaration.
                else
                {
                    // Invoke the global variable parser.
                    GlobalVar globalVariable = new GlobalVarParser().Parse(this.ParserContext);

                    // Emit the global variable.
                    globalVariable.Emit(this.Module.Source);
                }
            }
            // External definition.
            else if (type == TokenType.KeywordExternal)
            {
                // Invoke the external definition parser.
                Extern external = new ExternParser().Parse(this.ParserContext);

                // Emit the external definition.
                external.Emit(this.Module.Source);
            }
            // TODO: Enforce a single namespace definition per-file.
            // Namespace definition.
            else if (type == TokenType.KeywordNamespace)
            {
                // Invoke the namespace definition parser.
                Namespace namespaceEntity = new NamespaceParser().Parse(this.ParserContext);

                // Process the namespace definition reaction.
                namespaceEntity.React(this.Module.Source);
            }
            // Otherwise, throw an error.
            else
            {
                throw new Exception("Unexpected top-level entity");
            }

            // At this point, an entity was processed.
            return true;
        }
    }
}
