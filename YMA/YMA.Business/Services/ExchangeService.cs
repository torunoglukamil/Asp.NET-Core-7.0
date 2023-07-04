using YMA.DataAccess.Queries;

namespace YMA.Business.Services
{
    public class ExchangeService
    {
        private readonly ExchangeQuery _query;

        public ExchangeService(ExchangeQuery query)
        {
            _query = query;
        }

        public ExchangeQuery Query { get { return _query; } }
    }
}
