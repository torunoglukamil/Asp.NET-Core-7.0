using Microsoft.AspNetCore.Mvc;
using YMA.Business.Services;
using YMA.Entities.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class BrandApiController
    {
        private readonly BrandService _brandService;

        public BrandApiController(BrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [Route("api/[controller]/GetBrandList/{searchText?}")]
        public ResponseModel GetBrandList(string? searchText) => _brandService.Query.GetBrandList(searchText);

        [HttpGet]
        [Route("api/[controller]/GetFeaturedBrandList/{length?}/{searchText?}")]
        public ResponseModel GetFeaturedBrandList(int? length, string? searchText) => _brandService.Query.GetFeaturedBrandList(length, searchText);
    }
}
