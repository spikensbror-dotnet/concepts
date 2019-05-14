namespace Concepts.Tests.Fixtures
{
    class Fixture : IFixture
    {
        private readonly IStringService stringService;
        private readonly IOtherStringService otherStringService;

        public Fixture(IStringService stringService
            , IOtherStringService otherStringService
            )
        {
            this.stringService = stringService;
            this.otherStringService = otherStringService;
        }

        public void DoSomething(string key)
        {
            //
        }

        public void DoSomething(string key, int value)
        {
            //
        }

        public string GetCombinedStrings()
        {
            return $"{this.stringService.GetString()} {this.otherStringService.GetString()}";
        }
    }
}
