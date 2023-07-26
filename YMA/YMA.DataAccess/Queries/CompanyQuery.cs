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

        public ResponseModel GetCompanyById(string companyId, string requestingCompanyId) => _responseHelper.TryCatch(
            "CompanyQuery.GetCompanyById",
            () =>
            {
                company? company = _db.companies.Where(x => x.id == companyId).FirstOrDefault();
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
                CompanyModel companyModel = CompanyConverter.ToModel(company);
                ResponseModel response = _companyInviteQuery.GetCompanyInviteList(companyId, requestingCompanyId);
                companyModel.company_invite_list = response.data;
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyModel,
                };
            }
          );

        public ResponseModel GetCompanyList(string requestingCompanyId, string? searchText) => _responseHelper.TryCatch(
            "CompanyQuery.GetCompanyList",
            () =>
            {
                List<CompanyModel> companyList = new();
                _db.companies.Where(x => x.id != requestingCompanyId).OrderBy(x => x.name).ToList().ForEach(x =>
                {
                    ResponseModel response = GetCompanyById(x.id, requestingCompanyId);
                    if (response.status_code == StatusCodes.Status200OK)
                    {
                        companyList.Add(response.data);
                    }
                });
                companyList = CompanyHelper.GetCompanyListBySearch(companyList, searchText);
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyList,
                };
            }
          );

        public ResponseModel GetContractedCompanyList(string requestingCompanyId, string? searchText) => _responseHelper.TryCatch(
            "CompanyQuery.GetContractedCompanyList",
            () =>
            {
                ResponseModel response = GetCompanyList(requestingCompanyId, searchText);
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

        public ResponseModel GetBuyingCompanyList(string requestingCompanyId, string? searchText) => _responseHelper.TryCatch(
            "CompanyQuery.GetBuyingCompanyList",
            () =>
            {
                ResponseModel response = GetContractedCompanyList(requestingCompanyId, searchText);
                List<CompanyModel> companyList = new();
                ((List<CompanyModel>)(response.data!)).ForEach(x =>
                {
                    x.company_invite_list!.ForEach(y =>
                    {
                        if ((y.receiver_id == requestingCompanyId) && (y.is_selling ?? false))
                        {
                            companyList.Add(x);
                        }
                        else if ((y.sender_id == requestingCompanyId) && (y.is_buying ?? false))
                        {
                            companyList.Add(x);
                        }
                    });
                });
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyList,
                };
            }
          );

        public ResponseModel GetSellingCompanyList(string requestingCompanyId, string? searchText) => _responseHelper.TryCatch(
            "CompanyQuery.GetSellingCompanyList",
            () =>
            {
                ResponseModel response = GetContractedCompanyList(requestingCompanyId, searchText);
                List<CompanyModel> companyList = new();
                ((List<CompanyModel>)(response.data!)).ForEach(x =>
                {
                    x.company_invite_list!.ForEach(y =>
                    {
                        if ((y.receiver_id == requestingCompanyId) && (y.is_buying ?? false))
                        {
                            companyList.Add(x);
                        }
                        else if ((y.sender_id == requestingCompanyId) && (y.is_selling ?? false))
                        {
                            companyList.Add(x);
                        }
                    });
                });
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyList,
                };
            }
          );

        public ResponseModel GetNotContractedCompanyList(string requestingCompanyId, string? searchText) => _responseHelper.TryCatch(
            "CompanyQuery.GetNotContractedCompanyList",
            () =>
            {
                ResponseModel response = GetCompanyList(requestingCompanyId, searchText);
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

        public ResponseModel GetFeaturedCompanyList(string requestingCompanyId, int? length) => _responseHelper.TryCatch(
            "CompanyQuery.GetFeaturedCompanyList",
            () =>
            {
                List<CompanyModel> companyList = new();
                _db.featured_companies.Where(x => x.company_id != requestingCompanyId).OrderByDescending(x => x.order_counter).ToList().ForEach(x =>
                {
                    ResponseModel response = GetCompanyById(x.id, requestingCompanyId);
                    if (response.status_code == StatusCodes.Status200OK)
                    {
                        companyList.Add(response.data);
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
