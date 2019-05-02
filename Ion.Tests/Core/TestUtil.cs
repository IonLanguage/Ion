using System.IO;

namespace Ion.Tests.Core
{
    internal sealed class TestUtil
    {
        public static string dataDir = "../../../Data/";

        public static string ResolveDataPath(string path)
        {
            return Path.Join(TestUtil.dataDir, path);
        }

        public static string ReadDataFile(string path)
        {
            // Read the file contents.
            string content = File.ReadAllText(TestUtil.ResolveDataPath(path));

            // Trim any whitespace.
            content = content.Trim();

            // Remove platform-specific encoding.
            content = content.Replace("\r", "");

            return content;
        }
    }
}
