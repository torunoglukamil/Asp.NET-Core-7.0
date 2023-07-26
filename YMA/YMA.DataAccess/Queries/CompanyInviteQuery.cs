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

        public ResponseModel GetCompanyInviteById(string companyInviteId) => _responseHelper.TryCatch(
            "CompanyInviteQuery.GetCompanyInviteById",
            () =>
            {
                company_invite? companyInvite = _db.company_invites.Where(x => x.id == companyInviteId).FirstOrDefault();
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

        public ResponseModel GetCompanyInviteList(string companyId1, string companyId2) => _responseHelper.TryCatch(
            "CompanyInviteQuery.GetCompanyInviteList",
            () =>
            {
                List<CompanyInviteModel> companyInviteList = new();
                _db.company_invites.Where(x => x.receiver_id == companyId1 && x.sender_id == companyId2 && x.is_accepted != false).ToList().ForEach(x =>
                {
                    companyInviteList.Add(CompanyInviteConverter.ToModel(x));
                });
                _db.company_invites.Where(x => x.receiver_id == companyId2 && x.sender_id == companyId1 && x.is_accepted != false).ToList().ForEach(x =>
                {
                    companyInviteList.Add(CompanyInviteConverter.ToModel(x));
                });
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyInviteList,
                };
            }
          );

        private CompanyModel? GetCompanyById(string companyId, string requestingCompanyId)
        {
            company? company = _db.companies.Where(x => x.id == companyId).FirstOrDefault();
            if (company == null)
            {
                return null;
            }
            if (company.is_disabled ?? false)
            {
                return null;
            }
            CompanyModel companyModel = CompanyConverter.ToModel(company);
            ResponseModel response = GetCompanyInviteList(companyId, requestingCompanyId);
            companyModel.company_invite_list = response.data;
            return companyModel;
        }

        public ResponseModel GetIncomingCompanyInviteList(string companyId) => _responseHelper.TryCatch(
            "CompanyInviteQuery.GetIncomingCompanyInviteList",
            () =>
            {
                List<CompanyInviteModel> companyInviteList = new();
                _db.company_invites.Where(x => x.receiver_id == companyId && x.is_accepted == null).OrderByDescending(x => x.create_date).Select(CompanyInviteConverter.ToModel).ToList().ForEach(x =>
                {
                    CompanyModel? company = GetCompanyById(x.sender_id!, x.receiver_id!);
                    if (company != null)
                    {
                        x.company = company;
                        companyInviteList.Add(x);
                    }
                });
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyInviteList,
                };
            }
          );

        public ResponseModel GetSentCompanyInviteList(string companyId) => _responseHelper.TryCatch(
            "CompanyInviteQuery.GetSentCompanyInviteList",
            () =>
            {
                List<CompanyInviteModel> companyInviteList = new();
                _db.company_invites.Where(x => x.sender_id == companyId && x.is_accepted == null).OrderByDescending(x => x.create_date).Select(CompanyInviteConverter.ToModel).ToList().ForEach(x =>
                {
                    CompanyModel? company = GetCompanyById(x.receiver_id!, x.sender_id!);
                    if (company != null)
                    {
                        x.company = company;
                        companyInviteList.Add(x);
                    }
                });
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyInviteList,
                };
            }
          );

        public ResponseModel GetIncomingCompanyInviteCount(string companyId) => _responseHelper.TryCatch(
            "CompanyInviteQuery.GetIncomingCompanyInviteCount",
            () =>
            {
                ResponseModel response = GetIncomingCompanyInviteList(companyId);
                List<CompanyInviteModel> incomingCompanyInviteList = response.data!;
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = incomingCompanyInviteList.Count,
                };
            }
          );
    }
}
