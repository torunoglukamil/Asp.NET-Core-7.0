using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class category
{
    public int id { get; set; }

    public string? name { get; set; }

    public string? icon_url { get; set; }

    public bool? is_disabled { get; set; }

    public virtual ICollection<featured_category> featured_categories { get; set; } = new List<featured_category>();

    public virtual ICollection<product> products { get; set; } = new List<product>();
}
