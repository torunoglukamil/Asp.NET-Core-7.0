using Microsoft.AspNetCore.Mvc;
using YMA.Business.Services;
using YMA.Models.Models;

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

        [HttpGet]
        [Route("api/[controller]/SignInWithEmailAndPassword")]
        public async Task<ResponseModel> Get([FromBody] AuthModel auth) => await _service.SignInWithEmailAndPassword(auth);

        [HttpPost]
        [Route("api/[controller]/CreateAccountWithEmailAndPassword")]
        public async Task<ResponseModel> Post([FromBody] AuthModel auth) => await _service.CreateAccountWithEmailAndPassword(auth);
    }
}
