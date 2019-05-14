using Autofac;
using Concepts.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Concepts.Tests
{
    [TestClass]
    public class MockCaptureExtensionsTests : IntegrationTest
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

            var results = new List<string>();
            this.Concept.Mock<IFixture>()
                .Setup(f => f.DoSomething(It.IsAny<string>()))
                .Capture(results);

            using (var container = this.Concept.Build())
            {
                var fixture = container.Resolve<IFixture>();

                foreach (var key in expected)
                {
                    fixture.DoSomething(key);
                }
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

            var results = new List<Tuple<string, int>>();
            this.Concept.Mock<IFixture>()
                .Setup(f => f.DoSomething(It.IsAny<string>(), It.IsAny<int>()))
                .Capture(results);

            using (var container = this.Concept.Build())
            {
                var fixture = container.Resolve<IFixture>();

                foreach (var tuple in expected)
                {
                    fixture.DoSomething(tuple.Item1, tuple.Item2);
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
            this.Concept.Mock<IFixture>()
                .Setup(f => f.DoSomething(It.IsAny<string>(), It.IsAny<int>()))
                .Capture(stringResults, intResults);

            using (var container = this.Concept.Build())
            {
                var fixture = container.Resolve<IFixture>();

                for (var i = 0; i < expectedStrings.Length; i++)
                {
                    fixture.DoSomething(expectedStrings[i], expectedInts[i]);
                }
            }

            CollectionAssert.AreEqual(expectedStrings, stringResults);
            CollectionAssert.AreEqual(expectedInts, intResults);
        }
    }
}
