using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class CategoryQuery
    {
        private readonly ymaContext _db;
        private readonly ResponseHelper _responseHelper;
        private readonly CategoryConverter _categoryConverter;

        public CategoryQuery(ymaContext db, ResponseHelper responseHelper, CategoryConverter categoryConverter)
        {
            _db = db;
            _responseHelper = responseHelper;
            _categoryConverter = categoryConverter;
        }

        public ResponseModel GetCategoryList() => _responseHelper.TryCatch(
             () =>
             {
                 List<CategoryModel> categoryList = _db.categories.Where(x => x.is_disabled == false).OrderBy(x => x.name).Select(x => _categoryConverter.ToModel(x)).ToList();
                 return new ResponseModel()
                 {
                     status_code = StatusCodes.Status200OK,
                     data = categoryList,
                 };
             }
          );

        public ResponseModel GetFeaturedCategoryList() => _responseHelper.TryCatch(
             () =>
             {
                 List<CategoryModel> categoryList = new List<CategoryModel>();
                 _db.featured_categories.OrderByDescending(x => x.order_counter).ToList().ForEach(x =>
                 {
                     category? category = _db.categories.Where(y => y.id == x.category_id).FirstOrDefault();
                     if (category != null)
                     {
                         categoryList.Add(_categoryConverter.ToModel(category));
                     }
                 });
                 return new ResponseModel()
                 {
                     status_code = StatusCodes.Status200OK,
                     data = categoryList,
                 };
             }
          );
    }
}
