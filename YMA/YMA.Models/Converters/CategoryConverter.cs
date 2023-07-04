using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class CategoryConverter
    {
        public static CategoryModel ToModel(category category) => new()
        {
            id = category.id,
            name = category.name,
            icon_url = category.icon_url,
        };
    }
}
