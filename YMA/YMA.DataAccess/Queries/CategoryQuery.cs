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

        public CategoryQuery(ymaContext db, ResponseHelper responseHelper)
        {
            _db = db;
            _responseHelper = responseHelper;
        }

        public ResponseModel GetCategoryById(int id) => _responseHelper.TryCatch(
            "CategoryQuery.GetCategoryById",
            () =>
            {
                category? category = _db.categories.Where(x => x.id == id).FirstOrDefault();
                if (category == null)
                {
                    return new ResponseModel()
                    {
                        message = "Kategori bulunamadı.",
                    };
                }
                if (category.is_disabled ?? false)
                {
                    return new ResponseModel()
                    {
                        message = "Kategori devre dışı.",
                    };
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = CategoryConverter.ToModel(category),
                };
            }
          );

        public ResponseModel GetCategoryList() => _responseHelper.TryCatch(
            "CategoryQuery.GetCategoryList",
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

        public ResponseModel GetCategoryList(string searchText) => _responseHelper.TryCatch(
            "CategoryQuery.GetCategoryListBySearch",
            () =>
            {
                List<CategoryModel> categoryList = _db.categories.Where(x => x.is_disabled == false).ToList().Where(x => SearchHelper.IsSearchedText(x.name, searchText)).OrderBy(x => x.name).Select(x => CategoryConverter.ToModel(x)).ToList();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = categoryList,
                };
            }
          );

        public ResponseModel GetFeaturedCategoryList(int length) => _responseHelper.TryCatch(
            "CategoryQuery.GetFeaturedCategoryList",
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
                if (categoryList.Count > length)
                {
                    categoryList = categoryList.GetRange(0, length);
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = categoryList,
                };
            }
          );
    }
}
