using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class VersionQuery
    {
        private readonly ymaContext _db;
        private readonly ResponseHelper _responseHelper;

        public VersionQuery(ymaContext db, ResponseHelper responseHelper)
        {
            _db = db;
            _responseHelper = responseHelper;
        }

        public ResponseModel GetVersion(string name) => _responseHelper.TryCatch(
            "VersionQuery.GetVersion",
            () =>
            {
                version? version = _db.versions.Where(x => x.name == name).FirstOrDefault();
                if (version?.version1 == null)
                {
                    return new ResponseModel()
                    {
                        message = "Versiyon bulunamadı."
                    };
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = VersionConverter.ToModel(version),
                };
            }
          );
    }
}
