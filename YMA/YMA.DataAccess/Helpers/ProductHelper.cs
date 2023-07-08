using YMA.Entities.Models;

namespace YMA.DataAccess.Helpers
{
    public class ProductHelper
    {
        public static List<ProductModel> GetProductListBySearch(List<ProductModel> productList, string? searchText)
        {
            List<ProductModel> productModelList = new();
            productList.ForEach(x =>
            {
                List<string?> searchList = new()
                {
                    x.name,
                    x.model,
                    x.year,
                    x.code,
                    x.oem_no,
                    x.brand?.name,
                    x.company?.name,
                };
                if (SearchHelper.IsSearchedText(searchList, searchText ?? ""))
                {
                    productModelList.Add(x);
                }
            });
            return productModelList;
        }
    }
}
