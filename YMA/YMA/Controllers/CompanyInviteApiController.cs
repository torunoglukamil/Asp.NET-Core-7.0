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

        [HttpPost]
        [Route("api/[controller]/CreateCompanyInvite")]
        public ResponseModel CreateCompanyInvite([FromBody] CompanyInviteModel companyInvite) => _companyInviteService.Repository.CreateCompanyInvite(companyInvite);

        [HttpPut]
        [Route("api/[controller]/ReplyCompanyInvite")]
        public ResponseModel ReplyCompanyInvite([FromBody] ReplyCompanyInviteModel replyCompanyInvite) => _companyInviteService.Repository.ReplyCompanyInvite(replyCompanyInvite);
    }
}
