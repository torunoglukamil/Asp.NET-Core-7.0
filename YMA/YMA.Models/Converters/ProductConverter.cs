using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class ProductConverter
    {
        public static ProductModel ToModel(product product, BrandModel brand, CategoryModel category, CompanyModel company) => new()
        {
            id = product.id,
            name = product.name,
            model = product.model,
            year = product.year,
            description = product.description,
            image_url = product.image_url,
            code = product.code,
            oem_no = product.oem_no,
            price = product.price,
            discount = product.discount,
            is_in_stock = product.stock_counter != 0,
            brand = brand,
            category = category,
            company = company,
        };
    }
}
