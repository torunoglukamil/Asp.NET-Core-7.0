using Microsoft.AspNetCore.Mvc;
using YMA.Business.Services;
using YMA.Entities.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class ProductApiController
    {
        private readonly ProductService _productService;

        public ProductApiController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("api/[controller]/GetFeaturedProductList/{length?}/{searchText?}")]
        public ResponseModel GetFeaturedProductList(int? length, string? searchText) => _productService.Query.GetFeaturedProductList(length, searchText);
    }
}
