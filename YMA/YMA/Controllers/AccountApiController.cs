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

        [HttpPut]
        [Route("api/[controller]/UpdateAccount")]
        public async Task<ResponseModel> Put([FromBody] AccountModel account) => await _service.Repository.UpdateAccount(account);

        [HttpDelete]
        [Route("api/[controller]/DisableAccount/{id}")]
        public ResponseModel Delete(int id) => _service.Repository.DisableAccount(id);

        [HttpPut]
        [Route("api/[controller]/ActivateAccount/{id}")]
        public ResponseModel Put(int id) => _service.Repository.ActivateAccount(id);
    }
}
