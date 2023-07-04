using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class AdConverter
    {
        public static AdModel ToModel(ad ad) => new()
        {
            image_url = ad.image_url,
        };
    }
}
