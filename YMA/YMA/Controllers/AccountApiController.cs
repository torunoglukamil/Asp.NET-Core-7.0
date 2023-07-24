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
        public ResponseModel GetAccountById(string id) => _accountService.Query.GetAccountById(id);

        [HttpPut]
        [Route("api/[controller]/UpdateAccount")]
        public ResponseModel UpdateAccount([FromBody] AccountModel account) => _accountService.Repository.UpdateAccount(account);

        [HttpDelete]
        [Route("api/[controller]/DisableAccount/{id}")]
        public ResponseModel DisableAccount(string id) => _accountService.Repository.DisableAccount(id);

        [HttpPut]
        [Route("api/[controller]/ActivateAccount/{id}")]
        public ResponseModel ActivateAccount(string id) => _accountService.Repository.ActivateAccount(id);
    }
}
