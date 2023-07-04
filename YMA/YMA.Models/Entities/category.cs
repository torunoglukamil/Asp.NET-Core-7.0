using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class category
{
    public int id { get; set; }

    public string? name { get; set; }

    public string? icon_url { get; set; }

    public bool? is_disabled { get; set; }
}
