using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class FavoriteProductQuery
    {
        private readonly ymaContext _db;
        private readonly ResponseHelper _responseHelper;

        public FavoriteProductQuery(ymaContext db, ResponseHelper responseHelper)
        {
            _db = db;
            _responseHelper = responseHelper;
        }

        public ResponseModel GetFavoriteProductIdList(int accountId) => _responseHelper.TryCatch(
            "FavoriteProductQuery.GetFavoriteProductIdList",
            () =>
            {
                List<int> favoriteProductIdList = _db.favorite_products.Where(x => x.account_id == accountId).Select(x => x.product_id ?? 0).ToList();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = favoriteProductIdList,
                };
            }
          );
    }
}
