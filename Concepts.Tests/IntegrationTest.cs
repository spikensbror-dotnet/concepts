using Concepts.Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Concepts.Tests
{
    [TestClass]
    public abstract class IntegrationTest
    {
        protected Concept Concept { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            this.Concept = new Concept();
            this.Concept.Builder.RegisterForTest();
        }
    }
}
