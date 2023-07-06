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
        [Route("api/[controller]/GetCompanyList")]
        public ResponseModel GetCompanyList() => _companyService.Query.GetCompanyList();

        [HttpGet]
        [Route("api/[controller]/GetCompanyList/{searchText}")]
        public ResponseModel GetCompanyList(string searchText) => _companyService.Query.GetCompanyList(searchText);

        [HttpGet]
        [Route("api/[controller]/GetFeaturedCompanyList/{length}")]
        public ResponseModel GetFeaturedCompanyList(int length) => _companyService.Query.GetFeaturedCompanyList(length);
    }
}
