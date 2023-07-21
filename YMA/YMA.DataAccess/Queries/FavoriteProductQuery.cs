using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Converters;
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

        public ResponseModel GetFavoriteProductList() => _responseHelper.TryCatch(
            "FavoriteProductQuery.GetFavoriteProductList",
            () =>
            {
                List<AdModel> adList = _db.ads.Where(x => x.is_disabled == false).OrderBy(x => x.order_number).Select(x => AdConverter.ToModel(x)).ToList();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = adList,
                };
            }
          );
    }
}
