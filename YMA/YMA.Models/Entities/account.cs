using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class account
{
    public int id { get; set; }

    public string? first_name { get; set; }

    public string? last_name { get; set; }

    public string? email { get; set; }

    public string? phone { get; set; }

    public int? default_address_id { get; set; }

    public DateTime? create_date { get; set; }

    public bool? is_disabled { get; set; }

    public virtual address? default_address { get; set; }

    public virtual ICollection<favorite_product> favorite_products { get; set; } = new List<favorite_product>();
}
