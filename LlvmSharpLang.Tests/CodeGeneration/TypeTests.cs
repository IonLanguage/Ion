using NUnit.Framework;
using LlvmSharpLang.CodeGeneration;
using System;

namespace LlvmSharpLang.Tests.CodeGeneration
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
            Assert.Throws<Exception>(() => new LlvmSharpLang.CodeGeneration.Type("test").Emit());
            Assert.Pass();
        }
    }
}
