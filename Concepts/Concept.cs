using Autofac;
using Moq;
using System;
using System.Collections.Generic;

namespace Concepts
{
    /// <summary>
    /// A concept provides easy mocking for a given unit, resulting in an Autofac container used
    /// to resolve the unit that should be tested.
    /// </summary>
    public class Concept
    {
        private readonly Dictionary<Type, Mock> mocks = new Dictionary<Type, Mock>();

        /// <summary>
        /// Constructs a new concept.
        /// </summary>
        public Concept()
        {
            this.Builder = new ContainerBuilder();
        }

        /// <summary>
        /// The Autofac container builder of the concept.
        /// </summary>
        public ContainerBuilder Builder { get; }

        /// <summary>
        /// Stubs the given service type with a mock.
        /// </summary>
        /// <typeparam name="TService">The type of service to stub.</typeparam>
        /// <returns>The resulting mock.</returns>
        public Mock<TService> Mock<TService>() where TService : class
        {
            lock (this.mocks)
            {
                var type = typeof(TService);
                if (this.mocks.ContainsKey(type))
                {
                    return (Mock<TService>)this.mocks[type];
                }

                var mock = new Mock<TService>();

                this.Builder.RegisterInstance(mock.Object)
                    .As<TService>()
                    .SingleInstance();

                this.mocks[type] = mock;

                return mock;
            }
        }

        /// <summary>
        /// Builds an Autofac container from the given concept.
        /// </summary>
        /// <returns></returns>
        public IContainer Build()
        {
            return this.Builder.Build();
        }
    }
}
