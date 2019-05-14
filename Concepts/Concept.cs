using Autofac;
using Moq;
using System;
using System.Collections.Generic;

namespace Concepts
{
    public class Concept
    {
        private readonly Dictionary<Type, Mock> mocks = new Dictionary<Type, Mock>();

        public Concept()
        {
            this.Builder = new ContainerBuilder();
        }

        public ContainerBuilder Builder { get; }

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

        public IContainer Build()
        {
            return this.Builder.Build();
        }
    }
}
