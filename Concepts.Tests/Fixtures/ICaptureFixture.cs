using System.Threading.Tasks;

namespace Concepts.Tests.Fixtures
{
    interface ICaptureFixture
    {
        void DoSomething(string key);
        void DoSomething(string key, int value);
        Task DoSomethingAsync(string key);
        Task DoSomethingAsync(string key, int value);
    }
}
