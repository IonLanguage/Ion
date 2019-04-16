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
            LLVMValueRef? value = null;

            // Literal integer value.
            if (Pattern.Integer.IsMatch(valueToken.Value))
            {
                value = Resolver.Literal(valueToken, TypeFactory.Int32.Emit());
            }
            // TODO: Should determine whether Double or Float.
            // Literal decimal value.
            else if (Pattern.Decimal.IsMatch(valueToken.Value))
            {
                value = Resolver.Literal(valueToken, TypeFactory.Double.Emit()); ;
            }

            // Gather contextual info from upcoming token.
            Token nextToken = stream.Peek();

            // Expression is simply a single literal.
            if (nextToken.Type == TokenType.SymbolSemiColon)
            {
                expr.ExplicitValue = value;

                return expr;
            }

            // TODO: Add support for complex expressions.

            return expr;
        }
    }
}
