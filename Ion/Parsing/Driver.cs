using System;
using Ion.Abstraction;
using Ion.CodeGeneration;
using Ion.CodeGeneration.Structure;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.Parsing
{
    public class Driver
    {
        public Abstraction.Module Module { get; protected set; }

        // TODO: What if EOF token has not been processed itself?
        public bool HasNext => !this.stream.IsLastItem;

        public ParserContext ParserContext { get; protected set; }

        public PipeContext<LLVMModuleRef> ModulePipeContext { get; protected set; }

        protected TokenStream stream;

        public Driver(TokenStream stream, string name)
        {
            // Invoke the initializer method.
            this.Init(stream, name);
        }

        public Driver(TokenStream stream) : this(stream, SpecialName.Entry)
        {
            //
        }

        public Driver(Token[] tokens) : this(new TokenStream(tokens))
        {
            //
        }

        /// <summary>
        /// Initialize the values and properties of this class
        /// instance.
        /// </summary>
        protected void Init(TokenStream stream, string name)
        {
            // Assign the provided stream.
            this.stream = stream;

            // Create a new module instance.
            this.Module = new Abstraction.Module(name);

            // Create a new parser context instance.
            this.ParserContext = new ParserContext(this, this.stream);

            // Create a generic pipe context for potential use.
            this.ModulePipeContext = PipeContextFactory.CreateFromModule(this.Module);
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

            // Retrieve the current token.
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
                    function.Emit(this.ModulePipeContext);
                }
                // Otherwise, global variable declaration.
                else
                {
                    // Invoke the global variable parser.
                    GlobalVar globalVariable = new GlobalVarParser().Parse(this.ParserContext);

                    // Emit the global variable.
                    globalVariable.Emit(this.ModulePipeContext);
                }
            }
            // External definition.
            else if (type == TokenType.KeywordExternal)
            {
                // Invoke the external definition parser.
                Extern external = new ExternParser().Parse(this.ParserContext);

                // Emit the external definition.
                external.Emit(this.ModulePipeContext);
            }
            // TODO: Enforce a single namespace definition per-file.
            // Namespace definition.
            else if (type == TokenType.KeywordNamespace)
            {
                // Invoke the namespace definition parser.
                Namespace namespaceEntity = new NamespaceParser().Parse(this.ParserContext);

                // Process the namespace definition reaction.
                namespaceEntity.Invoke(this.Module.Target);
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
