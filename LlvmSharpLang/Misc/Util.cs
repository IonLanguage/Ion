using System.Text.RegularExpressions;

namespace LlvmSharpLang.Misc
{
    public static class Util
    {
        /// <summary>
        /// Create a Regex class with the provided pattern
        /// string along with the IgnoreCase and Compiled regex
        /// options.
        /// </summary>
        public static Regex CreateRegex(string pattern)
        {
            return new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    }
}

