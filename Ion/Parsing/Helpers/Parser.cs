using Ion.Engine.Parsing;
using Ion.Syntax;

namespace Ion.Parsing
{
    public interface IStreamParser<T>
    {
        T Parse(TokenStream stream);
    }

    public interface IParser<T> : IGenericParser<T, ParserContext>
    {
        //
    }
}
