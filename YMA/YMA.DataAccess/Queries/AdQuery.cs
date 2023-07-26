using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class AdQuery
    {
        private readonly ymaContext _db;
        private readonly ResponseHelper _responseHelper;

        public AdQuery(ymaContext db, ResponseHelper responseHelper)
        {
            _db = db;
            _responseHelper = responseHelper;
        }

        public ResponseModel GetAdList() => _responseHelper.TryCatch(
            "AdQuery.GetAdList",
            () =>
            {
                List<AdModel> adList = _db.ads.Where(x => x.is_disabled == false).OrderBy(x => x.order_number).Select(AdConverter.ToModel).ToList();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = adList,
                };
            }
          );
    }
}
