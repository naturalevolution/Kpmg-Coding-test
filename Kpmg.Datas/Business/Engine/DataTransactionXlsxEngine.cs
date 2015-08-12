using System.IO;
using Kpmg.Datas.Models.Files;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Kpmg.Datas.Business.Engine
{
    public class DataTransactionXlsxEngine : DataTransactionEngine
    {
        public DataTransactionXlsxEngine(string url) : base(url)
        {
        }

        public override DataTransactionResponse Run()
        {
            var dataTransactionResponse = new DataTransactionResponse();
            XSSFWorkbook xssfWorkbook;
            using (var file = new FileStream(Url, FileMode.Open, FileAccess.Read))
            {
                xssfWorkbook = new XSSFWorkbook(file);
            }

            var sheet = xssfWorkbook.GetSheetAt(0);
            var rows = sheet.GetRowEnumerator();
            rows.MoveNext();

            while (rows.MoveNext())
            {
                IRow row = (XSSFRow) rows.Current;
                var response = CheckAndInsertData(row);
                dataTransactionResponse.Messages.Add(response);
            }

            xssfWorkbook = null;
            sheet = null;
            return dataTransactionResponse;
        }

        protected DataTransactionMessage CheckAndInsertData(IRow row)
        {
            var result = new DataTransactionMessage(row.RowNum);
            var currentInformation = new Information();
            for (var i = 0; i < Columns.Length; i++)
            {
                var cell = row.GetCell(i);
                var currentCell = string.Empty;
                if (cell != null)
                {
                    currentCell = cell.ToString();
                }
                if (!IsValid(currentCell, i, result, currentInformation))
                {
                    break;
                }
            }
            InsertData(result, currentInformation);
            return result;
        }
    }
}