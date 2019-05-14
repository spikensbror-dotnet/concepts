using Autofac;
using Concepts.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Concepts.Tests
{
    [TestClass]
    public class ConceptTests : IntegrationTest
    {
        [TestMethod]
        public void ShouldDisplayNormalBehaviorWhenNotMocked()
        {
            using (var container = this.Concept.Build())
            {
                Assert.AreEqual("Hello Hello", container.Resolve<IFixture>().GetCombinedStrings());
            }
        }

        [TestMethod]
        public void ShouldBeAbleToStubOutServices()
        {
            this.Concept.Mock<IOtherStringService>()
                .Setup(oss => oss.GetString())
                .Returns("World");

            using (var container = this.Concept.Build())
            {
                Assert.AreEqual("Hello World", container.Resolve<IFixture>().GetCombinedStrings());
            }
        }
    }
}
