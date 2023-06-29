using Microsoft.AspNetCore.Mvc;
using YMA.Business.Services;
using YMA.Models.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class AccountApiController
    {
        private readonly AccountService _service;

        public AccountApiController(AccountService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/[controller]/GetAccountById/{id}")]
        public ResponseModel Get(int id) => _service.Query.GetAccountById(id, true);

        [HttpPost]
        [Route("api/[controller]/CreateAccount")]
        public async Task<ResponseModel> Post([FromBody] AccountModel account) => await _service.Repository.CreateAccount(account);

        [HttpPut]
        [Route("api/[controller]/UpdateAccount")]
        public async Task<ResponseModel> Put([FromBody] AccountModel account) => await _service.Repository.UpdateAccount(account);

        [HttpDelete]
        [Route("api/[controller]/DisableAccountById/{id}")]
        public ResponseModel Delete(int id) => _service.Repository.DisableAccountById(id);
    }
}
