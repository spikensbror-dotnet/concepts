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
                var fixture = container.Resolve<IFixture>();

                Assert.AreEqual("Hello World", container.Resolve<IFixture>().GetCombinedStrings());
            }
        }

        [TestMethod]
        public void ShouldSubOutServicesAsSingletons()
        {
            Assert.AreSame(this.Concept.Mock<IStringService>(), this.Concept.Mock<IStringService>());

            using (var container = this.Concept.Build())
            {
                Assert.AreSame(container.Resolve<IStringService>(), container.Resolve<IStringService>());
                Assert.AreSame(this.Concept.Mock<IStringService>().Object, container.Resolve<IStringService>());
            }
        }
    }
}
