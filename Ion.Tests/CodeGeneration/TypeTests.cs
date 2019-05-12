using NUnit.Framework;
using Ion.CodeGeneration;
using System;
using Ion.Core;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class TypeTests
    {
        [SetUp]
        public static void Setup()
        {
            // Reset the name counter before every test.
            NameCounter.ResetAll();
        }

        [Test]
        public void ThrowsOnInvalidParams()
        {
            Assert.Throws<Exception>(() => new Ion.CodeGeneration.Type("test").Emit());
        }
    }
}
