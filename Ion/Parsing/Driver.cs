using Ion.Abstraction;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;
using Ion.CognitiveServices;

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

        public void Next()
        {
            // TODO: What if EOF has not been processed?
            // End reached.
            if (this.stream.LastItem)
            {
                return;
            }

            TokenType type = this.stream.Get().Type;

            // Function definition or global variable.
            if (TokenIdentifier.IsType(type))
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
        }
    }
}
