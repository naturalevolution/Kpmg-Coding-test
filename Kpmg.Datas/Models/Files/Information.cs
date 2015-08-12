namespace Kpmg.Datas.Models.Files
{
    public class Information : BaseEntity
    {
        public string Account { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
    }
}