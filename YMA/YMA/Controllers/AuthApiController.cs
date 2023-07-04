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
        public async Task<ResponseModel> CreateAccount([FromBody] dynamic data) => await _authService.Repository.CreateAccount(JsonConvert.DeserializeObject<CreateAccountModel>(data.ToString()), JsonConvert.DeserializeObject<AccountModel>(data.ToString()));

        [HttpGet]
        [Route("api/[controller]/SignInAccount")]
        public async Task<ResponseModel> SignInAccount([FromBody] SignInAccountModel signInAccount) => await _authService.Query.SignInAccount(signInAccount);

        [HttpPost]
        [Route("api/[controller]/SendPasswordResetEmail")]
        public async Task<ResponseModel> SendPasswordResetEmail([FromBody] EmailModel email) => await _authService.Query.SendPasswordResetEmail(email);
    }
}
