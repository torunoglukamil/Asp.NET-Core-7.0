namespace YMA.Entities.Models
{
    public class ProductModel
    {
        public int id { get; set; }

        public string? name { get; set; }

        public string? model { get; set; }

        public string? year { get; set; }

        public string? description { get; set; }

        public string? image_url { get; set; }

        public string? code { get; set; }

        public string? oem_no { get; set; }

        public double? price { get; set; }

        public double? discount { get; set; }

        public BrandModel? brand { get; set; }

        public CategoryModel? category { get; set; }

        public CompanyModel? company { get; set; }
    }
}
