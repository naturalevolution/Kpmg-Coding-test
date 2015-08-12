using System.Collections.Generic;

namespace Kpmg.Datas.Business
{
    public class DataTransactionResponse
    {
        public DataTransactionResponse()
        {
            Messages = new List<DataTransactionMessage>();
        }

        public IList<DataTransactionMessage> Messages { get; set; }
    }
}