using System;
using System.Globalization;
using System.Linq;

namespace LlvmSharpLang.Misc
{
    public static class Extension
    {
        public static string Capitalize(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));

                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));

                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}
