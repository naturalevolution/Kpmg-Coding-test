using System.IO;
using Kpmg.Datas.Models.Files;

namespace Kpmg.Datas.Business.Engine
{
    public class DataTransactionCsvEngine : DataTransactionEngine
    {
        public DataTransactionCsvEngine(string url) : base(url)
        {
        }

        public override DataTransactionResponse Run()
        {
            var dataTransactionResponse = new DataTransactionResponse();
            using (var reader = new StreamReader(Url))
            {
                var counter = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (counter > 0 && line != null)
                    {
                        var response = CheckAndInsertData(line.Split(';'), counter);
                        dataTransactionResponse.Messages.Add(response);
                    }
                    counter++;
                }
            }
            return dataTransactionResponse;
        }

        protected DataTransactionMessage CheckAndInsertData(string[] row, int lineId)
        {
            var result = new DataTransactionMessage(lineId);
            var currentInformation = new Information();
            for (var i = 0; i < Columns.Length; i++)
            {
                if (!IsValid(row[i], i, result, currentInformation))
                {
                    break;
                }
            }
            InsertData(result, currentInformation);
            return result;
        }
    }
}