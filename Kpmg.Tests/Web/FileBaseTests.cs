using System.Web;
using Kpmg.Web.Common.Extensions;
using Moq;
using NUnit.Framework;

namespace Kpmg.Tests.Web
{
    [TestFixture]
    public class FileBaseTests : BaseWebTests
    {
        [TestCase("xlsx", true)]
        [TestCase("csv", true)]
        [TestCase("sql", false)]
        [TestCase("txt", false)]
        public void IsValidForUpload_Should_Check_Valid_Extensions(string extensionTested, bool mustBeValid)
        {
            var uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(f => f.ContentLength).Returns(10);
            uploadedFile.Setup(f => f.FileName).Returns(string.Format("{0}.{1}", "test", extensionTested));

            Assert.AreEqual(uploadedFile.Object.IsValidForUpload(), mustBeValid);
        }

        [Test]
        public void IsValidForUpload_Should_Check_File_Length()
        {
            var uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(f => f.ContentLength).Returns(0);
            uploadedFile.Setup(f => f.FileName).Returns("test.csv");

            Assert.IsFalse(uploadedFile.Object.IsValidForUpload());
        }
    }
}