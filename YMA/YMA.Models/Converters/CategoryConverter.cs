using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class CategoryConverter
    {
        public CategoryModel ToModel(category category) => new CategoryModel()
        {
            id = category.id,
            name = category.name,
            icon_url = category.icon_url,
        };
    }
}
