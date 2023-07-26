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
        [Route("api/[controller]/GetFavoriteProductList/{accountId}/{requestingCompanyId}/{searchText?}")]
        public ResponseModel GetFavoriteProductList(string accountId, string requestingCompanyId, string? searchText) => _productService.Query.GetFavoriteProductList(accountId, requestingCompanyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetFeaturedProductList/{requestingCompanyId}/{length?}/{searchText?}")]
        public ResponseModel GetFeaturedProductList(string requestingCompanyId, int? length, string? searchText) => _productService.Query.GetFeaturedProductList(requestingCompanyId, length, searchText);

        [HttpGet]
        [Route("api/[controller]/GetCategoryProductList/{categoryId}/{requestingCompanyId}/{searchText?}")]
        public ResponseModel GetCategoryProductList(string categoryId, string requestingCompanyId, string? searchText) => _productService.Query.GetCategoryProductList(categoryId, requestingCompanyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetBrandProductList/{brandId}/{requestingCompanyId}/{searchText?}")]
        public ResponseModel GetBrandProductList(string brandId, string requestingCompanyId, string? searchText) => _productService.Query.GetBrandProductList(brandId, requestingCompanyId, searchText);

        [HttpGet]
        [Route("api/[controller]/GetCompanyProductList/{companyId}/{requestingCompanyId}/{searchText?}")]
        public ResponseModel GetCompanyProductList(string companyId, string requestingCompanyId, string? searchText) => _productService.Query.GetCompanyProductList(companyId, requestingCompanyId, searchText);
    }
}
