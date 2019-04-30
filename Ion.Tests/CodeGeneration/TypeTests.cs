using NUnit.Framework;
using Ion.CodeGeneration;
using System;

namespace Ion.Tests.CodeGeneration
{
    internal sealed class TypeTests
    {
        [SetUp]
        public void SetUp()
        {
            //
        }

        [Test]
        public void ThrowsOnInvalidParams()
        {
            Assert.Throws<Exception>(() => new Ion.CodeGeneration.Type("test").Emit());
            Assert.Pass();
        }
    }
}
