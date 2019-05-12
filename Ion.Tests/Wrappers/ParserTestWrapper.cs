using System;
using Ion.Parsing;
using NUnit.Framework;

namespace Ion.Tests.Core
{
    public class ParserTestWrapper
    {
        public Driver Driver { get; protected set; }

        public bool Prepared => this.Driver != null;

        protected string inputFilename;

        public void Prepare(string inputFilename)
        {
            // Save input file name for future use.
            this.inputFilename = inputFilename;

            // Create and attach the driver.
            this.Driver = TestUtil.CreateDriverFromInputDataFile(this.inputFilename);
        }

        protected void EnsurePreparedOrThrow()
        {
            if (!this.Prepared)
            {
                throw new InvalidOperationException("Cannot proceed without preparing instance first");
            }
        }

        /// <summary>
        /// Invoke the driver the specified amount of times,
        /// ensuring that the driver has next on each iteration,
        /// and does not on the last one.
        /// </summary>
        public void InvokeDriver(int times)
        {
            // Ensure instance has been prepared.
            this.EnsurePreparedOrThrow();

            // Ensure times parameter is valid.
            if (times <= 0)
            {
                throw new ArgumentOutOfRangeException("The amount of times parameter must be one or higher");
            }

            for (int i = 0; i < times; i++)
            {
                // Expect the driver to have next.
                Assert.True(this.Driver.HasNext);

                // Invoke the driver.
                this.Driver.Next();
            }

            // Expect the driver to not have next.
            Assert.False(this.Driver.HasNext);
        }

        /// <summary>
        /// Assert that the driver's module IR code matches
        /// the expected output located under the provided
        /// input filename in the corresponding test data directory.
        /// </summary>
        public void Compare()
        {
            // Emit the driver's module.
            string actual = this.Driver.Module.ToString();

            // Load expected result.
            string expected = TestUtil.ReadOutputDataFile(this.inputFilename);

            // Compare results.
            Assert.AreEqual(expected, actual);
        }
    }
}
