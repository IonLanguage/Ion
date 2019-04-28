using LlvmSharpLang.Abstraction;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class Driver
    {
        protected readonly TokenStream stream;

        protected readonly Module module;

        public Driver(TokenStream stream)
        {
            this.stream = stream;
            this.module = new Module();
        }

        public Driver(Token[] tokens) : this(new TokenStream(tokens))
        {
            //
        }

        public void Next()
        {
            // TODO: What if EOF has not been processed?
            // End reached.
            if (this.stream.LastItem)
            {
                return;
            }

            switch (this.stream.Get().Type)
            {
                case TokenType.KeywordFunction:
                    {
                        // Invoke the function parser.
                        Function function = new FunctionParser().Parse(stream);

                        // Emit the function.
                        function.Emit(this.module.Source);

                        break;
                    }

                default:
                    {
                        // Invoke the top-level expression parser.
                        Function exprDelegate = new TopLevelExprParser().Parse(stream);

                        // Emit the top-level expression.
                        exprDelegate.Emit(this.module.Source);

                        break;
                    }
            }
        }
    }
}
