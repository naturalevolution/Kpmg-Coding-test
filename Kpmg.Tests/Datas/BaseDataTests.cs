using Kpmg.Datas.Factories;
using Kpmg.Tests.Datas.Memory;
using NUnit.Framework;

namespace Kpmg.Tests.Datas
{
    [TestFixture]
    public abstract class BaseDataTests
    {
        [SetUp]
        public void SetUp()
        {
            Repositories.Load(new Repositories());
            Repositories.LoadContext(new FakeKpmgContext());
        }
    }
}