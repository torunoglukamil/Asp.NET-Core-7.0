using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class ExchangeConverter
    {
        public ExchangeModel ToModel(exchange exchange) => new ExchangeModel()
        {
            currency = exchange.currency,
            unit_price = exchange.unit_price,
            exchange_ratio = exchange.exchange_ratio,
            exchange_amount = exchange.exchange_amount,
            is_increased = exchange.is_increased,
        };
    }
}
