using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class AdConverter
    {
        public AdModel ToModel(ad ad) => new AdModel()
        {
            image_url = ad.image_url,
        };
    }
}
