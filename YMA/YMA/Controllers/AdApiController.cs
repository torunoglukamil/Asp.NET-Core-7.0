using Microsoft.AspNetCore.Mvc;
using YMA.Business.Services;
using YMA.Entities.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class AdApiController
    {
        private readonly AdService _adService;

        public AdApiController(AdService adService)
        {
            _adService = adService;
        }

        [HttpGet]
        [Route("api/[controller]/GetAdList")]
        public ResponseModel GetAdList() => _adService.Query.GetAdList();
    }
}
