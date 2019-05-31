using System.Collections.Generic;
using System.IO;
using Ion.Parsing;
using Ion.SyntaxAnalysis;

namespace Ion.Tests.Core
{
    internal sealed class TestUtil
    {
        public const string DataDir = "../../../Data/";

        public static string ResolveDataPath(string path)
        {
            return Path.Join(TestUtil.DataDir, path);
        }

        public static string ReadDataFile(string path)
        {
            // Read the file contents.
            string content = File.ReadAllText(TestUtil.ResolveDataPath(path));

            // Trim extra whitespace.
            content = content.Trim();

            // Remove platform-specific encoding.
            content = content.Replace("\r", "");

            // Return the processed content string.
            return content;
        }

        public static string ReadOutputDataFile(string name, string extension = "ll")
        {
            return TestUtil.ReadDataFile($"Output/{name}.{extension}");
        }

        public static string ReadInputDataFile(string name, string extension = "ion")
        {
            return TestUtil.ReadDataFile($"Input/{name}.{extension}");
        }

        public static TokenStream CreateStreamFromInputDataFile(string path)
        {
            // Read the input file's content.
            string input = TestUtil.ReadInputDataFile(path);

            // Create the stream.
            TokenStream stream = TokenStreamFactory.CreateFromInput(input);

            // Return the stream.
            return stream;
        }

        public static Driver CreateDriverFromInputDataFile(string path)
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile(path);

            // Create the new driver instance.
            Driver driver = new Driver(stream);

            // Return the created driver.
            return driver;
        }
    }
}
