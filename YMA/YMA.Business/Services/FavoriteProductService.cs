using YMA.DataAccess.Queries;
using YMA.DataAccess.Repositories;

namespace YMA.Business.Services
{
    public class FavoriteProductService
    {
        private readonly FavoriteProductQuery _query;
        private readonly FavoriteProductRepository _repository;

        public FavoriteProductService(FavoriteProductQuery query, FavoriteProductRepository repository)
        {
            _query = query;
            _repository = repository;
        }

        public FavoriteProductQuery Query { get { return _query; } }

        public FavoriteProductRepository Repository { get { return _repository; } }
    }
}
