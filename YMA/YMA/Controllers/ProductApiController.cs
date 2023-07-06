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
        [Route("api/[controller]/GetProductList")]
        public ResponseModel GetProductList() => _productService.Query.GetProductList();

        [HttpGet]
        [Route("api/[controller]/GetProductList/{searchText}")]
        public ResponseModel GetProductList(string searchText) => _productService.Query.GetProductList(searchText);

        [HttpGet]
        [Route("api/[controller]/GetFeaturedProductList/{length}")]
        public ResponseModel GetFeaturedProductList(int length) => _productService.Query.GetFeaturedProductList(length);
    }
}
