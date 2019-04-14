using LLVMSharp;
using LlvmSharpLang.CodeGen;
using LlvmSharpLang.Misc;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class ExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Create the expression.
            Expr expr = new Expr();

            // Capture explicit value token.
            Token valueToken = stream.Next();

            // Resolve the literal value.
            // TODO: Temporarily using Int32, should be dynamic.
            Resolver.Literal(valueToken, TypeFactory.Int32.Emit());

            // TODO: Add support for complex expressions.

            return expr;
        }
    }
}
