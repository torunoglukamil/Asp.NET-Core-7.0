namespace YMA.Entities.Models
{
    public class ExchangeModel
    {
        public string? currency { get; set; }

        public double? unit_price { get; set; }

        public double? exchange_ratio { get; set; }

        public double? exchange_amount { get; set; }

        public bool? is_increased { get; set; }
    }
}
