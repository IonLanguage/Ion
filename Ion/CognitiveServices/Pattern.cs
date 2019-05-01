using System.Text.RegularExpressions;

namespace Ion.CognitiveServices
{
    public static class Pattern
    {
        public static Regex Identifier = Util.CreateRegex(@"[_a-z]+[_a-z0-9]*");

        public static Regex String = Util.CreateRegex(@"""(\\.|[^\""\\])*""");

        public static Regex Decimal = Util.CreateRegex(@"-?[0-9]+\.[0-9]+");

        public static Regex Integer = Util.CreateRegex(@"-?[0-9]+");

        public static Regex Character = Util.CreateRegex(@"'([^'\\\n]|\\.)'");
    }
}
