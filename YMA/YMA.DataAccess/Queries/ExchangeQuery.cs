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
        private readonly ResponseHelper _responseHelper;

        public ExchangeQuery(ymaContext db, ResponseHelper responseHelper)
        {
            _db = db;
            _responseHelper = responseHelper;
        }

        public ResponseModel GetExchangeList() => _responseHelper.TryCatch(
            "ExchangeQuery.GetExchangeList",
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
