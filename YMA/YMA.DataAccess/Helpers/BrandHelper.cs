using YMA.Entities.Models;

namespace YMA.DataAccess.Helpers
{
    public class BrandHelper
    {
        public static List<BrandModel> GetBrandListBySearch(List<BrandModel> brandList, string? searchText)
        {
            List<BrandModel> brandModelList = new();
            brandList.ForEach(x =>
            {
                if (SearchHelper.IsSearchedText(x.name, searchText ?? ""))
                {
                    brandModelList.Add(x);
                }
            });
            return brandModelList;
        }
    }
}
