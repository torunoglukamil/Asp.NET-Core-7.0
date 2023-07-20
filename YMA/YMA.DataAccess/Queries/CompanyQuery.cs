using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class CompanyQuery
    {
        private readonly ymaContext _db;
        private readonly CompanyInviteQuery _companyInviteQuery;
        private readonly ResponseHelper _responseHelper;

        public CompanyQuery(ymaContext db, CompanyInviteQuery companyInviteQuery, ResponseHelper responseHelper)
        {
            _db = db;
            _companyInviteQuery = companyInviteQuery;
            _responseHelper = responseHelper;
        }

        public ResponseModel GetCompanyById(int id) => _responseHelper.TryCatch(
            "CompanyQuery.GetCompanyById",
            () =>
            {
                company? company = _db.companies.Where(x => x.id == id).FirstOrDefault();
                if (company == null)
                {
                    return new ResponseModel()
                    {
                        message = "Firma bulunamadı.",
                    };
                }
                if (company.is_disabled ?? false)
                {
                    return new ResponseModel()
                    {
                        message = "Firma devre dışı.",
                    };
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = CompanyConverter.ToModel(company),
                };
            }
          );

        public ResponseModel GetCompanyList(int companyId, string? searchText) => _responseHelper.TryCatch(
            "CompanyQuery.GetCompanyList",
            () =>
            {
                List<CompanyModel> companyList = _db.companies.Where(x => x.is_disabled == false).Where(x => x.id != companyId).OrderBy(x => x.name).Select(x => CompanyConverter.ToModel(x)).ToList();
                companyList = CompanyHelper.GetCompanyListBySearch(companyList, searchText);
                companyList.ForEach(x =>
                {
                    ResponseModel response = _companyInviteQuery.GetCompanyInviteList(x.id, companyId);
                    x.company_invite_list = response.data;
                });
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyList,
                };
            }
          );

        public ResponseModel GetContractedCompanyList(int companyId, string? searchText) => _responseHelper.TryCatch(
            "CompanyQuery.GetContractedCompanyList",
            () =>
            {
                ResponseModel response = GetCompanyList(companyId, searchText);
                List<CompanyModel> companyList = new();
                ((List<CompanyModel>)(response.data!)).ForEach(x =>
                {
                    foreach (CompanyInviteModel companyInvite in x.company_invite_list!)
                    {
                        if (companyInvite.is_accepted == true)
                        {
                            companyList.Add(x);
                            break;
                        }
                    }
                });
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyList,
                };
            }
          );

        public ResponseModel GetBuyingCompanyList(int companyId, string? searchText) => _responseHelper.TryCatch(
            "CompanyQuery.GetBuyingCompanyList",
            () =>
            {
                ResponseModel response = GetContractedCompanyList(companyId, searchText);
                List<CompanyModel> companyList = new();
                ((List<CompanyModel>)(response.data!)).ForEach(x =>
                {
                    foreach (CompanyInviteModel companyInvite in x.company_invite_list!)
                    {
                        if ((companyInvite.receiver_id == companyId) && (companyInvite.is_selling ?? false))
                        {
                            companyList.Add(x);
                        }
                        else if ((companyInvite.sender_id == companyId) && (companyInvite.is_buying ?? false))
                        {
                            companyList.Add(x);
                        }
                    }
                });
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyList,
                };
            }
          );

        public ResponseModel GetSellingCompanyList(int companyId, string? searchText) => _responseHelper.TryCatch(
            "CompanyQuery.GetSellingCompanyList",
            () =>
            {
                ResponseModel response = GetContractedCompanyList(companyId, searchText);
                List<CompanyModel> companyList = new();
                ((List<CompanyModel>)(response.data!)).ForEach(x =>
                {
                    foreach (CompanyInviteModel companyInvite in x.company_invite_list!)
                    {
                        if ((companyInvite.receiver_id == companyId) && (companyInvite.is_buying ?? false))
                        {
                            companyList.Add(x);
                        }
                        else if ((companyInvite.sender_id == companyId) && (companyInvite.is_selling ?? false))
                        {
                            companyList.Add(x);
                        }
                    }
                });
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyList,
                };
            }
          );

        public ResponseModel GetNotContractedCompanyList(int companyId, string? searchText) => _responseHelper.TryCatch(
            "CompanyQuery.GetNotContractedCompanyList",
            () =>
            {
                ResponseModel response = GetCompanyList(companyId, searchText);
                List<CompanyModel> companyList = new();
                ((List<CompanyModel>)(response.data!)).ForEach(x =>
                {
                    bool isContracted = false;
                    foreach (CompanyInviteModel companyInvite in x.company_invite_list!)
                    {
                        if (companyInvite.is_accepted == true)
                        {
                            isContracted = true;
                            break;
                        }
                    }
                    if (!isContracted)
                    {
                        companyList.Add(x);
                    }
                });
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyList,
                };
            }
          );

        public ResponseModel GetFeaturedCompanyList(int companyId, int? length) => _responseHelper.TryCatch(
            "CompanyQuery.GetFeaturedCompanyList",
            () =>
            {
                List<CompanyModel> companyList = new();
                _db.featured_companies.OrderByDescending(x => x.order_counter).ToList().ForEach(x =>
                {
                    company? company = _db.companies.Where(y => y.id == x.company_id).Where(x => x.is_disabled == false).FirstOrDefault();
                    if (company != null && company.id != companyId)
                    {
                        companyList.Add(CompanyConverter.ToModel(company));
                    }
                });
                if ((length ?? 0) != 0 && companyList.Count > length)
                {
                    companyList = companyList.GetRange(0, length ?? 1);
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyList,
                };
            }
          );
    }
}
