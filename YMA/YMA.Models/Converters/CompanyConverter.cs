using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class CompanyConverter
    {
        public static CompanyModel ToModel(company company) => new()
        {
            id = company.id,
            name = company.name,
            image_url = company.image_url,
            email = company.email,
            phone = company.phone,
            web = company.web,
            address = company.address,
            theme_color = company.theme_color,
        };
    }
}
