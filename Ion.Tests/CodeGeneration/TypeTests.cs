using NUnit.Framework;
using Ion.CodeGeneration;
using System;
using Ion.Core;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class TypeTests : ConstructTest
    {
        [Test]
        public void ThrowsOnInvalidParams()
        {
            Assert.Throws<Exception>(() => new PrimitiveType("test").Emit());
        }

        [Test]
        public void Pointer()
        {
            // TODO: Finish testing implementation.
            this.Wrapper.Prepare("Pointer");

            this.Wrapper.InvokeDriver();

            System.Console.WriteLine(this.Wrapper.Driver.Module.Emit());

            this.Wrapper.Compare();
        }
    }
}
