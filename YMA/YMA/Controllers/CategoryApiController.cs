using Microsoft.AspNetCore.Mvc;
using YMA.Business.Services;
using YMA.Entities.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class CategoryApiController
    {
        private readonly CategoryService _categoryService;

        public CategoryApiController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("api/[controller]/GetCategoryList")]
        public ResponseModel GetCategoryList() => _categoryService.Query.GetCategoryList();

        [HttpGet]
        [Route("api/[controller]/GetFeaturedCategoryList")]
        public ResponseModel GetFeaturedCategoryList() => _categoryService.Query.GetFeaturedCategoryList();
    }
}
