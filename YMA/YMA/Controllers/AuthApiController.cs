using Microsoft.AspNetCore.Mvc;
using YMA.Business.Interfaces;
using YMA.Models.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class AuthApiController
    {
        private readonly IAuthService _service;

        public AuthApiController(IAuthService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/[controller]/SignInWithEmailAndPassword")]
        public async Task<ResponseModel> Get([FromBody] AuthModel authModel) => await _service.SignInWithEmailAndPassword(authModel);
    }
}
