using YMA.DataAccess.Helpers;
using YMA.DataAccess.Queries;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Repositories
{
    public class FavoriteProductRepository
    {
        private readonly ymaContext _db;
        private readonly FavoriteProductQuery _favoriteProductQuery;
        private readonly ResponseHelper _responseHelper;

        public FavoriteProductRepository(ymaContext db, FavoriteProductQuery favoriteProductQuery, ResponseHelper responseHelper)
        {
            _db = db;
            _favoriteProductQuery = favoriteProductQuery;
            _responseHelper = responseHelper;
        }

        public ResponseModel UpdateFavoriteProduct(int productId, int accountId) => _responseHelper.TryCatch(
            "FavoriteProductRepository.UpdateFavoriteProduct",
            () =>
            {
                List<favorite_product> favoriteProductList = _db.favorite_products.Where(x => x.product_id == productId).Where(x => x.account_id == accountId).ToList();
                if (favoriteProductList.Any())
                {
                    favoriteProductList.ForEach(x => _db.favorite_products.Remove(x));
                }
                else
                {
                    _db.favorite_products.Add(new favorite_product()
                    {
                        product_id = productId,
                        account_id = accountId,
                        create_date = DateTime.Now,
                    });
                }
                return _favoriteProductQuery.GetFavoriteProductIdList(accountId);
            }
        );
    }
}
