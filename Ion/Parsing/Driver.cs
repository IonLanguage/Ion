using System;
using Ion.CodeGeneration;
using Ion.CodeGeneration.Structure;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class Driver
    {
        public Module Module { get; protected set; }

        // TODO: What if EOF token has not been processed itself?
        public bool HasNext => !this.stream.IsLastItem;

        public ParserContext ParserContext { get; protected set; }

        public PipeContext<CodeGeneration.Module> ModulePipeContext { get; protected set; }

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

        // TODO: DriverResult should include the module?
        public DriverResult Invoke()
        {
            // Invoke the driver.
            while (this.HasNext)
            {
                this.Next();
            }

            // Create and populate the driver result.
            DriverResult result = new DriverResult
            {
                Notices = this.ParserContext.NoticeRepository.GetStack(),
                OutputIr = this.ParserContext.Driver.Module.Emit()
            };

            // Return the result.
            return result;
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
            this.Module = new CodeGeneration.Module(name);

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

            // Create and invoke the top level parser.
            new TopLevelParser(this.Module, this.ModulePipeContext).Parse(this.ParserContext);

            // At this point, an entity was processed.
            return true;
        }
    }
}
