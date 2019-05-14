namespace Concepts.Tests.Fixtures
{
    interface IFixture
    {
        string GetCombinedStrings();
        void DoSomething(string key);
        void DoSomething(string key, int value);
    }
}
