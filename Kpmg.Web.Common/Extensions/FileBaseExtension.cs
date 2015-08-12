using System.IO;
using System.Linq;
using System.Web;

namespace Kpmg.Web.Common.Extensions
{
    public static class FileBaseExtension
    {
        private static readonly string[] ValidFileTypes = {".xlsx", ".csv"};

        public static bool IsValidForUpload(this HttpPostedFileBase sender)
        {
            var fileExtension = Path.GetExtension(sender.FileName);
            return ValidFileTypes.Any(x => x.Equals(fileExtension)) && sender.ContentLength > 0;
        }
    }
}