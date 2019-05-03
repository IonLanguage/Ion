using System.Text.RegularExpressions;
using Ion.Misc;

namespace Ion.CognitiveServices
{
    public static class Pattern
    {
        public static readonly Regex identifier = Util.CreateRegex(@"[_a-z]+[_a-z0-9]*");

        public static readonly Regex String = Util.CreateRegex(@"""(\\.|[^\""\\])*""");

        public static readonly Regex Decimal = Util.CreateRegex(@"-?[0-9]+\.[0-9]+");

        public static readonly Regex integer = Util.CreateRegex(@"-?[0-9]+");

        public static readonly Regex character = Util.CreateRegex(@"'([^'\\\n]|\\.)'");
    }
}