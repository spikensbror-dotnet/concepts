using Concepts.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concepts.Tests
{
    [TestClass]
    public class MockAsyncCaptureExtensionsTests
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

            var mock = new Mock<ICaptureFixture>();

            var results = new List<string>();
            mock
                .Setup(f => f.DoSomethingAsync(It.IsAny<string>()))
                .Capture(results)
                .Returns(Task.CompletedTask);

            foreach (var key in expected)
            {
                await mock.Object.DoSomethingAsync(key);
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

            var mock = new Mock<ICaptureFixture>();

            var results = new List<Tuple<string, int>>();
            mock
                .Setup(f => f.DoSomethingAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Capture(results)
                .Returns(Task.CompletedTask);

            foreach (var tuple in expected)
            {
                await mock.Object.DoSomethingAsync(tuple.Item1, tuple.Item2);
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

            var mock = new Mock<ICaptureFixture>();

            var stringResults = new List<string>();
            var intResults = new List<int>();
            mock
                .Setup(f => f.DoSomethingAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Capture(stringResults, intResults);

            for (var i = 0; i < expectedStrings.Length; i++)
            {
                mock.Object.DoSomethingAsync(expectedStrings[i], expectedInts[i]);
            }

            CollectionAssert.AreEqual(expectedStrings, stringResults);
            CollectionAssert.AreEqual(expectedInts, intResults);
        }
    }
}
