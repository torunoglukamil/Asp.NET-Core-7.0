using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YMA.Business.Services;
using YMA.Entities.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class AuthApiController
    {
        private readonly AuthService _authService;

        public AuthApiController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("api/[controller]/CreateAccount")]
        public async Task<ResponseModel> Post([FromBody] dynamic data) => await _authService.CreateAccount(JsonConvert.DeserializeObject<CreateAccountModel>(data.ToString()), JsonConvert.DeserializeObject<AccountModel>(data.ToString()));

        [HttpGet]
        [Route("api/[controller]/SignInAccount")]
        public async Task<ResponseModel> Get([FromBody] SignInAccountModel signInAccount) => await _authService.SignInAccount(signInAccount);

        [HttpPost]
        [Route("api/[controller]/SendPasswordResetEmail")]
        public async Task<ResponseModel> Post([FromBody] EmailModel email) => await _authService.SendPasswordResetEmail(email);
    }
}
