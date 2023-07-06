using YMA.DataAccess.Queries;

namespace YMA.Business.Services
{
    public class ProductService
    {
        private readonly ProductQuery _query;

        public ProductService(ProductQuery query)
        {
            _query = query;
        }

        public ProductQuery Query { get { return _query; } }
    }
}
