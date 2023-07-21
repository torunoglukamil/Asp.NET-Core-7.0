using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class FavoriteProductConverter
    {
        public static FavoriteProductModel ToModel(favorite_product favoriteProduct) => new()
        {
            product_id = favoriteProduct.product_id,
            account_id = favoriteProduct.account_id,
        };

        public static favorite_product ToFavoriteProduct(FavoriteProductModel favoriteProduct) => new()
        {
            product_id = favoriteProduct.product_id,
            account_id = favoriteProduct.account_id,
        };
    }
}
