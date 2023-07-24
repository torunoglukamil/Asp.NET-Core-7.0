using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class address
{
    public string id { get; set; } = null!;

    public string? title { get; set; }

    public string? full_address { get; set; }

    public string? province { get; set; }

    public string? district { get; set; }

    public string? neighbourhood { get; set; }

    public DateTime? create_date { get; set; }

    public bool? is_disabled { get; set; }
}
