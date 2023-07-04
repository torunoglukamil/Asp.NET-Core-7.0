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
        private readonly ExchangeConverter _exchangeConverter;

        public ExchangeQuery(ymaContext db, ResponseHelper responseHelper, ExchangeConverter exchangeConverter)
        {
            _db = db;
            _responseHelper = responseHelper;
            _exchangeConverter = exchangeConverter;
        }

        public ResponseModel GetExchangeList() => _responseHelper.TryCatch(
             () =>
             {
                 List<ExchangeModel> exchangeList = _db.exchanges.Where(x => x.is_disabled == false).OrderBy(x => x.order_number).Select(x => _exchangeConverter.ToModel(x)).ToList();
                 return new ResponseModel()
                 {
                     status_code = StatusCodes.Status200OK,
                     data = exchangeList,
                 };
             }
          );
    }
}
