using YMA.Entities.Models;

namespace YMA.DataAccess.Helpers
{
    public class CategoryHelper
    {
        public static List<CategoryModel> GetCategoryListBySearch(List<CategoryModel> categoryList, string? searchText)
        {
            List<CategoryModel> categoryModelList = new();
            categoryList.ForEach(x =>
            {
                if (SearchHelper.IsSearchedText(x.name, searchText ?? ""))
                {
                    categoryModelList.Add(x);
                }
            });
            return categoryModelList;
        }
    }
}
