using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class ProductQuery
    {
        private readonly ymaContext _db;
        private readonly BrandQuery _brandQuery;
        private readonly CategoryQuery _categoryQuery;
        private readonly CompanyQuery _companyQuery;
        private readonly ResponseHelper _responseHelper;

        public ProductQuery(ymaContext db, BrandQuery brandQuery, CategoryQuery categoryQuery, CompanyQuery companyQuery, ResponseHelper responseHelper)
        {
            _db = db;
            _brandQuery = brandQuery;
            _categoryQuery = categoryQuery;
            _companyQuery = companyQuery;
            _responseHelper = responseHelper;
        }

        private List<ProductModel> GetProductModelList(List<product> productList)
        {
            List<ProductModel> productModelList = new();
            productList.ForEach(x =>
            {
                int? brandId = x.brand_id;
                int? categoryId = x.category_id;
                int? companyId = x.company_id;
                if (brandId != null && categoryId != null && companyId != null)
                {
                    ResponseModel response = _brandQuery.GetBrandById(brandId ?? 1);
                    if (response.status_code == StatusCodes.Status400BadRequest)
                    {
                        return;
                    }
                    BrandModel brand = response.data!;
                    response = _categoryQuery.GetCategoryById(categoryId ?? 1);
                    if (response.status_code == StatusCodes.Status400BadRequest)
                    {
                        return;
                    }
                    CategoryModel category = response.data!;
                    response = _companyQuery.GetCompanyById(companyId ?? 1);
                    if (response.status_code == StatusCodes.Status400BadRequest)
                    {
                        return;
                    }
                    CompanyModel company = response.data!;
                    productModelList.Add(ProductConverter.ToModel(x, brand, category, company));
                }
            });
            return productModelList;
        }

        public ResponseModel GetFeaturedProductList(int? length, string? searchText) => _responseHelper.TryCatch(
            "ProductQuery.GetFeaturedProductList",
            () =>
            {
                List<product> productList = new();
                _db.featured_products.OrderByDescending(x => x.order_counter).ToList().ForEach(x =>
                {
                    product? product = _db.products.Where(y => y.id == x.product_id).Where(x => x.is_disabled == false).FirstOrDefault();
                    if (product != null)
                    {
                        productList.Add(product);
                    }
                });
                List<ProductModel> productModelList = GetProductModelList(productList);
                productModelList = ProductHelper.GetProductListBySearch(productModelList, searchText);
                if ((length ?? 0) != 0 && productModelList.Count > length)
                {
                    productModelList = productModelList.GetRange(0, length ?? 1);
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = productModelList,
                };
            }
          );
    }
}
