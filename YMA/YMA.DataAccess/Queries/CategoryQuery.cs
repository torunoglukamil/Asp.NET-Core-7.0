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

        public CategoryQuery(ymaContext db)
        {
            _db = db;
        }

        public ResponseModel GetCategoryList() => ResponseHelper.TryCatch(
             () =>
             {
                 List<CategoryModel> categoryList = _db.categories.Where(x => x.is_disabled == false).OrderBy(x => x.name).Select(x => CategoryConverter.ToModel(x)).ToList();
                 return new ResponseModel()
                 {
                     status_code = StatusCodes.Status200OK,
                     data = categoryList,
                 };
             }
          );

        public ResponseModel GetFeaturedCategoryList() => ResponseHelper.TryCatch(
             () =>
             {
                 List<CategoryModel> categoryList = new();
                 _db.featured_categories.OrderByDescending(x => x.order_counter).ToList().ForEach(x =>
                 {
                     category? category = _db.categories.Where(y => y.id == x.category_id).FirstOrDefault();
                     if (category != null)
                     {
                         categoryList.Add(CategoryConverter.ToModel(category));
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
