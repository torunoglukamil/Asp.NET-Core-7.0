using Microsoft.AspNetCore.Mvc;
using YMA.Business.Services;
using YMA.Entities.Models;

namespace YMA.Controllers
{
    [ApiController]
    public class FavoriteProductApiController
    {
        private readonly FavoriteProductService _favoriteProductService;

        public FavoriteProductApiController(FavoriteProductService favoriteProductService)
        {
            _favoriteProductService = favoriteProductService;
        }

        [HttpPut]
        [Route("api/[controller]/UpdateFavoriteProduct/{productId}/{accountId}")]
        public ResponseModel UpdateFavoriteProduct(int productId, int accountId) => _favoriteProductService.Repository.UpdateFavoriteProduct(productId, accountId);

        [HttpDelete]
        [Route("api/[controller]/DeleteAllFavoriteProducts/{accountId}")]
        public ResponseModel DeleteAllFavoriteProducts(int accountId) => _favoriteProductService.Repository.DeleteAllFavoriteProducts(accountId);
    }
}
