namespace Concepts.Tests.Fixtures
{
    class StringService : IStringService, IOtherStringService
    {
        public string GetString()
        {
            return "Hello";
        }
    }
}
