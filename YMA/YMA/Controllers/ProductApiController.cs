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
        [Route("api/[controller]/GetFavoriteProductList/{accountId}/{searchText?}")]
        public ResponseModel GetFavoriteProductList(string accountId, string? searchText) => _productService.Query.GetFavoriteProductList(accountId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetFeaturedProductList/{length?}/{searchText?}")]
        public ResponseModel GetFeaturedProductList(int? length, string? searchText) => _productService.Query.GetFeaturedProductList(length, searchText);

        [HttpGet]
        [Route("api/[controller]/GetCategoryProductList/{id}/{searchText?}")]
        public ResponseModel GetCategoryProductList(string id, string? searchText) => _productService.Query.GetCategoryProductList(id, searchText);

        [HttpGet]
        [Route("api/[controller]/GetBrandProductList/{id}/{searchText?}")]
        public ResponseModel GetBrandProductList(string id, string? searchText) => _productService.Query.GetBrandProductList(id, searchText);

        [HttpGet]
        [Route("api/[controller]/GetCompanyProductList/{id}/{searchText?}")]
        public ResponseModel GetCompanyProductList(string id, string? searchText) => _productService.Query.GetCompanyProductList(id, searchText);
    }
}
