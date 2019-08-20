using Concepts.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Concepts.Tests
{
    [TestClass]
    public class MockCaptureExtensionsTests
    {
        [TestMethod]
        public void ShouldBeAbleToCaptureSingleParameterMethodArguments()
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
                .Setup(f => f.DoSomething(It.IsAny<string>()))
                .Capture(results);

            foreach (var key in expected)
            {
                mock.Object.DoSomething(key);
            }

            CollectionAssert.AreEqual(expected, results);
        }

        [TestMethod]
        public void ShouldBeAbleToCaptureTwoParameterMethodArgumentsAsTuples()
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
                .Setup(f => f.DoSomething(It.IsAny<string>(), It.IsAny<int>()))
                .Capture(results);

            foreach (var tuple in expected)
            {
                mock.Object.DoSomething(tuple.Item1, tuple.Item2);
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
                .Setup(f => f.DoSomething(It.IsAny<string>(), It.IsAny<int>()))
                .Capture(stringResults, intResults);

            for (var i = 0; i < expectedStrings.Length; i++)
            {
                mock.Object.DoSomething(expectedStrings[i], expectedInts[i]);
            }

            CollectionAssert.AreEqual(expectedStrings, stringResults);
            CollectionAssert.AreEqual(expectedInts, intResults);
        }
    }
}
