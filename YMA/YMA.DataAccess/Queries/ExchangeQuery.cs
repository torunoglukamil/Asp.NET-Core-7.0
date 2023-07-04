using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class ExchangeQuery
    {
        private readonly ymaContext _db;

        public ExchangeQuery(ymaContext db)
        {
            _db = db;
        }

        public ResponseModel GetExchangeList() => ResponseHelper.TryCatch(
             () =>
             {
                 List<ExchangeModel> exchangeList = _db.exchanges.Where(x => x.is_disabled == false).OrderBy(x => x.order_number).Select(x => ExchangeConverter.ToModel(x)).ToList();
                 return new ResponseModel()
                 {
                     status_code = StatusCodes.Status200OK,
                     data = exchangeList,
                 };
             }
          );
    }
}
