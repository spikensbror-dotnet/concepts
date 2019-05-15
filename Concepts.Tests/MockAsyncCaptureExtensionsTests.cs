using Autofac;
using Concepts.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concepts.Tests
{
    [TestClass]
    public class MockAsyncCaptureExtensionsTests : IntegrationTest
    {
        [TestMethod]
        public async Task ShouldBeAbleToCaptureSingleParameterMethodArguments()
        {
            var expected = new[]
            {
                "first",
                "second",
                "third",
            };

            var results = new List<string>();
            this.Concept.Mock<ICaptureFixture>()
                .Setup(f => f.DoSomethingAsync(It.IsAny<string>()))
                .Capture(results)
                .Returns(Task.CompletedTask);

            using (var container = this.Concept.Build())
            {
                var fixture = container.Resolve<ICaptureFixture>();

                foreach (var key in expected)
                {
                    await fixture.DoSomethingAsync(key);
                }
            }

            CollectionAssert.AreEqual(expected, results);
        }

        [TestMethod]
        public async Task ShouldBeAbleToCaptureTwoParameterMethodArgumentsAsTuples()
        {
            var expected = new[]
            {
                new Tuple<string, int>("first", 42),
                new Tuple<string, int>("second", 43),
                new Tuple<string, int>("third", 44),
            };

            var results = new List<Tuple<string, int>>();
            this.Concept.Mock<ICaptureFixture>()
                .Setup(f => f.DoSomethingAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Capture(results)
                .Returns(Task.CompletedTask);

            using (var container = this.Concept.Build())
            {
                var fixture = container.Resolve<ICaptureFixture>();

                foreach (var tuple in expected)
                {
                    await fixture.DoSomethingAsync(tuple.Item1, tuple.Item2);
                }
            }

            CollectionAssert.AreEqual(expected, results);
        }

        [TestMethod]
        public void ShouldBeAbleToCaptureTwoParameterMethodArgumentsAsLists()
        {
            var expectedStrings = new[]
            {
                "first",
                "second",
                "third",
            };

            var expectedInts = new[]
            {
                42,
                43,
                44
            };

            var stringResults = new List<string>();
            var intResults = new List<int>();
            this.Concept.Mock<ICaptureFixture>()
                .Setup(f => f.DoSomethingAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Capture(stringResults, intResults);

            using (var container = this.Concept.Build())
            {
                var fixture = container.Resolve<ICaptureFixture>();

                for (var i = 0; i < expectedStrings.Length; i++)
                {
                    fixture.DoSomethingAsync(expectedStrings[i], expectedInts[i]);
                }
            }

            CollectionAssert.AreEqual(expectedStrings, stringResults);
            CollectionAssert.AreEqual(expectedInts, intResults);
        }
    }
}
