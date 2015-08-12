using Kpmg.Datas.Business.Engine;

namespace Kpmg.Datas.Business
{
    internal enum FileType
    {
        None,
        Xlsx,
        Csv
    }

    public class DataTransaction
    {
        public DataTransactionResponse Execute(string url)
        {
            DataTransactionEngine transactionEngine = null;
            switch (GetFileType(url))
            {
                case FileType.Xlsx:
                    transactionEngine = new DataTransactionXlsxEngine(url);
                    break;
                case FileType.Csv:
                    transactionEngine = new DataTransactionCsvEngine(url);
                    break;
            }

            return transactionEngine.Run();
        }

        private FileType GetFileType(string url)
        {
            if (url.EndsWith(".xlsx"))
            {
                return FileType.Xlsx;
            }
            if (url.EndsWith(".csv"))
            {
                return FileType.Csv;
            }
            return FileType.None;
        }
    }
}