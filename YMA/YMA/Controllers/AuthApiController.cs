using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YMA.Business.Services;
using YMA.Entities.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class AuthApiController
    {
        private readonly AuthService _service;

        public AuthApiController(AuthService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("api/[controller]/CreateAccount")]
        public async Task<ResponseModel> Post([FromBody] dynamic data) => await _service.CreateAccount(JsonConvert.DeserializeObject<CreateAccountModel>(data.ToString()), JsonConvert.DeserializeObject<AccountModel>(data.ToString()));

        [HttpGet]
        [Route("api/[controller]/SignInAccount")]
        public async Task<ResponseModel> Get([FromBody] SignInAccountModel signInAccount) => await _service.SignInAccount(signInAccount);

        [HttpPost]
        [Route("api/[controller]/SendPasswordResetEmail")]
        public async Task<ResponseModel> Post([FromBody] EmailModel email) => await _service.SendPasswordResetEmail(email);
    }
}
