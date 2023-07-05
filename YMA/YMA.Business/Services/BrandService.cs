using YMA.DataAccess.Queries;

namespace YMA.Business.Services
{
    public class BrandService
    {
        private readonly BrandQuery _query;

        public BrandService(BrandQuery query)
        {
            _query = query;
        }

        public BrandQuery Query { get { return _query; } }
    }
}
