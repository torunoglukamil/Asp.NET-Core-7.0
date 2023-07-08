﻿using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class BrandQuery
    {
        private readonly ymaContext _db;
        private readonly ResponseHelper _responseHelper;

        public BrandQuery(ymaContext db, ResponseHelper responseHelper)
        {
            _db = db;
            _responseHelper = responseHelper;
        }

        public ResponseModel GetBrandById(int id) => _responseHelper.TryCatch(
            "BrandQuery.GetBrandById",
            () =>
            {
                brand? brand = _db.brands.Where(x => x.id == id).FirstOrDefault();
                if (brand == null)
                {
                    return new ResponseModel()
                    {
                        message = "Marka bulunamadı.",
                    };
                }
                if (brand.is_disabled ?? false)
                {
                    return new ResponseModel()
                    {
                        message = "Marka devre dışı.",
                    };
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = BrandConverter.ToModel(brand),
                };
            }
          );

        public ResponseModel GetBrandList(string? searchText) => _responseHelper.TryCatch(
            "BrandQuery.GetBrandList",
            () =>
            {
                List<BrandModel> brandList = _db.brands.Where(x => x.is_disabled == false).OrderBy(x => x.name).Select(x => BrandConverter.ToModel(x)).ToList();
                brandList = BrandHelper.GetBrandListBySearch(brandList, searchText);
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = brandList,
                };
            }
          );

        public ResponseModel GetFeaturedBrandList(int? length, string? searchText) => _responseHelper.TryCatch(
            "BrandQuery.GetFeaturedBrandList",
            () =>
            {
                List<BrandModel> brandList = new();
                _db.featured_brands.OrderByDescending(x => x.order_counter).ToList().ForEach(x =>
                {
                    brand? brand = _db.brands.Where(y => y.id == x.brand_id).Where(x => x.is_disabled == false).FirstOrDefault();
                    if (brand != null)
                    {
                        brandList.Add(BrandConverter.ToModel(brand));
                    }
                });
                brandList = BrandHelper.GetBrandListBySearch(brandList, searchText);
                if ((length ?? 0) != 0 && brandList.Count > length)
                {
                    brandList = brandList.GetRange(0, length ?? 1);
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
