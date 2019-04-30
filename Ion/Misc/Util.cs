using System.IO;
using System.Text.RegularExpressions;

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
    }
}

