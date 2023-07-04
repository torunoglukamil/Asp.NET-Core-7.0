using Microsoft.AspNetCore.Mvc;
using YMA.Business.Services;
using YMA.Entities.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class AccountApiController
    {
        private readonly AccountService _accountService;

        public AccountApiController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAccountById/{id}")]
        public ResponseModel GetAccountById(int id) => _accountService.Query.GetAccountById(id, true);

        [HttpPut]
        [Route("api/[controller]/UpdateAccount")]
        public async Task<ResponseModel> UpdateAccount([FromBody] AccountModel account) => await _accountService.Repository.UpdateAccount(account);

        [HttpDelete]
        [Route("api/[controller]/DisableAccount/{id}")]
        public ResponseModel DisableAccount(int id) => _accountService.Repository.DisableAccount(id);

        [HttpPut]
        [Route("api/[controller]/ActivateAccount/{id}")]
        public ResponseModel ActivateAccount(int id) => _accountService.Repository.ActivateAccount(id);
    }
}
