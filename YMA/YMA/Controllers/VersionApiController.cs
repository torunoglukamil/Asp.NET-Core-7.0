using Microsoft.AspNetCore.Mvc;
using YMA.Business.Services;
using YMA.Entities.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class VersionApiController
    {
        private readonly VersionService _versionService;

        public VersionApiController(VersionService versionService)
        {
            _versionService = versionService;
        }

        [HttpGet]
        [Route("api/[controller]/GetVersion/{name}")]
        public ResponseModel GetVersion(string name) => _versionService.Query.GetVersion(name);
    }
}
