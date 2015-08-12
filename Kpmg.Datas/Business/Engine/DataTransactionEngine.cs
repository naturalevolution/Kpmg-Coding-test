using System.Globalization;
using System.Linq;
using Kpmg.Datas.Factories;
using Kpmg.Datas.Models.Files;

namespace Kpmg.Datas.Business.Engine
{
    public abstract class DataTransactionEngine
    {
        protected readonly string[] Columns;
        protected readonly string Url;

        protected DataTransactionEngine(string url)
        {
            Url = url;
            Columns = new[]
            {
                "Account",
                "Description",
                "Currency",
                "Amount"
            };
        }

        public abstract DataTransactionResponse Run();

        protected void InsertData(DataTransactionMessage result, Information currentInformation)
        {
            if (result.State == DataTransactionState.Valid)
            {
                Repositories.Informations.Insert(currentInformation);
            }
        }

        protected bool IsValid(string cell, int index, DataTransactionMessage result, Information info)
        {
            if (string.IsNullOrWhiteSpace(cell))
            {
                result.AddError(DataCellErrorType.FieldRequired, Columns[index]);
            }
            else
            {
                if (index == 0)
                {
                    info.Account = cell;
                }
                if (index == 1)
                {
                    info.Description = cell;
                }
                if (index == 2)
                {
                    var region = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                        .Select(c => new RegionInfo(c.LCID))
                        .FirstOrDefault(ri => ri != null && ri.ISOCurrencySymbol == cell.ToString());
                    if (region == null)
                    {
                        result.AddError(DataCellErrorType.FieldNotCurrency, Columns[index]);
                    }
                    else
                    {
                        info.CurrencyCode = cell;
                    }
                }
                if (index == 3)
                {
                    decimal amount;
                    if (!decimal.TryParse(cell,  NumberStyles.Currency, CultureInfo.InvariantCulture, out amount))
                    {
                        result.AddError(DataCellErrorType.FieldMustBeDecimal, Columns[index]);
                    }
                    else
                    {
                        info.Amount = amount;
                    }
                }
            }
            return result.State == DataTransactionState.Valid;
        }
    }
}