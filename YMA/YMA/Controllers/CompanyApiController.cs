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
        [Route("api/[controller]/GetCompanyList/{requestingCompanyId}/{searchText?}")]
        public ResponseModel GetCompanyList(string requestingCompanyId, string? searchText) => _companyService.Query.GetCompanyList(requestingCompanyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetContractedCompanyList/{requestingCompanyId}/{searchText?}")]
        public ResponseModel GetContractedCompanyList(string requestingCompanyId, string? searchText) => _companyService.Query.GetContractedCompanyList(requestingCompanyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetBuyingCompanyList/{requestingCompanyId}/{searchText?}")]
        public ResponseModel GetBuyingCompanyList(string requestingCompanyId, string? searchText) => _companyService.Query.GetBuyingCompanyList(requestingCompanyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetSellingCompanyList/{requestingCompanyId}/{searchText?}")]
        public ResponseModel GetSellingCompanyList(string requestingCompanyId, string? searchText) => _companyService.Query.GetSellingCompanyList(requestingCompanyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetNotContractedCompanyList/{requestingCompanyId}/{searchText?}")]
        public ResponseModel GetNotContractedCompanyList(string requestingCompanyId, string? searchText) => _companyService.Query.GetNotContractedCompanyList(requestingCompanyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetFeaturedCompanyList/{requestingCompanyId}/{length?}")]
        public ResponseModel GetFeaturedCompanyList(string requestingCompanyId, int? length) => _companyService.Query.GetFeaturedCompanyList(requestingCompanyId, length);
    }
}
