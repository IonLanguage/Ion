using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public interface IParser<T>
    {
        T Parse(TokenStream stream);
    }
}