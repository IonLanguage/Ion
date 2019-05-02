using NUnit.Framework;
using Ion.CodeGeneration;
using System;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class TypeTests
    {
        [Test]
        public void ThrowsOnInvalidParams()
        {
            Assert.Throws<Exception>(() => new Ion.CodeGeneration.Type("test").Emit());
        }
    }
}
