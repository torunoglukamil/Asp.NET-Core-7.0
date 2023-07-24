using Microsoft.AspNetCore.Mvc;
using YMA.Business.Services;
using YMA.Entities.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class CompanyApiController
    {
        private readonly CompanyService _companyService;

        public CompanyApiController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        [Route("api/[controller]/GetCompanyList/{companyId}/{searchText?}")]
        public ResponseModel GetCompanyList(string companyId, string? searchText) => _companyService.Query.GetCompanyList(companyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetContractedCompanyList/{companyId}/{searchText?}")]
        public ResponseModel GetContractedCompanyList(string companyId, string? searchText) => _companyService.Query.GetContractedCompanyList(companyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetBuyingCompanyList/{companyId}/{searchText?}")]
        public ResponseModel GetBuyingCompanyList(string companyId, string? searchText) => _companyService.Query.GetBuyingCompanyList(companyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetSellingCompanyList/{companyId}/{searchText?}")]
        public ResponseModel GetSellingCompanyList(string companyId, string? searchText) => _companyService.Query.GetSellingCompanyList(companyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetNotContractedCompanyList/{companyId}/{searchText?}")]
        public ResponseModel GetNotContractedCompanyList(string companyId, string? searchText) => _companyService.Query.GetNotContractedCompanyList(companyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetFeaturedCompanyList/{companyId}/{length?}")]
        public ResponseModel GetFeaturedCompanyList(string companyId, int? length) => _companyService.Query.GetFeaturedCompanyList(companyId, length);
    }
}
