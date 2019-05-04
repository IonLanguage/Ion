using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ion.Linking
{
    public class ScannerOptions
    {
        /// <summary>
        /// The root, starting path of the scanner.
        /// </summary>
        public string Root { get; set; }

        /// <summary>
        /// The Regex string pattern(s) that will be
        /// used to determine which files to match.
        /// </summary>
        public string[] Match { get; set; }

        /// <summary>
        /// The Regex string pattern(s) that will be used
        /// to determine which files to exclude from
        /// matching.
        /// </summary>
        public string[] Exclude { get; set; }

        public Regex[] CompiledMatches { get; protected set; }

        public Regex[] CompiledExclusions { get; protected set; }

        public void Init()
        {
            // Populate compiled matches.
            this.CompiledMatches = this.Match.Select(match =>
                new Regex(match)).ToArray();

            // Populate compiled exclusions.
            this.CompiledExclusions = this.Exclude.Select(exclusion =>
                new Regex(exclusion)).ToArray();
        }

        /// <summary>
        /// Determine whether a path matches an
        /// exclusion.
        /// </summary>
        public bool IsExcluded(string path)
        {
            // Obtain the basename of the path.
            string baseName = Path.GetFileName(path);

            // Loop through all compiled exclusions.
            return this.CompiledExclusions.Any(exclude => exclude.IsMatch(baseName));
        }

        /// <summary>
        /// Determine whether a path matches.
        /// </summary>
        public bool IsMatch(string path)
        {
            // Obtain the basename of the path.
            string baseName = Path.GetFileName(path);

            // Loop through all compiled exclusions.
            return this.CompiledMatches.Any(match => match.IsMatch(baseName));
        }
    }
}
