using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class CompanyInviteQuery
    {
        private readonly ymaContext _db;
        private readonly ResponseHelper _responseHelper;

        public CompanyInviteQuery(ymaContext db, ResponseHelper responseHelper)
        {
            _db = db;
            _responseHelper = responseHelper;
        }

        public ResponseModel GetCompanyInviteById(int id) => _responseHelper.TryCatch(
            "CompanyInviteQuery.GetCompanyInviteById",
            () =>
            {
                company_invite? companyInvite = _db.company_invites.Where(x => x.id == id).FirstOrDefault();
                if (companyInvite == null)
                {
                    return new ResponseModel()
                    {
                        message = "Firma daveti bulunamadı.",
                    };
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyInvite,
                };
            }
          );

        public ResponseModel GetCompanyInviteList(int companyId) => _responseHelper.TryCatch(
            "CompanyInviteQuery.GetCompanyInviteList",
            () =>
            {
                List<CompanyInviteModel> companyInviteList = new();
                _db.company_invites.ToList().ForEach(x =>
                {
                    if ((x.receiver_id == companyId || x.sender_id == companyId) && x.is_accepted != false)
                    {
                        companyInviteList.Add(CompanyInviteConverter.ToModel(x));
                    }
                });
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyInviteList,
                };
            }
          );
    }
}
