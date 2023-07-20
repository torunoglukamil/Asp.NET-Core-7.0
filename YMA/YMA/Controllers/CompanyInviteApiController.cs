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
        public async Task<ResponseModel> CreateCompanyInvite([FromBody] CompanyInviteModel companyInvite) => await _companyInviteService.Repository.CreateCompanyInvite(companyInvite);

        [HttpPut]
        [Route("api/[controller]/ReplyCompanyInvite")]
        public async Task<ResponseModel> ReplyCompanyInvite([FromBody] ReplyCompanyInviteModel replyCompanyInvite) => await _companyInviteService.Repository.ReplyCompanyInvite(replyCompanyInvite);
    }
}
