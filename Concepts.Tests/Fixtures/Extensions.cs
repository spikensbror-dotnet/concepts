using Autofac;

namespace Concepts.Tests.Fixtures
{
    static class Extensions
    {
        public static void RegisterForTest(this ContainerBuilder builder)
        {
            builder.RegisterType<Fixture>()
                .As<IFixture>()
                .SingleInstance();

            builder.RegisterType<StringService>()
                .As<IStringService>()
                .As<IOtherStringService>()
                .SingleInstance();
        }
    }
}
