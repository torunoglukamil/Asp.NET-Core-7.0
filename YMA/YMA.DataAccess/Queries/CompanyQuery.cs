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
        private readonly ResponseHelper _responseHelper;

        public CompanyQuery(ymaContext db, ResponseHelper responseHelper)
        {
            _db = db;
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

        public ResponseModel GetCompanyList() => _responseHelper.TryCatch(
            "CompanyQuery.GetCompanyList",
            () =>
            {
                List<CompanyModel> companyList = _db.companies.Where(x => x.is_disabled == false).OrderBy(x => x.name).Select(x => CompanyConverter.ToModel(x)).ToList();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyList,
                };
            }
          );

        public ResponseModel GetCompanyList(string searchText) => _responseHelper.TryCatch(
            "CompanyQuery.GetCompanyListBySearch",
            () =>
            {
                List<CompanyModel> companyList = _db.companies.Where(x => x.is_disabled == false).ToList().Where(x => SearchHelper.IsSearchedText(x.name, searchText)).OrderBy(x => x.name).Select(x => CompanyConverter.ToModel(x)).ToList();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = companyList,
                };
            }
          );

        public ResponseModel GetFeaturedCompanyList(int length) => _responseHelper.TryCatch(
            "CompanyQuery.GetFeaturedCompanyList",
            () =>
            {
                List<CompanyModel> companyList = new();
                _db.featured_companies.OrderByDescending(x => x.order_counter).ToList().ForEach(x =>
                {
                    company? company = _db.companies.Where(y => y.id == x.company_id).FirstOrDefault();
                    if (company != null)
                    {
                        companyList.Add(CompanyConverter.ToModel(company));
                    }
                });
                if (companyList.Count > length)
                {
                    companyList = companyList.GetRange(0, length);
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
