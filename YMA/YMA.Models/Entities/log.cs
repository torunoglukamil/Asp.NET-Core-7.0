using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class log
{
    public string? type { get; set; }

    public string? message { get; set; }

    public string? data { get; set; }

    public DateTime? create_date { get; set; }
}
