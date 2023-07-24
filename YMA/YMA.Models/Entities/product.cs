using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class product
{
    public string id { get; set; } = null!;

    public string? name { get; set; }

    public string? model { get; set; }

    public string? year { get; set; }

    public string? description { get; set; }

    public string? image_url { get; set; }

    public string? code { get; set; }

    public string? oem_no { get; set; }

    public double? price { get; set; }

    public double? discount { get; set; }

    public int? stock_counter { get; set; }

    public string? brand_id { get; set; }

    public string? category_id { get; set; }

    public string? company_id { get; set; }

    public DateTime? create_date { get; set; }

    public bool? is_disabled { get; set; }
}
