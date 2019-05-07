using System.Text.RegularExpressions;
using Ion.Misc;

namespace Ion.CognitiveServices
{
    public static class Pattern
    {
        public static readonly Regex Identifier = Util.CreateRegex(@"[_a-z]+[_a-z0-9]*");

        public static readonly Regex String = Util.CreateRegex(@"""(\\.|[^\""\\])*""");

        public static readonly Regex Decimal = Util.CreateRegex(@"-?[0-9]+\.[0-9]+");

        public static readonly Regex Integer = Util.CreateRegex(@"-?[0-9]+");

        public static readonly Regex Character = Util.CreateRegex(@"'([^'\\\n]|\\.)'");

        public static readonly Regex ContinuousWhitespace = Util.CreateRegex(@"[\s]+");
    }
}
