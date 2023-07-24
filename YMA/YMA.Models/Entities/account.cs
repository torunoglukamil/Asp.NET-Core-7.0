using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class account
{
    public string id { get; set; } = null!;

    public string? first_name { get; set; }

    public string? last_name { get; set; }

    public string? email { get; set; }

    public string? phone { get; set; }

    public string? default_address_id { get; set; }

    public DateTime? create_date { get; set; }

    public bool? is_disabled { get; set; }
}
