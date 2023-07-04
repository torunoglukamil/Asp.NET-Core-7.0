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

        public AdQuery(ymaContext db)
        {
            _db = db;
        }

        public ResponseModel GetAdList() => ResponseHelper.TryCatch(
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
