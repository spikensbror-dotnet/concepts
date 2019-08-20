using Autofac;
using Concepts.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Concepts.Tests
{
    [TestClass]
    public class ConceptTests
    {
        private Concept concept;

        [TestInitialize]
        public void Initialize()
        {
            this.concept = new Concept();

            this.concept.Builder.RegisterType<Fixture>()
                .As<IFixture>()
                .SingleInstance();

            this.concept.Builder.RegisterType<StringService>()
                .As<IStringService>()
                .As<IOtherStringService>()
                .SingleInstance();
        }

        [TestMethod]
        public void ShouldDisplayNormalBehaviorWhenNotMocked()
        {
            using (var container = this.concept.Build())
            {
                Assert.AreEqual("Hello Hello", container.Resolve<IFixture>().GetCombinedStrings());
            }
        }

        [TestMethod]
        public void ShouldBeAbleToStubOutServices()
        {
            this.concept.Mock<IOtherStringService>()
                .Setup(oss => oss.GetString())
                .Returns("World");

            using (var container = this.concept.Build())
            {
                var fixture = container.Resolve<IFixture>();

                Assert.AreEqual("Hello World", container.Resolve<IFixture>().GetCombinedStrings());
            }
        }

        [TestMethod]
        public void ShouldStubOutServicesAsSingletons()
        {
            Assert.AreSame(this.concept.Mock<IStringService>(), this.concept.Mock<IStringService>());

            using (var container = this.concept.Build())
            {
                Assert.AreSame(container.Resolve<IStringService>(), container.Resolve<IStringService>());
                Assert.AreSame(this.concept.Mock<IStringService>().Object, container.Resolve<IStringService>());
            }
        }
    }
}
