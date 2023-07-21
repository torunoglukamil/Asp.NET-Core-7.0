using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class favorite_product
{
    public int id { get; set; }

    public int? product_id { get; set; }

    public int? account_id { get; set; }

    public DateTime? create_date { get; set; }

    public virtual account? account { get; set; }

    public virtual product? product { get; set; }
}
