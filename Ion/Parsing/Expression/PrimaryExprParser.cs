using Ion.CognitiveServices;
using Ion.Generation;
using Ion.Misc;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class PrimaryExprParser : IParser<Construct>
    {
        public Construct Parse(ParserContext context)
        {
            // Capture current token type.
            TokenType currentTokenType = context.Stream.Current.Type;

            // Pipe operation.
            if (currentTokenType == TokenType.SymbolColon)
            {
                return new PipeParser().Parse(context);
            }
            // Numeric expression.
            else if (TokenIdentifier.IsNumeric(currentTokenType))
            {
                return new NumericExprParser().Parse(context);
            }
            // Identifier expression.
            else if (currentTokenType == TokenType.Identifier)
            {
                return new IdentifierExprParser().Parse(context);
            }
            // Parentheses expression.
            else if (currentTokenType == TokenType.SymbolParenthesesL)
            {
                return new ParenthesesExprParser().Parse(context);
            }
            // String expression.
            else if (currentTokenType == TokenType.LiteralString)
            {
                return new StringExprParser().Parse(context);
            }
            // Boolean expression.
            else if (TokenIdentifier.IsBoolean(currentTokenType))
            {
                return new BooleanExprParser().Parse(context);
            }
            // Struct expression.
            else if (currentTokenType == TokenType.KeywordNew)
            {
                return new StructExprParser().Parse(context);
            }
            // Array expression.
            else if (currentTokenType == TokenType.SymbolBracketL)
            {
                // TODO: Type is hard-coded for debugging purposes, not yet supported auto-type (might need infering?).
                return new ArrayExprParser(PrimitiveTypeFactory.Int32()).Parse(context);
            }

            // At this point, return null.
            return null;
        }
    }
}
