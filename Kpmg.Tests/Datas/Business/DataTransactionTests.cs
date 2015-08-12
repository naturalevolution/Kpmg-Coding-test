using System.Linq;
using Kpmg.Datas.Business;
using Kpmg.Datas.Factories;
using NUnit.Framework;

namespace Kpmg.Tests.Datas.Business
{
    [TestFixture]
    public class DataTransactionTests : BaseDataTests
    {
        [TestCase(1, "Account is required.")]
        [TestCase(2, "Description is required.")]
        [TestCase(3, "Currency is required.")]
        [TestCase(4, "Amount is required.")]
        [TestCase(5, "Amount must be a decimal.")]
        [TestCase(6, "Currency must be a valid ISO 4217.")]
        public void Execute_Should_Check_Errors_On_Xlsx(int idLine, string errorMessage)
        {
            var dt = new DataTransaction();
            var results = dt.Execute(@"..\..\Datas\Business\FakeFiles\BadDatas.xlsx");

            var message = results.Messages.FirstOrDefault(x => x.IdLine == idLine);
            Assert.IsNotNull(message);
            Assert.AreEqual(errorMessage, message.Messages.FirstOrDefault());
            Assert.AreEqual(DataTransactionState.Ignored, message.State);
            Assert.AreEqual(0, Repositories.Informations.FindAll().Count);
        }

        [TestCase(1, "Account is required.")]
        [TestCase(2, "Description is required.")]
        [TestCase(3, "Currency is required.")]
        [TestCase(4, "Amount is required.")]
        [TestCase(5, "Amount must be a decimal.")]
        [TestCase(6, "Currency must be a valid ISO 4217.")]
        public void Execute_Should_Check_Errors_On_Csv(int idLine, string errorMessage)
        {
            var dt = new DataTransaction();
            var results = dt.Execute(@"..\..\Datas\Business\FakeFiles\BadDatas.csv");

            var message = results.Messages.FirstOrDefault(x => x.IdLine == idLine);
            Assert.IsNotNull(message);
            Assert.AreEqual(errorMessage, message.Messages.FirstOrDefault());
            Assert.AreEqual(DataTransactionState.Ignored, message.State);
            Assert.AreEqual(0, Repositories.Informations.FindAll().Count);
        }

        [Test]
        public void Execute_Should_Add_Information_On_Csv()
        {
            var dt = new DataTransaction();
            var results = dt.Execute(@"..\..\Datas\Business\FakeFiles\GoodDatas.csv");

            foreach (var dataTransaction in results.Messages)
            {
                Assert.IsNotNull(dataTransaction);
                Assert.AreEqual(0, dataTransaction.Messages.Count);
                Assert.AreEqual(DataTransactionState.Valid, dataTransaction.State);
            }
            var resultDatas = Repositories.Informations.FindAll();
            Assert.AreEqual(1, resultDatas.Count);
            Assert.AreEqual(10.55, resultDatas.ElementAt(0).Amount);
        }

        [Test]
        public void Execute_Should_Add_Information_On_Xlsx()
        {
            var dt = new DataTransaction();
            var results = dt.Execute(@"..\..\Datas\Business\FakeFiles\GoodDatas.xlsx");

            foreach (var dataTransaction in results.Messages)
            {
                Assert.IsNotNull(dataTransaction);
                Assert.AreEqual(0, dataTransaction.Messages.Count);
                Assert.AreEqual(DataTransactionState.Valid, dataTransaction.State);
            }
            var resultDatas = Repositories.Informations.FindAll();
            Assert.AreEqual(1, resultDatas.Count);
            Assert.AreEqual(10.55, resultDatas.ElementAt(0).Amount); 
        }
    }
}