using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class BrandQuery
    {
        private readonly ymaContext _db;

        public BrandQuery(ymaContext db)
        {
            _db = db;
        }

        public ResponseModel GetBrandList() => ResponseHelper.TryCatch(
             () =>
             {
                 List<BrandModel> brandList = _db.brands.Where(x => x.is_disabled == false).OrderBy(x => x.name).Select(x => BrandConverter.ToModel(x)).ToList();
                 return new ResponseModel()
                 {
                     status_code = StatusCodes.Status200OK,
                     data = brandList,
                 };
             }
          );

        public ResponseModel GetBrandList(string searchText) => ResponseHelper.TryCatch(
             () =>
             {
                 List<BrandModel> brandList = _db.brands.Where(x => x.is_disabled == false).ToList().Where(x => SearchHelper.IsSearchedText(x.name, searchText)).OrderBy(x => x.name).Select(x => BrandConverter.ToModel(x)).ToList();
                 return new ResponseModel()
                 {
                     status_code = StatusCodes.Status200OK,
                     data = brandList,
                 };
             }
          );

        public ResponseModel GetFeaturedBrandList(int length) => ResponseHelper.TryCatch(
             () =>
             {
                 List<BrandModel> brandList = new();
                 _db.featured_brands.OrderByDescending(x => x.order_counter).ToList().ForEach(x =>
                 {
                     brand? brand = _db.brands.Where(y => y.id == x.brand_id).FirstOrDefault();
                     if (brand != null)
                     {
                         brandList.Add(BrandConverter.ToModel(brand));
                     }
                 });
                 if (brandList.Count > length)
                 {
                     brandList = brandList.GetRange(0, length);
                 }
                 return new ResponseModel()
                 {
                     status_code = StatusCodes.Status200OK,
                     data = brandList,
                 };
             }
          );
    }
}
