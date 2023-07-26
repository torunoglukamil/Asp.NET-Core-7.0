using Microsoft.AspNetCore.Mvc;
using YMA.Business.Services;
using YMA.Entities.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class CompanyInviteApiController
    {
        private readonly CompanyInviteService _companyInviteService;

        public CompanyInviteApiController(CompanyInviteService companyInviteService)
        {
            _companyInviteService = companyInviteService;
        }

        [HttpGet]
        [Route("api/[controller]/GetIncomingCompanyInviteList/{companyId}")]
        public ResponseModel GetIncomingCompanyInviteList(string companyId) => _companyInviteService.Query.GetIncomingCompanyInviteList(companyId);

        [HttpGet]
        [Route("api/[controller]/GetSentCompanyInviteList/{companyId}")]
        public ResponseModel GetSentCompanyInviteList(string companyId) => _companyInviteService.Query.GetSentCompanyInviteList(companyId);

        [HttpGet]
        [Route("api/[controller]/GetIncomingCompanyInviteCount/{companyId}")]
        public ResponseModel GetIncomingCompanyInviteCount(string companyId) => _companyInviteService.Query.GetIncomingCompanyInviteCount(companyId);

        [HttpPost]
        [Route("api/[controller]/CreateCompanyInvite")]
        public ResponseModel CreateCompanyInvite([FromBody] CompanyInviteModel companyInvite) => _companyInviteService.Repository.CreateCompanyInvite(companyInvite);

        [HttpPut]
        [Route("api/[controller]/AcceptCompanyInviteById/{companyInviteId}")]
        public ResponseModel AcceptCompanyInviteById(string companyInviteId) => _companyInviteService.Repository.AcceptCompanyInviteById(companyInviteId);

        [HttpPut]
        [Route("api/[controller]/RejectCompanyInviteById/{companyInviteId}")]
        public ResponseModel RejectCompanyInviteById(string companyInviteId) => _companyInviteService.Repository.RejectCompanyInviteById(companyInviteId);

        [HttpPut]
        [Route("api/[controller]/CancelCompanyInviteById/{companyInviteId}")]
        public ResponseModel CancelCompanyInviteById(string companyInviteId) => _companyInviteService.Repository.CancelCompanyInviteById(companyInviteId);
    }
}
