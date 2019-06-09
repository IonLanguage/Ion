using System;
using System.IO;
using System.Text.RegularExpressions;
using Ion.CognitiveServices;
using Ion.Syntax;

namespace Ion.Misc
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

        /// <summary>
        /// Determine whether a path is a directory.
        /// </summary>
        public static bool IsDirectory(string path)
        {
            // Retrieve the attributes of the path.
            FileAttributes attributes = File.GetAttributes(path);

            // Return whether the path is a directory.
            return attributes.HasFlag(FileAttributes.Directory);
        }

        public static bool ValidateIdentifier(string identifier)
        {
            // Ensure name is not null nor empty.
            if (string.IsNullOrEmpty(identifier))
            {
                throw new Exception("Unexpected name to be null or empty");
            }

            // Ensure identifier pattern matches provided name.
            return Pattern.Identifier.IsMatch(identifier);
        }

        public static string ExtractStringLiteralValue(string stringLiteral)
        {
            return stringLiteral.Substring(1, stringLiteral.Length - 2);
        }

        public static string ExtractStringLiteralValue(Token token)
        {
            return Util.ExtractStringLiteralValue(token.Value);
        }
    }
}
