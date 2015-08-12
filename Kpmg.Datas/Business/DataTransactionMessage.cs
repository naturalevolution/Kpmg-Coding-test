using System.Collections.Generic;
using Kpmg.Datas.Business.Engine;
using Kpmg.Messages;

namespace Kpmg.Datas.Business
{
    public class DataTransactionMessage
    {
        public DataTransactionMessage(int idLine)
        {
            Messages = new List<string>();
            IdLine = idLine;
        }

        public int IdLine { get; set; }
        public DataTransactionState State { get; set; }
        public IList<string> Messages { get; set; }

        public void AddError(DataCellErrorType errorType, string columnName)
        {
            string result = null;
            switch (errorType)
            {
                case DataCellErrorType.FieldRequired:
                    result = string.Format(InformationMessage.Error_Field_Required, columnName);
                    break;
                case DataCellErrorType.FieldNotCurrency:
                    result = string.Format(InformationMessage.Error_Field_Not_Currency, columnName);
                    break;
                case DataCellErrorType.FieldMustBeDecimal:
                    result = string.Format(InformationMessage.Error_Field_Must_Be_Decimal, columnName);
                    break;
            }
            Messages.Add(result);
            State = DataTransactionState.Ignored;
        }
    }

    public enum DataTransactionState
    {
        Valid,
        Ignored
    }
}