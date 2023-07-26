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
        [Route("api/[controller]/GetAccountById/{accountId}")]
        public ResponseModel GetAccountById(string accountId) => _accountService.Query.GetAccountById(accountId);

        [HttpPut]
        [Route("api/[controller]/UpdateAccount")]
        public ResponseModel UpdateAccount([FromBody] AccountModel account) => _accountService.Repository.UpdateAccount(account);

        [HttpDelete]
        [Route("api/[controller]/DisableAccount/{accountId}")]
        public ResponseModel DisableAccount(string accountId) => _accountService.Repository.DisableAccount(accountId);

        [HttpPut]
        [Route("api/[controller]/ActivateAccount/{accountId}")]
        public ResponseModel ActivateAccount(string accountId) => _accountService.Repository.ActivateAccount(accountId);
    }
}
