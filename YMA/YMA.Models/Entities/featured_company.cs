using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class featured_company
{
    public int id { get; set; }

    public int? company_id { get; set; }

    public int? order_counter { get; set; }

    public virtual company? company { get; set; }
}
