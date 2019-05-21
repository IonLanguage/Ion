using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public interface IStreamParser<T>
    {
        T Parse(TokenStream stream);
    }

    public interface IParser<T>
    {
        T Parse(ParserContext context);
    }
}
