using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class BrandConverter
    {
        public static BrandModel ToModel(brand brand) => new()
        {
            id = brand.id,
            name = brand.name,
            image_url = brand.image_url,
        };
    }
}
