using NUnit.Framework;
using Ion.Misc;
using Ion.Tests.Core;
using Ion.SyntaxAnalysis;
using Ion.Optimization.Minification;

namespace Ion.Tests.Optimization
{
    [TestFixture]
    internal sealed class MinifierTests
    {
        [Test]
        public void Minify()
        {
            // Load the test token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("Minify");

            // Create the minifier instance.
            Minifier minifier = new Minifier();

            // Minify the stream.
            string actual = minifier.Minify(stream);

            // Load expected result.
            string expected = TestUtil.ReadOutputDataFile("Minified", "ion");

            // Compare results.
            Assert.AreEqual(expected, actual);
        }
    }
}
