using Kpmg.Datas.Factories;
using Kpmg.Datas.Models.Files;
using NUnit.Framework;

namespace Kpmg.Tests.Datas.Dao
{
    [TestFixture]
    public class InformationRepositoryTests : BaseDataTests
    {
        private Information GetDefaultEntity()
        {
            return new Information
            {
                Account = "myAccount",
                Amount = (decimal) 10.50,
                CurrencyCode = "EUR",
                Description = "myDescription"
            };
        }

        [Test]
        public void Insert_Should_RetrieveId_WhenInserted()
        {
            Repositories.Informations.Insert(GetDefaultEntity());
            Repositories.SaveChanges();

            var actual = Repositories.Informations.FindBy(x => x.Account == "myAccount");

            Assert.IsNotNull(actual);
            Assert.That(actual.Id > 0);
        }
    }
}