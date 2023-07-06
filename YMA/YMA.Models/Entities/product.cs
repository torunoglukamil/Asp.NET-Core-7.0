using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class product
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

    public int? brand_id { get; set; }

    public int? category_id { get; set; }

    public int? company_id { get; set; }

    public DateTime? create_date { get; set; }

    public bool? is_disabled { get; set; }

    public int? stock_counter { get; set; }

    public virtual brand? brand { get; set; }

    public virtual category? category { get; set; }

    public virtual company? company { get; set; }

    public virtual ICollection<featured_product> featured_products { get; set; } = new List<featured_product>();
}
